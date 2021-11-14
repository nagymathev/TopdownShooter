using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;

    public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);

        Destroy(gameObject);


		//Debug.Log(collision.otherCollider.gameObject);
		Health health = collision.otherCollider.gameObject.GetComponent<Health>();

		if (health == null)
        {
            return;
        }

		//Debug.Log(health);

        //health.health -= damage;
		health.Hurt(damage);
    }

}
