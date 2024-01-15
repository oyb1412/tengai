using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] RightSpawnPoint;
    public int SpawnCount;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int[] ranprefabs = new int[4]; 
        while (true)
        {
            ranprefabs[0] = Random.Range(0, enemyPrefabs.Length-1); ;
            ranprefabs[1] = Random.Range(0, enemyPrefabs.Length - 1); ;
            ranprefabs[2] = Random.Range(0, enemyPrefabs.Length - 1); ;
            ranprefabs[3] = Random.Range(0, enemyPrefabs.Length - 1); ;
            if (ranprefabs[0] != ranprefabs[1] && ranprefabs[0] != ranprefabs[2] && ranprefabs[0] != ranprefabs[3] && ranprefabs[1] != ranprefabs[2] &&
                ranprefabs[1] != ranprefabs[3] && ranprefabs[2] != ranprefabs[3])
                break;
        }
        

        if(time > 5f && SpawnCount <= 10)
        {
            for(int i = 0;i< 4; i++)
            {
                FactoryManager.instance.CreateEnemy(enemyPrefabs[ranprefabs[i]], RightSpawnPoint[i].transform.position);
            }
            SpawnCount++;
            time = 0;
        }
        if(SpawnCount == 11)
        {
            FactoryManager.instance.CreateEnemy(enemyPrefabs[6], new Vector3(11f,4f,0f));
            SpawnCount++;
        }

    }
}
