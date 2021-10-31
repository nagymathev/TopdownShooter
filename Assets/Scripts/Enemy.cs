using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    PlayerHealth playerHealthComponent;
    public float damageToPlayer = 25f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        playerHealthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

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
            playerHealthComponent.health -= damageToPlayer;
        }
    }
}
