using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject target;
    public float speed = 5f;
    public Rigidbody2D rb;
    public Vector2 movement;

    
    public float damageToPlayer = 25f;
	public float damageRepeatTime = 1.0f;
	public float damageTimer = 0;

	public bool playerInReach = false;
	PlayerHealth playerHealthComponent;


	// Start is called before the first frame update
	void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
		if (!target)
		{
			if (Random.value < 0.01f)
			{
				movement += Random.insideUnitCircle;
				movement.Normalize();
			}
			float _angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;
			rb.rotation = _angle;
			return;
		}

		if (playerInReach && playerHealthComponent)
		{
			if (damageTimer > 0)
			{
				damageTimer -= Time.deltaTime;
			} else
			{
				playerHealthComponent.Hurt(damageToPlayer);// .health -= damageToPlayer;
				damageTimer = damageRepeatTime;
			}
		}

        Vector3 direction = target.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        movement = direction;
		movement.Normalize();
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
			playerInReach = true;
			//playerHealthComponent.health -= damageToPlayer;
			playerHealthComponent = collision.gameObject.GetComponent<PlayerHealth>();
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			playerInReach = false;
			playerHealthComponent = null;
		}
	}
}
