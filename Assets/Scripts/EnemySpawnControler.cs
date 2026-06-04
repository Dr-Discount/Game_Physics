using CGL.Actor;
using CGL.Inventory;
using CGL.Spawner;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawnControler : MonoBehaviour
{
    [SerializeField]
    EnemySpawner[] enemySpawners;
    private EnemySpawner lastSpawner;

    [SerializeField]
    GameObject[] objectsCanSpawn;

    [SerializeField]
    float SpawnTimer;

    [SerializeField]
    Player player;

    float timer = 0;

    private bool BossHasSpawned = false;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
            SpawnEnemy(WhatToSpawn());
    }

    public void SpawnEnemy(GameObject enemy)
    {
        EnemySpawner closest = null;
        float closestDistance = float.MaxValue;

        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner == lastSpawner)
                continue;

            float distance = Vector3.Distance(player.transform.position, spawner.transform.position);

            if (distance < 15f)
                continue;

            if (distance < closestDistance)
            {
                closest = spawner;
                closestDistance = distance;
            }
        }

        Instantiate(enemy, closest.transform.position, Quaternion.identity);
        lastSpawner = closest;
        timer = SpawnTimer;
    }

    private GameObject WhatToSpawn()
    {
        int randNum;
        randNum = Random.Range(0, 3);

        if (player.scoreData.value < 50) {} else if (player.scoreData.value < 120) {
            randNum = Random.Range(0, 2);
            switch (randNum)
            {
                case 0:
                    return objectsCanSpawn[0];
                case 1:
                    return objectsCanSpawn[1];
            }
        } else if (player.scoreData.value > 250 && !BossHasSpawned )
        {
            BossHasSpawned = true;
            return objectsCanSpawn[3];
        } else
        {
            switch (randNum)
            {
                case 0:
                    return objectsCanSpawn[0];
                case 1:
                    return objectsCanSpawn[1];
                case 2:
                    return objectsCanSpawn[2];
            }
        }
        return objectsCanSpawn[0];
    }
}
