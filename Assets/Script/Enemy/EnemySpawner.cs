using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    public float spawnDelay = 1f; //spawnDelay=1/x secs
    private void Start()
    {
        StartSpawn(7);
    }
    private void StartSpawn(int spawnAmount)
    {
        StartCoroutine(Spawning(spawnAmount));
    }
    private IEnumerator Spawning(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Rigidbody2D rb = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.right;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
