using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private float speed = 2f;
    private Rigidbody2D rb;

    public GameObject GameTutScreen;
    private float horizontalInput;
    private float verticalInput;
    //private bool _showGameTut = true;

    //private bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
       // verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0.0f || verticalInput != 0.0f)
        {
            //_showGameTut = false;
            //GameTutScreen.SetActive(false);
            transform.Translate(new Vector3(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime,transform.position.z));
        }
    }

    public void ResetPlayerPosition()
    {
        gameObject.transform.position.Set(0,0,0);
    }

    public void AddDirectionForce(int horizontalValue,int verticalValue)
    {
        verticalInput = verticalValue;
        horizontalInput = horizontalValue;
    }
    

}
