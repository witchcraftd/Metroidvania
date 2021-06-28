using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] protected float speed = 1.0f;
    protected float direction;

    protected bool facingRight = true;

    [Header("Jump Variables")]
    //force, time, and counter(will count time you have been jumping)
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float jumpTime;
    [SerializeField] protected float jumpTimeCounter;
    [SerializeField] protected bool stoppedJumping;
    [SerializeField] protected Transform groundCheck; //transform will verify the position of the object
    [SerializeField] protected float radOCircle;  //creating a radial value to use in gizmos to verify ground
    [SerializeField] protected LayerMask whatIsGround; // gonna define what groud is based on a layer mask
    [SerializeField] protected bool grounded; //check if tis touching the floor

    //[Header("Attack Variables")]

    //[Header("Character Variables")]

    protected Rigidbody2D rb;
    protected Animator myAnimator;

    #region monos
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        //what is to be grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);
        
        //check vertical velocity
        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("fall", true);
        }
    }

    public virtual void FixedUpdate()
    {
        //handle physics
        HandleMovement();
        HandleLayers();
    }
    #endregion

    #region mechanics
    protected void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    #endregion


    #region subMechanics

    protected abstract void HandleJumping();
    protected virtual void HandleMovement()
    {
        Move();
    }
    protected void TurnAround(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    protected void HandleLayers()
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
    #endregion


    #region Gizmos
    protected void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOCircle); //gizmos to recognize when the floor bool is true or false
    }
    #endregion

}
