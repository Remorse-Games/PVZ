using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject nextWaveButton;
    [System.Serializable]
    public struct EnemyGroup
    {
        public EnemyData enemyData;
        public int amount;
        public float spawnDelayPerEnemy;//spawnDelay=1/x secs
        public List<Enemy.MovementData> path;
    };
    [System.Serializable]
    public struct EnemyWave
    {
        public List<EnemyGroup> enemies;
        public float spawnDelayPerEnemyGroup;
    };
    public List<EnemyWave> waves;
    public float spawnDelayPerWave = 10f;
    private int currentWave = 0;
    private bool forceNext, currentWaveDone;
    private Coroutine wait;
    [HideInInspector]
    public List<Enemy> enemyAlive;
    public List<AudioClip> waveVOs;
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        enemyAlive = new List<Enemy>();
        if (waves.Count == 0) return; 
        nextWaveButton.SetActive(true);
        wait = StartCoroutine(WaitForNextWave());
    }
    private void StartWave(int waveIndex)
    {
        if (waveIndex < waves.Count)
        {
            if (wait != null)
            {
                StopCoroutine(wait);
            }
            nextWaveButton.SetActive(false);
            forceNext = false;
            currentWaveDone = false;
            StartSpawn(waves[waveIndex].enemies, waves[waveIndex].spawnDelayPerEnemyGroup);
        }
    }
    private void StartSpawn(List<EnemyGroup> enemies, float waitTime)
    {
        StartCoroutine(Spawning(enemies, waitTime));
    }
    public void ForceNextWave()
    {
        forceNext = true;
        StartWave(currentWave);
        currentWave++;
    }
    public void RemoveEnemy(Enemy enemy)
    {
        if (enemyAlive.Contains(enemy))
        {
            enemyAlive.Remove(enemy);
            if (currentWaveDone)
            {
                if (enemyAlive.Count == 0)
                {
                    if (waveVOs.Count >= currentWave)
                    {
                        source.clip = waveVOs[currentWave - 1];
                        source.Play();
                    }
                    nextWaveButton.SetActive(true);
                    wait = StartCoroutine(WaitForNextWave());
                }
            }
        }
    }
    private IEnumerator Spawning(List<EnemyGroup> enemies, float waitTime)
    {
        if (waitTime == 0) waitTime = 10f;
        int length = enemies.Count;
        for (int i = 0; i < length; i++)
        {
            if (enemies[i].spawnDelayPerEnemy <= 0f)
            {
                EnemyGroup enemy = enemies[i];
                enemy.spawnDelayPerEnemy = 1f;
            }
            for (int j = 0; j < enemies[i].amount; j++)
            {
                Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
                if (!enemies[i].enemyData.isMelee)
                {
                    EnemyRanged ranged = enemy.gameObject.AddComponent<EnemyRanged>();
                    ranged.range = enemies[i].enemyData.range;
                    ranged.towerMask = enemy.towerMask;
                    ranged.sliderHP = enemy.sliderHP;
                    Destroy(enemy);
                    enemy = ranged;
                }
                enemy.GetComponent<Animator>().runtimeAnimatorController = enemies[i].enemyData.animatorController;
                enemyAlive.Add(enemy);
                enemy.SetEnemy(enemies[i].enemyData, enemies[i].path, this);
                if (j < enemies[i].amount - 1)
                {
                    yield return new WaitForSeconds(enemies[i].spawnDelayPerEnemy);
                }
            }
            if (i < length - 1)
            {
                yield return new WaitForSeconds(waitTime);
            }
        }
        currentWaveDone = true;
    }
    private IEnumerator WaitForNextWave()
    {
        yield return new WaitForSeconds(spawnDelayPerWave);
        if (!forceNext)
        {
            StartWave(currentWave);
            currentWave++;
        }
    }
}
