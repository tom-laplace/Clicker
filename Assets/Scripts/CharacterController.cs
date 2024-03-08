using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float damage;

    public float money;

    public float passiveDamage;

    public GameManager gameManager;

    private TMPro.TextMeshProUGUI moneyText;
    

    //SerializeField MoneyAmount TextMesh object

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        damage = 10;
        passiveDamage = 0;
        money = 0;
        moneyText = GameObject.Find("MoneyAmount").GetComponent<TMPro.TextMeshProUGUI>();
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgradeAttack()
    {
        damage *= 1.2f;
    }   

    public void upgradePassiveDamage()
    {
        passiveDamage *= 1.2f;
        if(passiveDamage == 0){
            passiveDamage = 10;
        }
    }

    public void EarnMoney()
    {
        if(money == 0){
            money = gameManager.level;
        }
        money += gameManager.level * 1.2f;
        moneyText.text = Math.Round(money).ToString();
    }

    public void LoseMoney(float amount)
    {
        money -= amount;
        moneyText.text = Math.Round(money).ToString();
    }
}
