using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class handling detection when the obstacle is certain distance from the player...
 * Oncollision with the player it triggers the questionBox to pop up and then the player would have to make a choice/ select an answer...
 * Depending on the players answer a certain action is performed...
 */
public class TriggerPoint : MonoBehaviour
{
    //Properties
    [Header("Main Properties")]
    //Reference to the time manager in the scene...
    public TimeManager timeManager;
    //Reference to the user interface manager in the scene...
    public L5_UIManager uIManager;


    // Start is called before the first frame update
    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        if (timeManager)
            print("Trigger Found: Time Manager");
        uIManager = FindObjectOfType<L5_UIManager>();
        if (uIManager)
            print("Trigger Found: UI Manager");

    }

    //Making changes to the parameter here....
    //Collider to Collider2D...
    private void OnTriggerEnter(Collider other)
    {
        //On collision with object of tag "Player"...
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            //If time manager is valid and exists...
            if (timeManager)
            {
                Debug.Log("Triggered Slowmotion...");
                //Time can slow down...
                timeManager.bCanSlowdown = true;
            }
            //if the user interface manager is valid and exists...
            if(uIManager)
            {
                //Shwo the question box(UI)...
                uIManager.ShowQuestionUI(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //On collision with object of tag "Player"...
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            //If time manager is valid and exists...
            if (timeManager)
            {
                Debug.Log("Triggered Slowmotion...");
                //Time can slow down...
                timeManager.bCanSlowdown = true;
            }
            //if the user interface manager is valid and exists...
            if (uIManager)
            {
                //Shwo the question box(UI)...
                uIManager.ShowQuestionUI(true);
            }
        }
    }
}
