using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Obtiene la cantidad de puntos desde el ControladorPuntos y los muestra
        if (ControladorPuntos.Instance != null)
        {
            textMesh.text = ControladorPuntos.Instance.cantidadPuntos.ToString("0");
        }
    }
}
