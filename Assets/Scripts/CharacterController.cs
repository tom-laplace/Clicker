using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float damage;

    public float money;

    public GameManager gameManager;

    private TMPro.TextMeshProUGUI moneyText;
    

    //SerializeField MoneyAmount TextMesh object

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        damage = 10;
        money = 0;
        moneyText = GameObject.Find("MoneyAmount").GetComponent<TMPro.TextMeshProUGUI>();
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void upgradeAttack()
    {
        damage += 10;
    }

    public void EarnMoney()
    {
        if(money == 0){
            money = gameManager.level;
        }
        money += gameManager.level * 1.1f;
        moneyText.text = Math.Round(money).ToString();
    }
}
