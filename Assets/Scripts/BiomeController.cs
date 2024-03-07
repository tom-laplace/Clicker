using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeController : MonoBehaviour
{
    public GameObject[] monsters;
    public GameObject[] boss;
    public int biomeLevel = 0;

    public void LoadResourcesFromBiomeLevel(int biomeLevel)
    {
        monsters = null;
        boss = null;
        switch (biomeLevel)
        {
            case 0:
                monsters = Resources.LoadAll<GameObject>("Prefabs/GothicLand/Monsters");
                boss = Resources.LoadAll<GameObject>("Prefabs/GothicLand/Boss");
                break;
            case 1: 
                monsters = Resources.LoadAll<GameObject>("Prefabs/GolemLand/Monsters");
                boss = Resources.LoadAll<GameObject>("Prefabs/GolemLand/Boss");
                break;
            default:
                break;
        }
    }
}
