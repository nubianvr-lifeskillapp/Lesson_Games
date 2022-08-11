using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private float speed = 10.0f;
    private Rigidbody2D rb;
    //private bool isColliding = false;
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
        if (horizontalInput != 0.0f || verticalInput != 0.0f)
        {
            rb.AddForce(new Vector2(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime));
        }
    }
}
