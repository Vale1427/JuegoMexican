using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocidad = 2f;           // Velocidad de movimiento
    public float rangoMovimiento = 3f;     // Cuánto se moverá el enemigo en total
    private bool moviendoDerecha = true;   // Para alternar la dirección de movimiento
    private Vector3 posicionInicial;       // Para guardar la posición inicial del enemigo

    // Referencia al Animator para reproducir la animación "alienAtack"
    private Animator animator;
    private bool estaAtacado = false;

    void Start()
    {
        posicionInicial = transform.position;  // Guardamos la posición inicial
        animator = GetComponent<Animator>();   // Obtener el Animator del enemigo
    }

    void Update()
    {
        if (!estaAtacado)
        {
            MoverEnemigo();
        }
    }

    void MoverEnemigo()
    {
        if (moviendoDerecha)
        {
            transform.position += new Vector3(velocidad * Time.deltaTime, 0, 0);
            if (transform.position.x >= posicionInicial.x + rangoMovimiento)
            {
                moviendoDerecha = false;
                Girar();
            }
        }
        else
        {
            transform.position -= new Vector3(velocidad * Time.deltaTime, 0, 0);
            if (transform.position.x <= posicionInicial.x - rangoMovimiento)
            {
                moviendoDerecha = true;
                Girar();
            }
        }
    }

    void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    // Método para ejecutar la animación de ataque y destruir el enemigo
    public void AtacarYDestruir()
    {
        estaAtacado = true;               // Detener el movimiento
        animator.SetBool("alienAtack", true);
        Destroy(gameObject, 0.5f);        // Destruir el enemigo después de un corto tiempo
    }
}

