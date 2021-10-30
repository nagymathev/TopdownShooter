using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public int scoreReward = 100;

    private GameObject scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        int score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score;
        //Score score = scoreBoard.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            score += scoreReward;
            OnKill();
        }

    }

    void OnKill()
    {
        Destroy(gameObject);
        
    }
}
