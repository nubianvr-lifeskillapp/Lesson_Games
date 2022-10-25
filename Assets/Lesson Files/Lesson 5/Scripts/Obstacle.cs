using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //Properties...
    [Header("Main Properties")]
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private TimeManager timeManager;
    [SerializeField]
    private L5_UIManager uIManager;
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private bool canMove = true;
   
    // Start is called before the first frame update
    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
       if(timeManager)
            print("Obstacle Found: Time Manager");
        uIManager = FindObjectOfType<L5_UIManager>();
        if(uIManager)
            print("Obstacle Found: UI Manager");
        player = FindObjectOfType<PlayerController>();
        if(player)
            print("Obstacle Found: Player Controller");
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            if(player.playerIsDead)
            {
                timeManager.bCanScaleUp = true;
            }
            else
            {
                player.AddDamage();
                timeManager.bCanScaleUp = true;
                L5_GameManager.gameManager.MoveToNextQuestion();
                uIManager.ShowQuestionUI(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            if (player.playerIsDead)
            {
                timeManager.bCanScaleUp = true;
            }
            else
            {
                player.AddDamage();
                print("Damage to player");
                timeManager.bCanScaleUp = true;
                //L5_GameManager.gameManager.MoveToNextQuestion();
                uIManager.ShowQuestionUI(false);
                Destroy(gameObject);
            }
        }
    }

    private void Move()
    {
        if(canMove)
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
