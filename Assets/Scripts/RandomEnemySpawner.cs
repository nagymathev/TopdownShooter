using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEnemySpawner : MonoBehaviour
{
	public Text waveText;

    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

	//public float timeBetweenEnemy = 5f;
	//public float maxSpawnRate = 0.5f;
	//public float spawnRateIncrement = 0.1f;
	private float countDown;

	//wave definition
	[System.Serializable]
	public struct Wave
	{
		public float timeBefore;
		public float timeSpawning;
		public float timeAfter;
		//public float timeBetweenEnemy;
		public float enemiesPerSecond;
		public bool waitForAllDead;

		public Transform[] spawnPoints;
		public GameObject[] enemyPrefabs;
	};

	public List<Wave> waves;
	public int currentWave = 0;
	public int currentLoop = 0;

	public float currentWaveTime;


	// Start is called before the first frame update
	void Start()
    {
		StartWave();
	}

	void StartWave()
	{
		currentWaveTime = 0;
		if (waveText)
		{
			waveText.text = string.Format("Wave {0}", currentWave + 1 + currentLoop * waves.Count);
		}
		//ToDo: UI/audio feedback and warning (reward) :)
	}

	// Update is called once per frame
	void Update()
    {
		if (currentWave >= waves.Count)
		{
			//end of waves! well, survived everything :)
			//ToDo: notify UI to show victory screen
			
			//temp.: restart but harder
			for(int i=0; i<waves.Count; i++)
			{
				Wave _wave = waves[i];
				_wave.enemiesPerSecond *= 1.5f;
				_wave.timeSpawning *= 1.5f;
				_wave.timeBefore *= 0.75f;
				_wave.timeAfter *= 0.75f;
                waves[i] = _wave;
			}
			currentWave = 0;
			currentLoop++;
			return;
		}

		Wave wave = waves[currentWave];

		if (currentWaveTime >= wave.timeBefore + wave.timeSpawning + wave.timeAfter)
		{
			if(wave.waitForAllDead)
			{
				//check if the number of existing enemies are zero and return if it isn't
			}

			//initialise next wave!
			currentWave++;

			StartWave();
			return;
		}

		currentWaveTime += Time.deltaTime;

		if (currentWaveTime < wave.timeBefore)
		{
			//do not spawn yet
			return;
		}

		if (currentWaveTime >= wave.timeBefore + wave.timeSpawning)
		{
			//do not spawn any more
			return;
		}

		//spawning time! :)
		if (countDown <= 0)
        {
			//GameObject target = GameObject.FindGameObjectWithTag("Player");
			//if (!target) return;

			int randEnemy = Random.Range(0, 1000) % wave.enemyPrefabs.Length;
            int randSpawnPoint = Random.Range(0, 1000) % wave.spawnPoints.Length;

            Instantiate(wave.enemyPrefabs[randEnemy], wave.spawnPoints[randSpawnPoint].position, transform.rotation);
			//timeBetweenEnemy -= spawnRateIncrement;
			//if (timeBetweenEnemy <= maxSpawnRate)
			//{
			//    timeBetweenEnemy = maxSpawnRate;
			//}

			//countDown = timeBetweenEnemy;
			countDown = wave.enemiesPerSecond > 0 ? 1.0f / wave.enemiesPerSecond : 0.0f;
        }
        else
        {
            countDown -= Time.deltaTime;
        }

		//currentWaveTime += Time.deltaTime;
    }
}
