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
    
    public void SumarPuntos(float puntos){

        cantidadPuntos += puntos;
    }

}

