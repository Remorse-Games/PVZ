using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    public float spawnDelay = 1f; //spawnDelay=1/x secs
    public List<Enemy.MovementData> path;
    public int enemyCount = 10;
    private void Start()
    {
        StartSpawn(enemyCount);
    }
    private void StartSpawn(int spawnAmount)
    {
        StartCoroutine(Spawning(spawnAmount));
    }
    private IEnumerator Spawning(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            enemy.SetPath(path);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
