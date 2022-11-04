using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
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

    public GameObject Player;
    
    public GameObject levelManager;
    private L5_GameManager levelManagerScript;

    private void Awake()
    {
        levelManagerScript = levelManager.GetComponent<L5_GameManager>();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        //existingObstacle = FindObjectOfType<Obstacle>();
        existingObstacle = Instantiate(obstacle, new Vector3(Player.transform.position.x + 55.0f, yPosition, 0.0f), new Quaternion());
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
            if (!levelManagerScript.isLevelFinished)
            {
                existingObstacle = Instantiate(obstacle, new Vector3(Player.transform.position.x + 55.0f, yPosition, 0.0f), new Quaternion());
            }
            return;
        }

        if (levelManagerScript.questionAnswered)
        {
            existingObstacle.GetComponent<BoxCollider2D>().enabled = false;
        }
        // else
        // {
        //     existingObstacle.GetComponent<BoxCollider2D>().enabled = true;
        // }

        if (Player.transform.position.x > existingObstacle.transform.position.x + 50.0f)
        {
            Debug.Log("Obstacle is at destroy point...");
            Destroy(existingObstacle);
            if (!levelManagerScript.isLevelFinished)
            {
                existingObstacle = Instantiate(obstacle, new Vector3(Player.transform.position.x + 55.0f, yPosition, 0.0f), new Quaternion());
            }
        }


    }
}

