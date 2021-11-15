using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject topLeft;
    public GameObject bottomRight;

    public GameObject[] powerUps;

    public float countDown;
    public float maxCountDown;
    // Start is called before the first frame update
    void Start()
    {
        countDown = maxCountDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0)
        {
			//GameObject target = GameObject.FindGameObjectWithTag("Player");
			//if (!target) return;

			//int randEnemy = Random.Range(0, wave.enemyPrefabs.Length);
			//int randSpawnPoint = Random.Range(0, wave.spawnPoints.Length);

			//powerUp spawn
			int index = Random.Range(0, 1000) % powerUps.Length;
            SpawnPowerUp(new Vector3(Random.Range(bottomRight.transform.position.x, topLeft.transform.position.x), Random.Range(bottomRight.transform.position.y, topLeft.transform.position.y), 0), powerUps[index]);
            //Instantiate(powerUps[index], new Vector3(Random.Range(bottomRight.transform.position.x, topLeft.transform.position.x),Random.Range(bottomRight.transform.position.y, topLeft.transform.position.y),0), Quaternion.identity);


            //timeBetweenEnemy -= spawnRateIncrement;
            //if (timeBetweenEnemy <= maxSpawnRate)
            //{
            //    timeBetweenEnemy = maxSpawnRate;
            //}

            //countDown = timeBetweenEnemy;
            countDown = maxCountDown;
        }
        else
        {
            countDown -= Time.deltaTime;
        }
    }

    public void SpawnPowerUp(Vector3 position, GameObject powerUp)
    {
        Instantiate(powerUp, position, Quaternion.identity);
    }
}