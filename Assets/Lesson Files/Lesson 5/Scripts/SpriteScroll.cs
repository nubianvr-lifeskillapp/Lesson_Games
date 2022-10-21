using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroll : MonoBehaviour
{

    public float scrollSpeed;

    public float startPos;

    public float lengthOfSprite;

    public float temp;
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position.x;
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        temp = gameObject.transform.position.x;
        
        
        
        if (temp < (startPos - lengthOfSprite))
        {
            startPos -= lengthOfSprite;
            gameObject.transform.SetPositionAndRotation(new Vector2(Mathf.Abs(startPos),transform.position.y), new Quaternion() );
        }
        
        moveSprite();


    }

    private void moveSprite()
    {
        gameObject.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
    }
}
