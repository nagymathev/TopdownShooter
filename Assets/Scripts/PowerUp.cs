using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float lifeTime = 60f;
    public Type powerUp;

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
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            switch (powerUp) 
            {
                case Type.instantHealth:
                    playerHealth.Heal(50f);
                    break;

                case Type.healthRegen:
                    playerHealth.HealthRegen(80f, 10f);
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