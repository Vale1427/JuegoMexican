using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject arma;
    public GameObject cohete;
    public GameObject corona;

    private void Update()
    {
        GameObject[] aliens = GameObject.FindGameObjectsWithTag("alien");
        GameObject[] blues = GameObject.FindGameObjectsWithTag("blue");
        GameObject[] reyes = GameObject.FindGameObjectsWithTag("rey");

        // Obtener el nombre de la escena actual
        string escenaActual = SceneManager.GetActiveScene().name;

        // Verificar si no hay aliens
        if (aliens.Length == 0)
        {
            Debug.Log("Todos los aliens han sido destruidos");
            arma.SetActive(true);
        }

        // Verificar si no hay reyes y la escena es "Nivel2"
        if (reyes.Length == 0 && escenaActual == "Nivel3")
        {
            Debug.Log("Todos los reyes han sido destruidos en Nivel2");
            corona.SetActive(true);
        }


        if (blues.Length == 0 && escenaActual == "Nivel2")
        {
            Debug.Log("Todos los blues han sido destruidos en Nivel2");
            cohete.SetActive(true);
        }

        // Si ya no hay aliens ni reyes y la lógica está completa, puedes desactivar el Update
        if (aliens.Length == 0 && reyes.Length == 0 && blues.Length == 0)
        {
            enabled = false;
        }


    }
}
