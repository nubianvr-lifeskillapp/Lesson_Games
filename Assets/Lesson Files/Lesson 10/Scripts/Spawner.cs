using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private Patrol enemy;
    [SerializeField]
    private Transform[] spawnPositions;
    private int spawnIndex = 0;
    public float spawnWaitTime = 0;
    [HideInInspector]
    // public bool isEnemyPresent = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnWaitTime = 0f;
        SpawnEnemies();
        // isEnemyPresent = true;
        spawnWaitTime = 2f;
    }

    private void SpawnEnemies()
    {
        for (spawnIndex = 0; spawnIndex < spawnPositions.Length; spawnIndex++)
        {
            Patrol enemyRef = Instantiate(enemy, spawnPositions[spawnIndex].GetChild(0).position, Quaternion.identity);
            for (int i = 0; i < spawnPositions[spawnIndex].childCount; i++)
            {
                enemyRef.moveSpots.Add(spawnPositions[spawnIndex].GetChild(i).GetComponent<Transform>());
            }
            enemyRef.speed = Random.Range(2, 4);
            enemyRef.startWaitTime = Random.Range(1, 3);
            //Debug.Log("SpawnIndex: " + spawnIndex);
        }
    }



    #region Unused Code
    // Update is called once per frame
    // void Update()
    // {
    //     if (!isEnemyPresent)
    //     {
    //         SpawnEnemy();
    //         isEnemyPresent = true;
    //     }
    // }

    // public void CallSpawnEnemy()
    // {
    //     StartCoroutine(nameof(SpawnEnemy));
    // }
    // // private IEnumerator SpawnEnemy()
    // {
    //     yield return new WaitForSeconds(spawnWaitTime);
    //     if (spawnIndex < spawnPositions.Length)
    //     {
    //         Patrol enemyRef = Instantiate(enemy, spawnPositions[spawnIndex].GetChild(0).position, Quaternion.identity);
    //         for (int i = 0; i < spawnPositions[spawnIndex].childCount; i++)
    //         {
    //             enemyRef.moveSpots.Add(spawnPositions[spawnIndex].GetChild(i).GetComponent<Transform>());
    //         }
    //         spawnIndex++;
    //         Debug.Log("SpawnIndex: " + spawnIndex);
    //     }
    //     else
    //     {
    //         spawnIndex = 0;
    //         Patrol enemyRef = Instantiate(enemy, spawnPositions[spawnIndex].GetChild(0).position, Quaternion.identity);
    //         for (int i = 0; i < spawnPositions[spawnIndex].childCount; i++)
    //         {
    //             enemyRef.moveSpots.Add(spawnPositions[spawnIndex].GetChild(i).GetComponent<Transform>());
    //         }
    //         spawnIndex++;
    //         Debug.Log("SpawnIndex: " + spawnIndex);
    //     }
    // }
    #endregion
}