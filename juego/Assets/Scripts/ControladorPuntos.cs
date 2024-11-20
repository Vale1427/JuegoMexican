using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControladorPuntos : MonoBehaviour
{
    public static ControladorPuntos Instance;
    [SerializeField] public float cantidadPuntos;

    public void Awake()
    {
        if (ControladorPuntos.Instance == null)
        {
            ControladorPuntos.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Cargar puntos del perfil actual si existe
        if (ProfileStorage.s_currentProfile != null)
        {
            cantidadPuntos = ProfileStorage.s_currentProfile.points; // Cargar los puntos guardados
        }
    }

    public void SumarPuntos(float puntos)
    {
        cantidadPuntos += puntos;

        // Asegúrate de actualizar los puntos en el perfil actual
        if (ProfileStorage.s_currentProfile != null)
        {
            ProfileStorage.s_currentProfile.points = (int)cantidadPuntos; // Conversión a int

            // Guarda el perfil actualizado
            ProfileStorage.StorePlayerProfile(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    public void ActualizarPuntosDesdePerfil()
    {
        // Método para sincronizar puntos desde el perfil si fuera necesario
        if (ProfileStorage.s_currentProfile != null)
        {
            cantidadPuntos = ProfileStorage.s_currentProfile.points;
        }
    }
}
