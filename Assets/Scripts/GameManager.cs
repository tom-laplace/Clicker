using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{

    int mobIndex = 0;
    BiomeController biomeController;
    MobSpawner mobSpawner;

    CharacterController characterController;
    public float level = 1f;

    private float upgradeAttackCost = 20f;

    private float upgradePassiveDamageCost = 20f;

    private TMPro.TextMeshProUGUI upgradeAttackCostText;

    private TMPro.TextMeshProUGUI upgradePassiveDamageCostText;

    private TMPro.TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        mobSpawner = gameObject.GetComponent<MobSpawner>();
        biomeController = gameObject.GetComponent<BiomeController>();
        characterController = GameObject.Find("Character").GetComponent<CharacterController>();
        biomeController.LoadResourcesFromBiomeLevel(gameObject.GetComponent<BiomeController>().biomeLevel);
        mobSpawner.SpawnMonster(biomeController.monsters[mobIndex]);
        mobIndex++;
        upgradeAttackCostText = GameObject.Find("UpgradeText").GetComponent<TMPro.TextMeshProUGUI>();
        upgradePassiveDamageCostText = GameObject.Find("UpgradePassiveText").GetComponent<TMPro.TextMeshProUGUI>();
        levelText = GameObject.Find("LevelText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleMonsterSpawn()
    {
        level += 1;
        levelText.text = "Level " + level;
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
            biomeController.biomeLevel++;
            biomeController.LoadResourcesFromBiomeLevel(gameObject.GetComponent<BiomeController>().biomeLevel);

            // On repasse l'index a 0 pour recommencer le cycle de monstres 
            mobIndex = 0;
        }
    }

    public void UpradeCharacterAttack()
    {
        if (characterController.money >= upgradeAttackCost)
        {
            characterController.LoseMoney(upgradeAttackCost);
            characterController.upgradeAttack();
            upgradeAttackCost *= 1.5f;
            upgradeAttackCostText.text = "Upgrade Attack: " + upgradeAttackCost;
        }
    }

    public void UpgradeCharacterPassiveDamage()
    {
        if (characterController.money >= upgradePassiveDamageCost)
        {
            characterController.LoseMoney(upgradePassiveDamageCost);
            characterController.upgradePassiveDamage();
            upgradePassiveDamageCost *= 1.5f;
            upgradePassiveDamageCostText.text = "Upgrade Passive Damage: " + upgradePassiveDamageCost;
        }
    }

}
