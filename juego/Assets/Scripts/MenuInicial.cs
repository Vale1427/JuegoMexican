using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar(){
        SceneManager.LoadScene("Nivel");
    }

    public void Salir(){
      Debug.Log("Salir..");
      Application.Quit();
    }

    public void Controles(){
        SceneManager.LoadScene("controles");
    }

    public void VolverInicio(){
        SceneManager.LoadScene("menu-inicial");
    }

    public void VolverJugar(){
        SceneManager.LoadScene("Nivel");
    }

    public void SeleccionPersonaje(){
        SceneManager.LoadScene("jugador");
    }
}
