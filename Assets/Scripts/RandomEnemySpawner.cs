using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEnemySpawner : MonoBehaviour
{
	public Text waveText;

    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

	public GameObject prefab_startGame;
	public GameObject prefab_nextWave;

	public GameObject player;

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
		public int doNotStartUntilFewerThan;	//do not start NEXT wave
		public float doStartAfterMaxWait;		//also

		public Transform[] spawnPoints;
		public GameObject[] enemyPrefabs;
	};

	public List<Wave> waves;
	public int currentWave = 0;
	public int currentLoop = 0;

	public float currentWaveTime;

	public float overallTime;

	public List<GameObject> currentEnemies = new List<GameObject>();

	public int GetWaveIndex()
	{
		return currentWave + 1 + currentLoop * waves.Count;
	}


	// Start is called before the first frame update
	void Start()
    {
		StartWave();

		player = GameObject.FindGameObjectWithTag("Player");

		if (player)
			if (prefab_startGame)
				Instantiate(prefab_startGame);

		overallTime = 0;
	}

	void StartWave()
	{
		currentWaveTime = 0;
		SetWaveText(waveText);
	}

	public void SetWaveText(Text text)
	{
		waveText = text;
		if (waveText)
		{
			waveText.text = string.Format("Wave {0}", GetWaveIndex());// currentWave + 1 + currentLoop * waves.Count);
		}
		//ToDo: UI/audio feedback and warning (reward) :)
	}

	// Update is called once per frame
	void Update()
    {
		//garbage collection
		currentEnemies.RemoveAll(a => a == null);

		overallTime += Time.deltaTime;

		if (currentWave >= waves.Count)
		{
            //end of waves! well, survived everything :)
            //ToDo: notify UI to show victory screen

            //temp.: restart but harder
            if (currentLoop % 2 == 1) 
            {
                for (int i = 0; i < waves.Count; i++)
                {
                    Wave _wave = waves[i];
                    _wave.enemiesPerSecond *= 1.2f;
                    _wave.timeSpawning *= 1f;
                    _wave.timeBefore *= 0.75f;
                    _wave.timeAfter *= 0.75f;
                    waves[i] = _wave;
                }
            }

            if (currentLoop % 2 == 0) 
            {
                for (int i = 0; i < waves.Count; i++)
                {
                    Wave _wave = waves[i];
                    _wave.enemiesPerSecond *= 1f;
                    _wave.timeSpawning *= 1.2f;
                    _wave.timeBefore *= 0.75f;
                    _wave.timeAfter *= 1.2f;
                    waves[i] = _wave;
                }
            }

            currentWave = 0;
			currentLoop++;
			return;
		}

		Wave wave = waves[currentWave];

		if (currentWaveTime >= wave.timeBefore + wave.timeSpawning + wave.timeAfter)
		{
			bool okToStart = true;
			if (currentWaveTime >= wave.timeBefore + wave.timeSpawning + wave.timeAfter + wave.doStartAfterMaxWait)
			{
				okToStart = true;
			} else
			if (wave.doNotStartUntilFewerThan >= 0)
			{
				//check if the number of existing enemies are zero and return if it isn't
				if (!(currentEnemies.Count < wave.doNotStartUntilFewerThan))
					okToStart = false;
			}

			if (okToStart)
			{
				//initialise next wave!
				currentWave++;

				if (prefab_nextWave)
					Instantiate(prefab_nextWave);

				StartWave();
				return;
			}
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

            GameObject enemy = Instantiate(wave.enemyPrefabs[randEnemy], wave.spawnPoints[randSpawnPoint].position, Quaternion.AngleAxis(Random.Range(0,360), Vector3.forward));
			currentEnemies.Add(enemy);
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
