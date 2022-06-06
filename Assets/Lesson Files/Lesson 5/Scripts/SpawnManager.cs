using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Properties...
    [Header("Main Properties")]
    [SerializeField]
    private GameObject obstacle;
    [HideInInspector]
    public GameObject existingObstacle;

    [SerializeField]
    private float yPosition = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //existingObstacle = FindObjectOfType<Obstacle>();
        existingObstacle = Instantiate(obstacle, new Vector3(25.0f, yPosition, 0.0f), new Quaternion());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObstacle();
    }

    public void SpawnObstacle()
    {
        if (!existingObstacle)
        {
            if (!L5_GameManager.gameManager.isLevelFinished)
            {
                existingObstacle = Instantiate(obstacle, new Vector3(25.0f, yPosition, 0.0f), new Quaternion());
            }
            return;
        }
            

        if (existingObstacle.transform.position.x <= -25.0f)
        {
            Debug.Log("Obstacle is at destroy point...");
            GameObject.Destroy(existingObstacle);
            if (!L5_GameManager.gameManager.isLevelFinished)
            {
                existingObstacle = Instantiate(obstacle, new Vector3(25.0f, yPosition, 0.0f), new Quaternion());
            }
        }
        ////////////
        //else if (existingObstacle == null)
        //{
        //    if (!L5_GameManager.gameManager.isLevelFinished)
        //    {
        //        existingObstacle = Instantiate(obstacle, new Vector3(25.0f, yPosition, 0.0f), new Quaternion());
        //    }
        //}

    }
}

