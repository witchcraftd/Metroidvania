using UnityEngine;

public class Player : Character
{
    private float runSpeed = 2.0f;
    private float walkSpeed = 1.0f;
    public override void Start()
    {
        base.Start();
        speed = runSpeed;
    }
    public override void Update()
    {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        HandleJumping();
    }

    protected override void HandleMovement()
    {
        base.HandleMovement();
        myAnimator.SetFloat("speed", Mathf.Abs(direction));
        TurnAround(direction);
    }

    protected override void HandleJumping()
    {
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("fall", false);
        }

        //if we press the jumop button (wich is set to space in unity, can be changed later)
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
            stoppedJumping = false;
            //telling the animator to play the jump anim
            myAnimator.SetTrigger("jump");
        }

        //if we hold the jumo button
        if (Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
        {
            Jump();
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
    }
}
