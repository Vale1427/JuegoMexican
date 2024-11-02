using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void CambiarMaxima(float vidaMaxima){
        slider.maxValue = vidaMaxima;
    }

    public void CambiarVidaActual(float cantidadVida){
        slider.value = cantidadVida;
    }

    public void InicializarBarraDeVida(float cantidadVida){
        CambiarMaxima(cantidadVida);
        CambiarVidaActual(cantidadVida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
