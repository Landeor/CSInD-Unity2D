using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int beforeIndex;
    public int afterIndex;

    public GameObject[] Maps;
    public GameObject player;

    public GameObject[] enemyobjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    void Awake()
    {
        beforeIndex = 0;
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(3f, 5f);
            curSpawnDelay = 0;
        }
    }
    void SpawnEnemy()
    {
        int ranEnemy = 0;
        int ranPoint = Random.Range(0, 3);
        Instantiate(enemyobjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation); 

    }
    public void MapTeleport()
    {
        Maps[beforeIndex].SetActive(false);
        switch (afterIndex)
        {
            case 0:
                //스폰
                beforeIndex = afterIndex;
                break; 

            case 1:
                //사냥터 1
                beforeIndex = afterIndex;
                break; 
        }

        Maps[afterIndex].SetActive(true);
    }
}
