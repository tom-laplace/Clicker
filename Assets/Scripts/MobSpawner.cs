using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject spawnedMonster;

    void Start()
    {
        spawnedMonster = GameObject.Find("Monster");
    }

    public void SpawnMonster(GameObject monster)
    {
        spawnedMonster = Instantiate(monster, new Vector3(8, -2.5f, 0), Quaternion.identity);
        spawnedMonster.AddComponent<MonsterController>();
    }
}
