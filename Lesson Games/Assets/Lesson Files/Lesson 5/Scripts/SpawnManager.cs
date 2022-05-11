using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Properties...
    [Header("Main Properties")]
    [SerializeField]
    private GameObject obstacle;
    private GameObject existingObstacle;

    [SerializeField]
    private float yPosition = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //existingObstacle = FindObjectOfType<Obstacle>();
        existingObstacle = Instantiate(obstacle, new Vector3(25.0f, yPosition, 0.0f), new Quaternion());
        //TriggerPoint triggerPoint = existingObstacle.GetComponentInChildren<TriggerPoint>();
        //triggerPoint.timeManager = FindObjectOfType<TimeManager>();
        //triggerPoint.uIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!existingObstacle)
            return;

        if (existingObstacle.transform.position.x <= -25.0f)
        {
            Debug.Log("Obstacle is at destroy point...");
            GameObject.Destroy(existingObstacle);
            if(!GameManager.gameManager.isLevelFinished)
            {
                existingObstacle = Instantiate(obstacle, new Vector3(25.0f, yPosition, 0.0f), new Quaternion());
                //TriggerPoint triggerPoint = existingObstacle.GetComponentInChildren<TriggerPoint>();
                //triggerPoint.timeManager = FindObjectOfType<TimeManager>();
                //triggerPoint.uIManager = FindObjectOfType<UIManager>();
            }
               
        }
    }
}
