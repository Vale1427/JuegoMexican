using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanderaScript : MonoBehaviour
{

    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntaje puntaje;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            puntaje.SumarPuntos(cantidadPuntos);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
