using System;
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
    private bool isGrounded = false;
    [SerializeField]
    private int playerLife = 3;
    public bool playerIsDead = false;
    [SerializeField]
    private SpriteRenderer bag;

    [SerializeField] private float characterRunSpeed;
    public bool isRunning;
    private Vector3 currentTempPosition;

    public Animator PlayerAnimator;
    private float YAxisJumpCheck;
    private bool AllowJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        YAxisJumpCheck = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        currentTempPosition  = transform.position;
        
        AllowJump = transform.position.y > YAxisJumpCheck;
        
        if (isGrounded)
        {
            PlayerAnimator.CrossFade(isRunning ? "RunningAnimation" : "IdleAnimation", 0, 0);
        }
        else
        {
            PlayerAnimator.CrossFade(rb2D.velocity.y < 0 ? "FallAnimation" : "JumpAnimation", 0, 0);
        }
    }

    private void FixedUpdate()
    {
        
        if (isRunning)
        {
            transform.Translate(Vector3.right * (characterRunSpeed * Time.deltaTime));
        }
        else
        {
            transform.position = currentTempPosition;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Floor")) return;
        if(AllowJump)
            isGrounded = false;
    }
    

    public void Jump()
    {
        if (!isGrounded) return;
        if(rb)
            rb.AddForce(Vector3.up * jumpForce);
        if (rb2D)
        {
            rb2D.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
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
