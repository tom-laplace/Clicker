using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int index = 0;
    BiomeController biomeController;
    MobSpawner mobSpawner;

    void Start()
    {
        mobSpawner = gameObject.GetComponent<MobSpawner>();
        biomeController = gameObject.GetComponent<BiomeController>();
        biomeController.LoadResourcesFromBiomeLevel(gameObject.GetComponent<BiomeController>().biomeLevel);
    }


    void Update()
    {
        
    }

    public void HandleMonsterSpawn(float monsterHealth)
    {
        if (monsterHealth <= 0)
        {
            // Index inférieur à 4 pour ne pas spawn le boss et éviter l'index out of range
            if (index < 4)
            {
                mobSpawner.SpawnMonster(biomeController.monsters[index]);
                index++;
            }
            else
            {
                mobSpawner.SpawnMonster(biomeController.boss[0]);

                // On augmente le lvl du biome pour charger les ressources du prochain biome 
                // biomeController.biomeLevel++;
                // biomeController.LoadResourcesFromBiomeLevel(gameObject.GetComponent<BiomeController>().biomeLevel);

                // On repasse l'index a 0 pour recommencer le cycle de monstres 
                index = 0;
            }
        }
    }
}
