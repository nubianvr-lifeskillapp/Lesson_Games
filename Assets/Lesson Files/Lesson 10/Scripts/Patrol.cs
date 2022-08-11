using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime = 5.0f;
    public float startWaitTime;

    public List<Transform> moveSpots = new List<Transform>();
    private int nextSpotIndex;
    private bool swapDirection = false;

    
    // Start is called before the first frame update
    void Start()
    {
        //randomSpot = Random.Range(0, moveSpots.Count);
        nextSpotIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpotIndex].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, moveSpots[nextSpotIndex].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                if(!swapDirection) //If swap is false...
                {
                    if (nextSpotIndex == moveSpots.Count-1)
                        swapDirection = true;
                    else
                        nextSpotIndex++;
                }
                else //If swap is true...
                {
                    if (nextSpotIndex <= 0)
                        swapDirection = false;
                    else
                        nextSpotIndex--;
                }
                Debug.Log("Next Spot Index: " + nextSpotIndex);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
