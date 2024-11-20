using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    public GameObject alejandro; // Referencia al jugador
    public float minX = -15f; // Límite izquierdo
    public float maxX = 60f;  // Límite derecho

    void Update()
    {
        if (alejandro != null)
        {
            Vector3 position = transform.position;

            //posición de la cámara en función del jugador
            position.x = Mathf.Clamp(alejandro.transform.position.x, minX, maxX);
            transform.position = position;
        }
    }
}
