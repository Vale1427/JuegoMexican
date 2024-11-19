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

        //arma.SetActive(false);

        // Verificar si el array de aliens está vacío
        if (aliens.Length == 0)
        {
            Debug.Log("Todos los aliens han sido destruidos");
            arma.SetActive(true);
            // Detener el Update si ya no necesitas verificar
            enabled = false;

            
        }
    }



}