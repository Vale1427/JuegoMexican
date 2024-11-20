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
    //Para sonidos
    private AudioSource audioSource;
    public AudioClip sonidodisparo;


    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Asigna el componente AudioSource al script
    }


    // Update is called once per frame
    void Update()
    {
        jugadorEnRango = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);

        if(jugadorEnRango){
            if (Time.time > tiempoEntreDisparos + tiempoUltimoDisparo){
                
                tiempoUltimoDisparo = Time.time;
                animator.SetTrigger("disparar");
                Invoke(nameof(Disparar), tiempoEsperaDisparo);
            }
        }
    }

    private void Disparar(){
         // Reproducir sonido del disparo
            if (sonidodisparo != null)
            {
            audioSource.PlayOneShot(sonidodisparo);
            }
        Instantiate(balaEnemigo, controladorDisparo.position, controladorDisparo.rotation);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaLinea);
    }
}
