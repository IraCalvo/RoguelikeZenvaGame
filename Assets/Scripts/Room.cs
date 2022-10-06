using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Door Objects")]
    public Transform northDoor;
    public Transform southDoor;
    public Transform eastDoor;
    public Transform westDoor;

    [Header("Wall Objects")]
    public Transform northWall;
    public Transform southWall;
    public Transform eastWall;
    public Transform westWall;

    [Header("Values")]
    public int insideWidth;
    public int insideHeight;

    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public GameObject healthPrefab;
    public GameObject keyPrefab;
    public GameObject exitDoorPrefab;

    private List<Vector3> usedPositions = new List<Vector3>();


    public void GenerateInterior()
    {
        //spawn enemy checker
        if(Random.value < Generation.instance.enemySpawnRate)
        {
            SpawnPrefab(enemyPrefab, 1 , Generation.instance.maxEnemiesPerRoom + 1);
        }
        
        //spawn coin checker
        if(Random.value < Generation.instance.coinSpawnRate)
        {
            SpawnPrefab(coinPrefab, 1,  Generation.instance.maxCoinsPerRoom + 1);
        }

        //spawn health checker
        if(Random.value < Generation.instance.healthSpawnRate)
        {
            SpawnPrefab(healthPrefab, 1 , Generation.instance.maxHealthPerRoom + 1);
        }
    }

    public void SpawnPrefab(GameObject prefab, int min = 0, int max = 0)
    {
        //amount of prefabs to instantiate
        int num = 1;

        if(min != 0 || max != 0)
        {
            num = Random.Range(min, max);
        }

        for(int x = 0; x < num; ++x)
        {
            GameObject obj = Instantiate(prefab);
            //gets a random position in the room
            Vector3 pos = transform.position + new Vector3(Random.Range(-insideWidth/2, insideWidth/2 + 1), Random.Range(-insideHeight/2, insideHeight/2 + 1), 0);

            //this checks if the position is already being used, and if it is, itll try to spawn it else where
            while(usedPositions.Contains(pos))
            {
                pos = transform.position + new Vector3(Random.Range(-insideWidth/2, insideWidth/2 + 1), Random.Range(-insideHeight/2, insideHeight/2 + 1), 0);
            }

            obj.transform.position = pos;
            usedPositions.Add(pos);

            if(prefab == enemyPrefab)
            {
                EnemyManager.instance.enemies.Add(obj.GetComponent<Enemy>());
            }

        }
    }
}
