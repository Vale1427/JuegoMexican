using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public float velocidadDeMovimiento;
    public LayerMask capaAbajo;
    public LayerMask capaEnfrente;
    public float distanciaAbajo;
    public float distanciaEnfrente;

    public Transform controladorAbajo;
    public Transform controladorEnfrente;
    public bool informacionAbajo;
    public bool informacionEnfrente;
    public bool mirandoDerecha = true;

    // Referencia al Animator para reproducir la animación "alienAtack"
    private Animator animator;
    private bool Atacado = false;
    

    //
    //private Vector3 posicionInicial;

    void Start()
    {
        //rb2D.velocity = new Vector2(velocidadDeMovimiento, rb2D.velocity.y);
       // rb2D.velocity = transform.position;  // Guardamos la posición inicial
       animator = GetComponent<Animator>(); 


    }

    void Update()
    {
        // transform.position += new Vector3(2f * Time.deltaTime, 0, 0);

        rb2D.velocity = new Vector2(velocidadDeMovimiento, rb2D.velocity.y);


        informacionEnfrente = Physics2D.Raycast(controladorEnfrente.position, transform.right, distanciaEnfrente, capaEnfrente);
        informacionAbajo = Physics2D.Raycast(controladorAbajo.position, transform.up * -1, distanciaAbajo, capaAbajo);


        //condicion para cuado debe girar
        if(informacionEnfrente || !informacionAbajo){
            Girar();
        }



    }

    private void Girar(){
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidadDeMovimiento *= -1;
    }


    private void OnDrawGizmos(){
        //dibujar la linea
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorAbajo.transform.position, controladorAbajo.transform.position + transform.up * -1 * distanciaAbajo);
        Gizmos.DrawLine(controladorEnfrente.transform.position, controladorEnfrente.transform.position + transform.right * distanciaEnfrente);

    }

    // Método para ejecutar la animación de ataque y destruir el enemigo
    public void AtacarYDestruir()
    {
        Atacado = true;               // Detener el movimiento
        animator.SetBool("Atacado", true);
        Destroy(gameObject, 1f);        // Destruir el enemigo después de un corto tiempo
    }

}
