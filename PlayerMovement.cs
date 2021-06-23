using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //Necessarios para as animacoes e fisica
    private Rigidbody2D rb2D;
    private Animator myAnimator;

    private bool facingRight = true;

    //variaveis para movimento
    public float speed = 2.0f;
    public float movHorizontal;

    private void Start()
    {
        //definindo os gameObjects encontrados no jogador
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    //input das fisicas
    private void Update()
    {
        //checa se o jogador deu algum input (comando)
        movHorizontal = Input.GetAxisRaw("Horizontal");
    }
    //roda a fisica
    private void FixedUpdate()
    {
        //move o personagem
        rb2D.velocity = new Vector2(movHorizontal * speed,rb2D.velocity.y); //y nao eh tocado
        Flip(movHorizontal);
        myAnimator.SetFloat("speed", Mathf.Abs(movHorizontal));
    }

    //Criando a funcao Flip
    private void Flip(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale; // criando um vetor para controlar o Scale do objeto local(Player)
            theScale.x *= -1; //*= aqui funciona igual a linguagem python onde o valor sera ele mesmo vezes determinado numero.
            transform.localScale = theScale;
        }
    }
}
