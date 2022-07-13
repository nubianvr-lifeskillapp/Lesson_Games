using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private float speed = 10.0f;
    private Rigidbody2D rb;
    private bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //if (horizontalInput != 0.0f || verticalInput != 0.0f)
        //{
        //    //transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        //    transform.Translate(new Vector3(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime, 0.0f));
        //}
        if (horizontalInput != 0.0f || verticalInput != 0.0f)
        {
            //rb.simulated = true;
            rb.AddForce(new Vector2(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime));
            //rb.AddForce(new Vector3(horizontalInput * speed * Time.deltaTime, 0.0f, 0.0f));
            //rb.AddForce(new Vector3(0.0f, verticalInput * speed * Time.deltaTime, 0.0f));
        }
        //else
        //{
        //    rb.simulated = false;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        isColliding = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exit!");
        isColliding = false;
    }
}
