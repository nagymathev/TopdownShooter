using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public int scoreReward = 100;

    //private GameObject scoreBoard;
    Score scorecomponent;

	public GameObject prefabOnHurt;
	public GameObject prefabOnDeath;

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

	public void Hurt(float damage)
	{
		//Debug.Log("hurt " + damage, this);
		health -= damage;

		if (health > 0)
		{
			//ok still alive
			if (prefabOnHurt)
				Instantiate(prefabOnHurt, transform.position, transform.rotation);
			return;
		}

		//died
		OnKill();
	}

	void OnKill()
    {
		//ToDo: particles, audio, notify gameplay, etc
		if (prefabOnDeath)
			Instantiate(prefabOnDeath, transform.position, transform.rotation);
		Destroy(gameObject);
    }
}
