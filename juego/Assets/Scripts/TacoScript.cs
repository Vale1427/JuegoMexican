using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacoScript : MonoBehaviour
{

    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntaje puntaje;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            ControladorPuntos.Instance.SumarPuntos(cantidadPuntos);
            Destroy(gameObject);
            
        }
    }
}
