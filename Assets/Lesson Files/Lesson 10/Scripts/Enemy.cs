using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private L10_GameManager gameManager;
    private Spawner spawner;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<L10_GameManager>();
        spawner = GameObject.FindObjectOfType<Spawner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Entered");
            Destroy(gameObject);
            gameManager.AddToScore(10);
        }
    }
}
