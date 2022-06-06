using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Properties...
    private Rigidbody rb;
    private Rigidbody2D rb2D;
    [SerializeField]
    private float jumpForce = 300.0f;
    [SerializeField]
    private bool isGrounded = true;
    [SerializeField]
    private int playerLife = 3;
    public bool playerIsDead = false;
    [SerializeField]
    private SpriteRenderer bag;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rb)
                rb.AddForce(Vector3.up * jumpForce);
            if (rb2D)
                rb2D.AddForce(Vector3.up * jumpForce);
        }
    }

    //For 3D...
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            isGrounded = false;
    }

    //For 2D...
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            isGrounded = false;
    }

    public void Jump()
    {
        if(isGrounded)
        {
            if(rb)
                rb.AddForce(Vector3.up * jumpForce);
            if (rb2D)
                rb2D.AddForce(Vector3.up * jumpForce);
        }
    }

    public void AddDamage()
    {
        playerLife--;
        if (playerLife<=0)
        {
            playerLife = 0;
            playerIsDead = true;
            L5_GameManager.gameManager.OnLevelFinshed();
        }
        else if(playerLife == 1)
        {
            bag.color = Color.red;
        }
        else if (playerLife == 2)
        {
            bag.color = Color.yellow;
        }
    }
}
