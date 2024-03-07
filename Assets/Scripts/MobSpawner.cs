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
        if (spawnedMonster != null)
        {
            Destroy(spawnedMonster);
        }

        spawnedMonster = Instantiate(monster, new Vector3(6, -1, 0), Quaternion.identity);
        spawnedMonster.AddComponent<MonsterController>();
    }
}
