using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject arma;
    

// Actualizar cada cierto tiempo si todos los aliens fueron destruidos
    private void Update()
    {
        // Buscar todos los objetos en la escena con la etiqueta "Alien"
        GameObject[] aliens = GameObject.FindGameObjectsWithTag("alien");

        arma.SetActive(false);

        // Verificar si el array de aliens está vacío
        if (aliens.Length == 0)
        {
            Debug.Log("Todos los aliens han sido destruidos");
            arma.SetActive(true);

            // Hacer que el jugador ejecute la animación de celebración
            //CelebrarJugador();
            
            // Detener el Update si ya no necesitas verificar
            enabled = false;
        }
    }



    // private void CelebrarJugador()
    // {
    //     // Ejecutar la animación de celebración del jugador
    //     if (jugador != null)
    //     {
    //         Animator jugadorAnimator = jugador.GetComponent<Animator>();
    //         if (jugadorAnimator != null)
    //         {
    //             jugadorAnimator.SetTrigger("celebrar"); // Asume que la animación usa el trigger "celebrar"
    //         }
    //     }
    // }

}