using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

////////////////////////////////////////////////
//PlayerController class contains functions to 
//control the players movements.
//////////////////////////////////////////////

public class PlayerController : MonoBehaviour
{
    //Start Variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
   
    //////////////////////////////////////////////////////////
    //Animation enumerators, player will start in idle state.
    //Player will be able to run, jump, land and get hurt.
    //////////////////////////////////////////////////////////
    private enum State {idle, running, jumping, landing, hurt} 
    private State st = State.idle;
   
    ////////////////////////////////////////////////////////////////////////
    //Inspector Variables,
    //User can make various adjustments in Unity Inspector. Can make the game
    //easier or more difficult.
    ///////////////////////////////////////////////////////////////////////////
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f; //How fast the player will move
    [SerializeField] private float jumpForce = 10f; //How high the player will jump
    [SerializeField] private int diam = 0; //Number of diamonds player collected
    [SerializeField] private Text diaText; //Text for diamonds
    [SerializeField] private float hurtForce = 10f; //Force as to how far back the enemy pushes the player
    [SerializeField] private int health; //Number of health allowed before player starts back at beginning
    [SerializeField] private Text healthAmount; //Text for health


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthAmount.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(st != State.hurt)
        {
            Movement();
        }
        

        AnimationState();
        anim.SetInteger("st", (int)st); //sets animation based on enumerator state
    }

    //Player collects diamonds on collision
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Diamonds")
        {
            Destroy(collision.gameObject);
            diam += 1;
            diaText.text = diam.ToString();
        }
    }

    //Player colliding with Enemy
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" )
        {
            Evil evils = other.gameObject.GetComponent<Evil>();
            if (st == State.landing) //If player is in the landing position, enemy will be destroyed
            {
                evils.JumpedOn(); //Calling JumpedOn function
                Jump(); //Jumps off enemy as we destroy him 
            }
            else
            {
                //Player not in landing position, Player will be considered hurt and lose a health
                st = State.hurt;
                Health();
               
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy to right of player causes player being pushed to left with damages
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //Enemy to left of player causes player being pushed to right with damages
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    //UI health. Will reset level if health is less than or equal to 0
    void Health()
    {
        health -= 1;
        healthAmount.text = health.ToString();
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Restart of current scene
        }
    }

    //Player movements
    void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //Player moving right
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y); //speed of x and y values
            transform.localScale = new Vector2(-1, 1); //Player flips to left

        }

        //Player moving left
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); //speed of x and y values 
            transform.localScale = new Vector2(1, 1); //Player flips to right

        }

        // Ensures that player can only jump off of 'ground' layers and not infinitly jump while in the air
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();

        }
    }

    //Player Jumping
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        st = State.jumping;
    }

    void AnimationState()
    {
        if (st == State.jumping)
        {
            if (rb.velocity.y < .1f) //velocity tapers off as player is falling to ground
            {
                st = State.landing;
            }
        }

        //When character touches the ground, animation will go back to idle
        else if (st == State.landing)
        {
            if (coll.IsTouchingLayers(ground))
            {
                st = State.idle;
            }
        }

        else if (st == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                
                st = State.idle;

            }
        }

        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            //Moving
            st = State.running;

        }

        else
        {
            st = State.idle;
        }
    }
}
