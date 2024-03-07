using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int mobIndex = 0;
    BiomeController biomeController;
    MobSpawner mobSpawner;

    CharacterController characterController;
    public float level = 1f;
    // Start is called before the first frame update
    void Start()
    {
        mobSpawner = gameObject.GetComponent<MobSpawner>();
        biomeController = gameObject.GetComponent<BiomeController>();
        characterController = GameObject.Find("Character").GetComponent<CharacterController>();
        biomeController.LoadResourcesFromBiomeLevel(gameObject.GetComponent<BiomeController>().biomeLevel);
        mobSpawner.SpawnMonster(biomeController.monsters[mobIndex]);
        mobIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleMonsterSpawn(float monsterHealth)
    {
        if (monsterHealth <= 0)
        {
            level += 1;
            characterController.EarnMoney();
            // Index inférieur à 4 pour ne pas spawn le boss et éviter l'index out of range
            if (mobIndex < 4)
            {
                mobSpawner.SpawnMonster(biomeController.monsters[mobIndex]);
                mobIndex++;
            }
            else
            {   
                mobSpawner.SpawnMonster(biomeController.boss[0]);

                // On augmente le lvl du biome pour charger les ressources du prochain biome 
                // biomeController.biomeLevel++;
                // biomeController.LoadResourcesFromBiomeLevel(gameObject.GetComponent<BiomeController>().biomeLevel);

                // On repasse l'index a 0 pour recommencer le cycle de monstres 
                mobIndex = 0;
            }
        }
    }
    
}
