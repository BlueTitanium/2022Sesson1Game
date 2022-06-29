using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;
    public float gravityModifier;
    private Rigidbody playerRb;

    public float health;

    //Grounded Vars
    bool grounded = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier; // shorthand notation for the line below
    }

    void Update()
    {
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
                //grounded = false;
            }
            
        }

        moveVelocity = Input.GetAxis("Horizontal") * speed;
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Attack")){
            Debug.Log("Hit!");
        }
    }

}