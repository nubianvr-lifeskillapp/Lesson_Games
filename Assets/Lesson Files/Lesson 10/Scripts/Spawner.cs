using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private Transform[] spawnPositions;
    private int spawnIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnPositions.Length > 0)
        {
            Enemy reference = Instantiate<Enemy>(enemy, spawnPositions[spawnIndex].GetChild(0).position, Quaternion.identity);
            if (reference)
                Debug.Log("EnemySpawned");
            reference.SpawnPos = spawnPositions[spawnIndex].GetChild(0).position;
            //Debug.Log(reference.SpawnPos);
            reference.EndPos = spawnPositions[spawnIndex].GetChild(1).position;
            //Debug.Log(reference.EndPos);
            reference.canLerp = true;
            //reference.MoveToLocation(reference.EndPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
