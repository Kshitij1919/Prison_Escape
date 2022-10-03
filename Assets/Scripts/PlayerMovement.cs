using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float climbingSpeed = 6f;
    [SerializeField] private Vector2 DeathKick = new Vector2(10f,10f);
    //[SerializeField] private BullletPool myBullletPool;
    [SerializeField] private GameObject bullet;
    private Vector2 moveInput;
    private Rigidbody2D myRigidbody;
    private bool playerhashorizontalSpeed;
    private bool isAlive = true;
    private Animator myAnimator;
    private Collider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    private float myGravityScaleatStart;
    private Transform myGun;
    
    

    void Start()
    {
        
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myGravityScaleatStart = myRigidbody.gravityScale;
        myGun = transform.Find("Gun");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            
            return;
        }
        Run();
        Climb();
        FlipSprite();
        Die();
       
        //Debug.Log(playerhashorizontalSpeed);
    }

    


    //callback Input Function for Player Movement
    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        
    }


    //callbakc Ju,p function for Player Jump
    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        if (value.isPressed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.velocity += new Vector2(0f,jumpSpeed);
        }
    }

    //callback fire function for player shoot
    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
            Instantiate(bullet, transform.Find("Gun").transform.position, transform.rotation);

        //myBullletPool.spawn(myGun);
        
        
    }


    //Run Mechanics for player
    void Run()
    { 
        Vector2 PlayerVelocity = new Vector2 (moveInput.x * movementSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = PlayerVelocity;
        myAnimator.SetBool("isRunning", playerhashorizontalSpeed);
    }


    //climb mechanics for player
    void Climb()
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidbody.gravityScale = 0f;
            Vector2 ClimbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbingSpeed);
            myRigidbody.velocity = ClimbVelocity;

            //checking for vertical velocity for animation
            bool playerhasVerticalVelocity = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
             myAnimator.SetBool("isClimbing", playerhasVerticalVelocity);   
        }
        else
        {
            myRigidbody.gravityScale = myGravityScaleatStart;
            myAnimator.SetBool("isClimbing", false);
        }
    }


    //players flips based on player's horizontal input(left or right)
    void FlipSprite()
    {
         playerhashorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerhashorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }


    //player death
    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dieing");
            //myRigidbody.AddForce(new Vector2(0f, jumpSpeed),ForceMode2D.Impulse);
            myRigidbody.velocity = DeathKick;
            FindObjectOfType<GameSession>().processDeath();
            

        }
    }

}



