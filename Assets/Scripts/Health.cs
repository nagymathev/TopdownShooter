using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public int scoreReward = 100;

	public GameObject prefabOnHurt;
	public GameObject prefabOnDeath;

	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
		Score scorecomponent = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
		if (scorecomponent)
			scorecomponent.score += scoreReward;

		//ToDo: particles, audio, notify gameplay, etc
		if (prefabOnDeath)
			Instantiate(prefabOnDeath, transform.position, transform.rotation);
		Destroy(gameObject);
    }
}
