using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public int scoreReward = 100;

    //private GameObject scoreBoard;
    Score scorecomponent;

    // Start is called before the first frame update
    void Start()
    {
         scorecomponent = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        //score = score.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            scorecomponent.score += scoreReward;
            OnKill();
        }

    }

    void OnKill()
    {
        Destroy(gameObject);
        
    }
}
