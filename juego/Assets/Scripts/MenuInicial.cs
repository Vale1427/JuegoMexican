using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    float mousePosX;
    float mousePosY;

    [SerializeField] float movementQuantity;

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Salir()
    {
        Debug.Log("Salir..");
        Application.Quit();
    }

    public void Controles()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void VolverInicio()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void VolverJugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

<<<<<<< HEAD
    void Update()
    {
        // Captura la posición del ratón en pantalla
        mousePosX = Input.mousePosition.x;
        mousePosY = Input.mousePosition.y;

        // Convierte la posición del ratón a coordenadas del mundo con una profundidad fija
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosX, mousePosY, Camera.main.transform.position.z * -1));

        // Mueve el objeto hacia la posición del ratón con una cantidad de movimiento especificada
        transform.position = Vector3.MoveTowards(transform.position, mousePosition, movementQuantity * Time.deltaTime);
=======
    public void SeleccionPersonaje(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
>>>>>>> 5c0c99ead6400435689144e768b7cd3593a8187c
    }
}
