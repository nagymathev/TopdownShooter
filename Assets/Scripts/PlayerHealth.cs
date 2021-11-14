using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float health = 100f;

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
		health -= damage;

		if (health > 0)
		{
			//ok still alive
			if (prefabOnHurt)
				Instantiate(prefabOnHurt, transform.position, transform.rotation);
			return;
		}

		//died
		{
			//ToDo: particles, audio, notify gameplay, etc
			if (prefabOnDeath)
				Instantiate(prefabOnDeath, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}



    //powerUp Healing
    public void Heal(float healAmount)
    {
        health += healAmount;
    }

    public IEnumerator HealthRegen(float maxHealing, float healAmount)
    {
        for (float i = 0; i < maxHealing; i += healAmount)
        {


            health += healAmount;
            yield return new WaitForSeconds(1);
        }
    }
}
