using System.Collections;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    Spawner[] spawners;

    float speedUp = 0;
    float waitTime = 3;

    private void Start()
    {
        SpawnWall();
    }

    private void Update()
    {
        waitTime -= 1 * Time.deltaTime;
        if (waitTime < 0)
        {
            SpawnWall();
            if (speedUp < 1)
                speedUp += 0.07f;
            waitTime = 3 - speedUp;
        }
    }

    void SpawnWall()
    {
        int num = Random.Range(0, spawners.Length);
        spawners[num].spawnObject(num);
    }
}
