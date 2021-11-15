using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float lifeTime = 60f;
    public Type powerUp;

	public GameObject prefabOnSpawn;
	public GameObject prefabOnPickup;

	public Shooting.WeaponDef weaponDef;

    public enum Type 
    { 
        healthRegen,
        instantHealth,
        weaponSwitch,

        MAX
    }

    // Start is called before the first frame update
    void Start()
    {
		if (prefabOnSpawn)
			Instantiate(prefabOnSpawn, transform.position, transform.rotation);
	}

	// Update is called once per frame
	void Update()
    {


        if(lifeTime <= 0) 
        {
            Destroy(this.gameObject);
        }
        else 
        {
            lifeTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("PowerUp");

        if (collision.gameObject.tag == "Player") 
        {
			if (prefabOnPickup)
				Instantiate(prefabOnPickup, transform.position, transform.rotation);

			PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            Shooting shootingScript = collision.gameObject.GetComponent<Shooting>();

            switch (powerUp) 
            {
                case Type.instantHealth:
                    playerHealth.Heal(50f);
                    break;

                case Type.healthRegen:
                    StartCoroutine(playerHealth.HealthRegen(80f, 10f));
                    //playerHealth.HealthRegen(80f, 10f);
                    break;

                case Type.weaponSwitch:
                    shootingScript.SetWeapon(weaponDef);
                    break;

                default:
                    break;
            }

            Destroy(this.gameObject);
        }

    }

    /*
    void Heal(float health, float healAmount) 
    {
        health += healAmount;
    }

    IEnumerator HealthRegen(float health, float maxHealing, float healAmount)
    {
        for (float i = 0; i < maxHealing; i+=healAmount)
        {


            health += healAmount;
            yield return new WaitForSeconds(5);
        }
    }
    */
}
