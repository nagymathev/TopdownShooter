using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;

    public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
		GameObject other = collision.gameObject;
		Debug.Log(other.name, other);
		//ignore collision with other bullets (shotgun ones spawn at the same point)
		if (other.GetComponent<Bullet>())
		{
			return;
		}

		if (hitEffect)
		{
			GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
			//Destroy(effect, 0.5f);
		}

		Health health = other.GetComponent<Health>();

		if (health == null)
        {
			//self-destruct
			Destroy(gameObject);
			return;
        }

		if (health.health <= 0)
		{
			//if it's already dead, bounce
			return;
		}

		//Debug.Log(health);

        //health.health -= damage;
		health.Hurt(damage);

		//self-destruct
		Destroy(gameObject);
	}

}
