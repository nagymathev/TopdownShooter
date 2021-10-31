using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    public float timeBetweenEnemy = 5f;
    private float countDown;
    public float maxSpawnRate = 0.5f;
    public float spawnRateIncrement = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //countDown = timeBetweenEnemy;

        if (countDown <= 0)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefabs[0], spawnPoints[randSpawnPoint].position, transform.rotation);
            timeBetweenEnemy -= spawnRateIncrement;
            if (timeBetweenEnemy <= maxSpawnRate)
            {
                timeBetweenEnemy = maxSpawnRate;
            }

            countDown = timeBetweenEnemy;
        }
        else
        {
            countDown -= Time.deltaTime;
        }
    }
}
