using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [HideInInspector]
    public Vector3 SpawnPos;
    [HideInInspector]
    public Vector3 EndPos;
    //private Vector3 currentDestination;
    [SerializeField]
    private Transform waypoint;
    //private Vector3 defaultPosition;
    public bool canLerp = false;
    public float movementSpeed = 1f;
   
    // Start is called before the first frame update
    void Start()
    {
        //defaultPosition = gameObject.transform.position;
        //Debug.Log(defaultPosition);
        agent = GetComponent<NavMeshAgent>();
       
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //MoveToLocation(EndPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.Equals(EndPos))
        {
            Debug.Log("At end pos");
            MoveToLocation(SpawnPos);
        }
        else if(transform.position.Equals(SpawnPos))
        {
            MoveToLocation(EndPos);
            Debug.Log("At spawn pos");
        }
        if (canLerp)
        {
            MoveToLocation(EndPos);
        }
    }

    public void MoveToLocation(Vector3 destination)
    {
        //currentDestination = destination;
        //agent.SetDestination(destination);
        transform.position = Vector3.Lerp(transform.position, destination, 0.5f * Time.deltaTime);
        //transform.DOMove(destination, 3.0f);
    }
}
