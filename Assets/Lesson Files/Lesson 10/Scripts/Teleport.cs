using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Properties...
    [SerializeField]
    private Transform teleportDestination;
    [SerializeField]
    [Tooltip("What sign is associated with the position of this gameobject(Represent with -1 or 1)")]
    private float positionSign;
    private Transform player;
    private bool isOverllaped = false;
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // For positive postions...
        if (isOverllaped && positionSign > 0 && player.transform.position.x > transform.position.x)
        {
            player.position = teleportDestination.position;
            isOverllaped = false;
        }
        // For negative postions...
        else if (isOverllaped && positionSign < 0 && player.transform.position.x < transform.position.x)
        {
            player.position = teleportDestination.position;
            isOverllaped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOverllaped = true;
        }
    }
}
