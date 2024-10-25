using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugador : MonoBehaviour
{
    public float fuerzaSalto;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private bool estaMoviendo = false;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator.SetBool("estaCorriendo", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento horizontal
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        if (movimientoHorizontal != 0)
        {
            animator.SetBool("estaCorriendo", true);
            estaMoviendo = true;
            // Mover el jugador
            transform.position += new Vector3(movimientoHorizontal, 0, 0) * 3f * Time.deltaTime;
            

            // Girar el sprite 
            if (movimientoHorizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); //izqierda
            }
            else if (movimientoHorizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); //derecha
            }
        }
        else
        {
            animator.SetBool("estaCorriendo", false);
            estaMoviendo = false;
            
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            animator.SetBool("estaSaltando", true);
            rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Suelo"){
            animator.SetBool("estaSaltando", false);
        }

        if (collision.gameObject.tag == "alien")
        {
            animator.SetBool("choco", true);
        }
        // else if(collision.gameObject.tag == "alien"){
        //     animator.SetBool("choco", true);
        // }
    }

    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     // Si el jugador sigue colisionando con un alien, mantener la animación "choco"
    //     if (collision.gameObject.tag == "alien")
    //     {
    //         animator.SetBool("choco", true);
    //     }
    // }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Si el jugador deja de colisionar con un alien, detener la animación "choco"
        if (collision.gameObject.tag == "alien")
        {
            animator.SetBool("choco", false);
        }
    }

    // Verificar si el jugador se está moviendo
    public bool JugadorSeEstaMoviendo()
    {
        return estaMoviendo;
    }
}
