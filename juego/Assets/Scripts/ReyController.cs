using UnityEngine;
using UnityEngine.SceneManagement;

public class ReyController : MonoBehaviour
{
    public int golpesNecesarios = 5; // Número de golpes necesarios para destruir al "rey"
    private int golpesActuales = 0;  // Contador de golpes recibidos

    private Animator animator;
    private bool Atacado = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator no encontrado en el objeto Rey.");
        }
    }

    public void RecibirGolpe()
    {
        if (Atacado) return; // Si ya ha sido derrotado, no hacer nada

        golpesActuales++;

        if (golpesActuales >= golpesNecesarios)
        {
            AtacarYDestruir();
        }
        else
        {
            Debug.Log($"Rey golpeado {golpesActuales}/{golpesNecesarios} veces.");
        }
    }

    // Método para ejecutar la animación de ataque y destruir el enemigo
    public void AtacarYDestruir()
    {
        Atacado = true;               // Detener el movimiento
        animator.SetBool("Atacado", true);
        Destroy(gameObject, 0.7f);    // Destruir el enemigo después de un corto tiempo
        SceneManager.LoadScene("Mapa");
    }
}
