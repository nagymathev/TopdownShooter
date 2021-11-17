using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class Leaderboard: MonoBehaviour
{
	[System.Serializable]
	public struct Score
	{
		public string name;
		public int score;
	}

	public List<Score> scores = new List<Score>();

	public bool scoresWaiting;
	public bool scorePosting;

	public List<Score> RetrieveScores()
    {
		Debug.Log("Retrieve");
		scores = new List<Score>();
        StartCoroutine(DoRetrieveScores());
		//returns it by reference; it will be filled out in the background!
        return scores;
    }

    public void PostScore(string name, int score)
    {
		Debug.Log("Post");
		StartCoroutine(DoPostScore(name, score));
    }

    public IEnumerator DoRetrieveScores()
    {
		Debug.Log("Retrieving scores...");

		scoresWaiting = true;

		string URL = "http://www.magicmotiongames.com/gethighscores.php";
		WWWForm form = new WWWForm();
        //form.AddField("retrieve_leaderboard", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            //if (www.isNetworkError || www.isHttpError)
			if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully retrieved scores!");
                string contents = www.downloadHandler.text;
				Debug.Log(contents);

                using (StringReader reader = new StringReader(contents))
                {
					int numScores = -1;
					string line;
                    while ((line = reader.ReadLine()) != null)
                    {
						if (numScores > 0)
						{
							string[] parts = line.Split(new char[]{','});
							if (parts.Length >= 2)
							{
								Score entry = new Score();
								entry.name = parts[0];
								try
								{
									entry.score = Int32.Parse(parts[1]);
								}
								catch (Exception e)
								{
									Debug.Log("Invalid score: " + e);
									continue;
								}
								scores.Add(entry);
							}
							numScores--;
							continue;
						} 
						if (line.StartsWith("RESULTS:"))
						{
							numScores = Int32.Parse(line.Substring(8));
						}
                    }
                }
            }
        }

		scoresWaiting = false;
	}

	IEnumerator DoPostScore(string name, int score)
    {
		Debug.Log("Posting score...");

		scorePosting = true;

		string URL = "http://www.magicmotiongames.com/addhighscore.php";
		WWWForm form = new WWWForm();
        //form.AddField("post_leaderboard", "true");
        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

			//if (www.isNetworkError || www.isHttpError)
			if (www.result != UnityWebRequest.Result.Success)
			{
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Successfully posted score!");
            }
        }

		scorePosting = false;
	}
}
