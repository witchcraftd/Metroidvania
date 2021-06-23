using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //criar forca, aplicar forca, 1x
    [Header("PublicVars")]
    public float jumpForce;
    public bool grounded; //checar se esta no chao
    private Rigidbody2D rb;

    [Header("PrivateVars")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radOCircle;
    [SerializeField] private LayerMask whatIsGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() //reconhecendo o input para pular
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);
        if(Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOCircle); //gizmos para reconhecer quando estiver tocando o chao (ground)
    }
}
