using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Properties...
    [SerializeField]
    private float speed = 10.0f;
    private Rigidbody2D rb;

    public GameObject GameTutScreen;

    private bool _showGameTut = true;

    //private bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameTutScreen.SetActive(_showGameTut);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0.0f || verticalInput != 0.0f)
        {
            _showGameTut = false;
            GameTutScreen.SetActive(false);
            rb.AddForce(new Vector2(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime));
        }
    }

    public void ResetPlayerPosition()
    {
        gameObject.transform.position.Set(0,0,0);
    }

    
}
