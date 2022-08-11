using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

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
       if(collision.CompareTag("Player"))
        {
            Debug.Log("Player Entered");
            spawner.isEnemyPresent = false;
            gameManager.score++;
            Debug.Log("Score Points: " + gameManager.score);
            Destroy(gameObject);
        }
    }
}
