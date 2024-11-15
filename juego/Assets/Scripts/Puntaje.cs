using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    private float puntos;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        puntos = PlayerPrefs.GetFloat("Puntaje", 0); // Cargar puntos previos
    }

    void Update()
    {
        textMesh.text = puntos.ToString("0");
    }

    public void SumarPuntos(float puntosEntrada)
    {
        puntos += puntosEntrada;
        PlayerPrefs.SetFloat("Puntaje", puntos); // Guardar puntos
    }

    public void ReiniciarPuntos()
    {
        puntos = 0;
        PlayerPrefs.SetFloat("Puntaje", puntos); // Guardar puntos como 0
    }
}
