using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{

    public Transform controladorDisparo;
    public float  distanciaLinea;
    public LayerMask capaJugador;
    public bool jugadorEnRango;
    public GameObject balaEnemigo;
    public float tiempoEntreDisparos;
    public float tiempoUltimoDisparo;
    public float tiempoEsperaDisparo;
    private bool Atacado = false;

    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        jugadorEnRango = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);

        if(jugadorEnRango){
            if(Time.time > tiempoEntreDisparos + tiempoUltimoDisparo){
                
                tiempoUltimoDisparo = Time.time;
                animator.SetTrigger("disparar");
                Invoke(nameof(Disparar), tiempoEsperaDisparo);
            }
        }
    }

    private void Disparar(){
        Instantiate(balaEnemigo, controladorDisparo.position, controladorDisparo.rotation);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaLinea);
    }

    // Método para ejecutar la animación de ataque y destruir el enemigo
    public void AtacarYDestruir()
    {
        Atacado = true;               // Detener el movimiento
        animator.SetBool("Atacado", true);
        Destroy(gameObject, 0.7f);        // Destruir el enemigo después de un corto tiempo
    }
}
