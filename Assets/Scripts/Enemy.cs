using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////////////
//Enemy class is the derived class of Evil class.
//This class will inherit and override functions.
//of the parent class: Evil.
/////////////////////////////////////////////////

public class Enemy : Evil
{
    ///////////////////////////////////////////////////////
    //leftCap and rightCap are objects used to see if enemy
    //is at a certain position in the game. User can adjust
    //the position of the caps in Unity Inspector.
    ////////////////////////////////////////////////////////
    [SerializeField] private float leftCap =0;
    [SerializeField] private float rightCap =0;

    ///////////////////////////////////////////////////////
    //jumpLength is for how far the enemy can jump.
    //jumpHeight is for high the enemy can jump. User can
    //make adjustments as needed in Unity Inspector.
    ////////////////////////////////////////////////////////
    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;


    private Collider2D coll;
    private Rigidbody2D rb;
   
    //Is the enemy facing the right?True or False//
    private bool facingRight = true;

    /////////////////////////////////////////////////
    //Overriding the start function from Evil Class.
    /////////////////////////////////////////////////
    protected override void Start()
    {
        base.Start(); //Access start from Evil class
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
       
    }

   
    void Update()
    {
        //Enemy movements transitioning from a jump position to a fall position
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        //Enemy movements transitioning from a fall position to a walk position
        if(coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    /////////////////////////////////////////////////////
    //Movements for the enemy. Enemies are able to jump
    //and fall while moving towards the player.
    /////////////////////////////////////////////////////

    void Move()
    {
        if (facingRight)
        {
            //Test to see if enemy beyond rightCap
            if (transform.position.x < rightCap)
            {
                //Make sure sprite is facing right direction, if not, then face right direction
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                //Test to see if player is on ground
                if (coll.IsTouchingLayers(ground))
                {
                    //Jump
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingRight = false;
            }
        }

        //Testing to see if enemy is facing left direction
        else
        {
            
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                //Test to see if player is on ground, if so jump
                if (coll.IsTouchingLayers(ground))
                {
                    //Jump
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }

            else
            {
                facingRight = true;
            }
        }
    }

   
}
