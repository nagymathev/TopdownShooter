using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject player;
	public Score score;
	public Leaderboard leaderboard;

	public GameObject root_InGameUI;
	public GameObject root_GameOverScreen;

	public List<Leaderboard.Score> scores;

	public enum State
	{
		Playing,
		GameOver,
		MAX
	}
	public State state;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		score = FindObjectOfType<Score>();
		leaderboard = FindObjectOfType<Leaderboard>();

		state = player ? State.Playing : State.GameOver;
		Time.timeScale = 1.0f;

		if (leaderboard)
		{
			scores = leaderboard.RetrieveScores();
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
					root_InGameUI.SetActive(false);

					//ToDo: display score, ask for name, upload to leaderboard,
					// refresh high scores and display list,
					// wait for button to restart
					if (Input.GetKeyDown(KeyCode.Return))   //TEMP
					{
						StartCoroutine(Reload());
					}
				}
				break;

			case State.Playing:
			default:
				root_GameOverScreen.SetActive(false);
				root_InGameUI.SetActive(true);
				if (player == null)
				{//died
					state = State.GameOver;
					if (leaderboard && score)
					{
						leaderboard.PostScore("Player X", score.score);
					}
				}
				break;
		}
    }

	private IEnumerator Reload()
	{
		Debug.Log("Reloading...");
		yield return new WaitForSeconds(0.5f);
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		Resources.UnloadUnusedAssets();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		Time.timeScale = 1.0f;
	}

}
