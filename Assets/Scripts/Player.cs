using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    private float speed = 4f;
    [SerializeField]
    private float jumpForce = 0.7f;
    int playerLayer, groundLayer;
  
    int i = -1;
    private bool isGrounded = true;
    private string groundTAG = "Ground";
    private bool DoubleJump = true;
    private bool JumpOnPl = false;
    
 


    private float movementX;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;

    private void Update()
    {
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        PlayerJump();
        AnimatePlayer();
        PlayerMovement();
     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        speed = speed * i;

    }
    void PlayerMovement()
    {
        /* movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;
        */
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
        movementX = speed;
    }

    void AnimatePlayer()
    {
        if (movementX < 0)
        {
            sr.flipX = true;
        } else if (movementX > 0)
        {
            sr.flipX = false;
        }

    }
     

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("player");
        groundLayer = LayerMask.NameToLayer("ground");
    }

  private void PlayerJump()
    {

        if (Input.GetButtonDown("Jump") && isGrounded)
          {

              myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
              isGrounded = false;
              DoubleJump = true;
        

          }
          else if (Input.GetButtonDown("Jump") && !isGrounded && DoubleJump)
          {
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

              DoubleJump = false;
        
              ScoreScript.scoreValue++;
              StartCoroutine("JumpOn");

          }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTAG))
        {
            isGrounded = true;
        } 
      

    }
    IEnumerator JumpOn()
    {
        JumpOnPl = true;
        Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
        JumpOnPl = false;
    }
}