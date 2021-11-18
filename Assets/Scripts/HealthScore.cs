using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScore : MonoBehaviour
{

    public Text healthText;
    public float healthScore;

    public PlayerHealth playerHealthComponent;

    // Start is called before the first frame update
    void Start()
    {
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		playerHealthComponent = player ? player.GetComponent<PlayerHealth>() : null;
    }

    // Update is called once per frame
    void Update()
    {
		if (!playerHealthComponent) return;
        healthScore = playerHealthComponent.health;
        healthText.text = healthScore.ToString();
    }
}
