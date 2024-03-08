using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public Image image;
    public float health;
    public float maxHealth;

    private HealthBarUI healthBarUI;
    // Start is called before the first frame update
    private GameManager gameManager;

    private CharacterController characterController;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterController = GameObject.Find("Character").GetComponent<CharacterController>();
        healthBarUI = GameObject.Find("HealthBar").GetComponent<HealthBarUI>();
        //get level from GameManager
        maxHealth = 100 * (float)Math.Pow(1.1, gameManager.level);
        health = maxHealth;
        healthBarUI.SetMaxHealth(health);
        healthBarUI.SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        //do something when left click
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(characterController.damage);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
            gameManager.HandleMonsterSpawn();
        }

        if (Time.frameCount % 60 == 0 && characterController.passiveDamage > 0)
        {
            TakeDamage(characterController.passiveDamage);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthBarUI.SetHealth(health);
    }
}
