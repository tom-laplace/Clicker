using System;
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

    private TMPro.TextMeshProUGUI attackText;

    private TMPro.TextMeshProUGUI dotText;

    private TMPro.TextMeshProUGUI levelText;
    

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
        attackText = GameObject.Find("AttackText").GetComponent<TMPro.TextMeshProUGUI>();
        dotText = GameObject.Find("DotText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void HandleMonsterSpawn()
    {
        level += 1;
        levelText.text = "Level " + level;
        characterController.EarnMoney();
        // Index inférieur à 4 pour ne pas spawn le boss et éviter l'index out of range
        Debug.Log(mobIndex);
        Debug.Log(biomeController.biomeLevel);
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
            biomeController.LoadResourcesFromBiomeLevel(biomeController.biomeLevel);

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
            upgradeAttackCostText.text = "Upgrade Attack (" + Math.Round(upgradeAttackCost) + ")";
            attackText.text = "Attack: " + Math.Round(characterController.damage);
        }
    }

    public void UpgradeCharacterPassiveDamage()
    {
        if (characterController.money >= upgradePassiveDamageCost)
        {
            characterController.LoseMoney(upgradePassiveDamageCost);
            characterController.upgradePassiveDamage();
            upgradePassiveDamageCost *= 1.5f;
            upgradePassiveDamageCostText.text = "Upgrade DOT (" + Math.Round(upgradePassiveDamageCost) + ")";
            dotText.text = "DOT: " + Math.Round(characterController.passiveDamage);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("level", level);
        PlayerPrefs.SetFloat("upgradeAttackCost", upgradeAttackCost);
        PlayerPrefs.SetFloat("upgradePassiveDamageCost", upgradePassiveDamageCost);
        PlayerPrefs.SetFloat("money", characterController.money);
        PlayerPrefs.SetFloat("damage", characterController.damage);
        PlayerPrefs.SetFloat("passiveDamage", characterController.passiveDamage);
        PlayerPrefs.SetFloat("biomeLevel", biomeController.biomeLevel);
        PlayerPrefs.SetFloat("mobIndex", mobIndex);
    }

    public void Load()
    {
        level = PlayerPrefs.GetFloat("level") -1;
        upgradeAttackCost = PlayerPrefs.GetFloat("upgradeAttackCost");
        upgradePassiveDamageCost = PlayerPrefs.GetFloat("upgradePassiveDamageCost");
        characterController.money = PlayerPrefs.GetFloat("money");
        characterController.damage = PlayerPrefs.GetFloat("damage");
        characterController.passiveDamage = PlayerPrefs.GetFloat("passiveDamage");
        biomeController.biomeLevel = (int)PlayerPrefs.GetFloat("biomeLevel");
        mobIndex = PlayerPrefs.GetInt("mobIndex");
        biomeController.LoadResourcesFromBiomeLevel(biomeController.biomeLevel);
        Destroy(GameObject.FindWithTag("Monster"));
        upgradePassiveDamageCostText.text = "Upgrade DOT (" + Math.Round(upgradePassiveDamageCost) + ")";
        upgradeAttackCostText.text = "Upgrade Attack (" + Math.Round(upgradeAttackCost) + ")";
        attackText.text = "Attack: " + Math.Round(characterController.damage);
        dotText.text = "DOT: " + Math.Round(characterController.passiveDamage);
        HandleMonsterSpawn();
    }

}
