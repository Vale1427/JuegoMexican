using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioJugador : MonoBehaviour
{
    public CamaraScript camaraScript; // Referencia al script de la cámara

    void Start()
    {
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");

        // Instancia el personaje jugable
        GameObject jugadorInstanciado = Instantiate(GameManagerPersonajes.Instance.personajes[indexJugador].personajeJugable, transform.position, Quaternion.identity);

        // Asigna el jugador instanciado a la cámara
        camaraScript.alejandro = jugadorInstanciado;

    }
}
