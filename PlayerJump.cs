using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerJump : MonoBehaviour
{
    //criar forca, aplicar forca, 1x
    [Header("JumpVars")]
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;

    [Header("GroudDetails")]
    [SerializeField] private Transform groundCheck; //transform will verify the position of the object
    [SerializeField] private float radOCircle;  //variavel para criar a radial do gizmos
    [SerializeField] private LayerMask whatIsGround; // gonna define what groud is based on a layer mask
    public bool grounded; //check if tis touching the floor

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator myAnimator;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
    }

    //myAnimator.SetBool("fall", true);
    //myAnimator.SetBool("fall", false);

    //myAnimator.SetTrigger("jump");
    //myAnimator.ResetTrigger("jump");
    private void Update() 
    {
        //what is to be grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);

        if(grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("fall", false);
        }

        //if we press the jumop button (wich is set to space in unity, can be changed later)
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);    //jump
            stoppedJumping = false;
            //telling the animator to play the jump anim
            myAnimator.SetTrigger("jump");
        }

        //if we hold the jumo button
        if(Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
            myAnimator.SetTrigger("jump");
        }

        //if we release the jump button
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true; // cant hold the button and cant keep going up
            myAnimator.SetBool("fall", true);
            myAnimator.ResetTrigger("jump");
        }

        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("fall", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOCircle); //gizmos para reconhecer quando estiver tocando o chao (ground)
    }

    private void FixedUpdate()
    {
        HandleLayers();
    }

    private void HandleLayers()
    {
        if (!grounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}
