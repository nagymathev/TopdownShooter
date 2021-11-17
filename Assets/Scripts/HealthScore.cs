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
        playerHealthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        healthScore = playerHealthComponent.health;
        healthText.text = healthScore.ToString();
    }
}
