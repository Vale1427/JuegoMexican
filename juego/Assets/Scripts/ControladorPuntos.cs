using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControladorPuntos : MonoBehaviour
{
    public static ControladorPuntos Instance;
    [SerializeField] public float cantidadPuntos;

      public void Awake(){
        if(ControladorPuntos.Instance == null){
            ControladorPuntos.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    
    public void SumarPuntos(float puntos)
    {
        cantidadPuntos += puntos;

        // Asegúrate de actualizar los puntos en el perfil actual
        if (ProfileStorage.s_currentProfile != null)
        {
            ProfileStorage.s_currentProfile.points = (int)cantidadPuntos;

            // Guarda el perfil actualizado
            ProfileStorage.StorePlayerProfile(GameObject.FindGameObjectWithTag("Player"));
        }
    }


}

