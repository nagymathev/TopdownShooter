using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject player;
	public Score score;
	public Leaderboard leaderboard;
	public UIScript uiScript;
	public HealthScore uiHealth;
	public RandomEnemySpawner spawner;

	//public GameObject root_persistence;

	public GameObject root_InGameUI;
	public GameObject root_GameOverScreen;
	public GameObject root_NameEntry;
	public GameObject root_PressStart;

	public Text your_score;
	public InputField your_name;

	public Text scores_names;
	public Text scores_scores;

	public Text ui_wave_text;

	public List<Leaderboard.Score> scores;

	public enum State
	{
		None,
		Playing,
		GameOver,
		EnterName,
		MAX
	}
	public State state;
	public float stateTime;

	public bool firstStart = true;

	void Awake()
	{
	}

	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");

		if (firstStart)
		{
			Debug.Log("FIRST START");
			firstStart = false;

			if (player)
				Destroy(player);
			SetState(State.GameOver);
		} else
		{
			Debug.Log("NORMAL START");
			SetState(player ? State.Playing : State.GameOver);
			Time.timeScale = 1.0f;
		}

		score = FindObjectOfType<Score>();
		leaderboard = FindObjectOfType<Leaderboard>();
		uiScript = FindObjectOfType<UIScript>();
		uiHealth = FindObjectOfType<HealthScore>();
		spawner = FindObjectOfType<RandomEnemySpawner>();

		score.score = 0;
		uiHealth.playerHealthComponent = player ? player.GetComponent<PlayerHealth>() : null;
		uiScript.shootingScript = player ? player.GetComponent<Shooting>() : null;
		spawner.SetWaveText(ui_wave_text);

		if (leaderboard)
		{
			scores = leaderboard.RetrieveScores();
			StartCoroutine(WaitForScoreDisplay());
		}

		//if (your_name)
		//	your_name.onEndEdit.AddListener(delegate { NameEntered(); });
	}

	IEnumerator WaitForScoreDisplay()
	{
		//if (!leaderboard) yield return null;

		while(leaderboard && leaderboard.scoresWaiting)
		{
			yield return new WaitForSecondsRealtime(0.1f);
		}

		if (scores_names && scores_scores)
		{
			scores_names.text = "";
			scores_scores.text = "";
			//foreach (Leaderboard.Score score in scores)
			for(int i=0; i<scores.Count; i++)
			{
				if (i >= 10) break;	//temp; do not display more than 10, even though we receive more
				Leaderboard.Score score = scores[i];
				scores_names.text += score.name + "\n";
				scores_scores.text += score.score.ToString() + "\n";
			}
		}
	}

	// Update is called once per frame
	void Update()
    {

		switch (state)
		{
			case State.GameOver:
				{
					Time.timeScale = 0.5f;
					root_GameOverScreen.SetActive(true);
					//root_InGameUI.SetActive(false);
					root_NameEntry.SetActive(false);

					//ToDo: display score, ask for name, upload to leaderboard,
					// refresh high scores and display list,
					// wait for button to restart
					if (stateTime < 1.0f)
					{
						root_PressStart.SetActive(false);
					} else
					{
						root_PressStart.SetActive((stateTime % 1.0f) <= 0.6f);
						if (Input.GetKeyDown(KeyCode.Return)
							|| Input.GetMouseButtonDown(0))
						{
							StartCoroutine(Reload());
						}
					}
				}
				break;

			case State.Playing:
				root_GameOverScreen.SetActive(false);
				root_InGameUI.SetActive(true);
				root_NameEntry.SetActive(false);

				if (player == null)
				{//died
					SetState(State.EnterName);
				}
				break;

			case State.EnterName:
				root_GameOverScreen.SetActive(true);
				//root_InGameUI.SetActive(false);
				root_NameEntry.SetActive(true);

				your_score.text = score.score.ToString();
				if (!your_name.isFocused)
					your_name.Select();
				if (Input.GetKeyDown(KeyCode.Return))
					NameEntered();
				break;

			default:
				break;
		}

		stateTime += Time.deltaTime;
	}

	void SetState(State s)
	{
		Debug.Log(state.ToString() + "->" + s.ToString());

		state = s;
		stateTime = 0;
	}

	void NameEntered()
	{
		if (stateTime < 1.0f) return;

		StartCoroutine(DoSubmitScore());
	}

	bool submittingScore = false;
	IEnumerator DoSubmitScore()
	{
		if (submittingScore) yield return null;
		submittingScore = true;

		root_NameEntry.SetActive(false);

		if (your_name && !string.IsNullOrEmpty(your_name.text))
		if (leaderboard && score)
		{
			leaderboard.PostScore(your_name.text, score.score);

			// wait for the posting to finish before retrieving so we get the new one included
			yield return new WaitForSecondsRealtime(1.0f);
			
			scores = leaderboard.RetrieveScores();
			StartCoroutine(WaitForScoreDisplay());
		}

		SetState(State.GameOver);
		submittingScore = false;
	}


	bool reloading = false;
	private IEnumerator Reload()
	{
		if (reloading) yield return null;
		reloading = true;

		Debug.Log("Reloading...");
		root_GameOverScreen.SetActive(false);
		root_InGameUI.SetActive(true);
		root_NameEntry.SetActive(false);

		yield return new WaitForSeconds(0.5f);
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		Resources.UnloadUnusedAssets();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

		yield return new WaitForSeconds(0.1f);
		Time.timeScale = 1.0f;
		Start();

		reloading = false;
	}

}
