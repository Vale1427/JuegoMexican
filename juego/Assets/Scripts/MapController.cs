using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public Button nivel1Button;
    public Button nivel2Button;
    public Button nivel3Button;

    private void Start()
    {
        // Revisar si el perfil cargado está disponible
        if (ProfileStorage.s_currentProfile != null)
        {
            var completedLevels = ProfileStorage.s_currentProfile.completedLevels;

            // Activar los botones según el progreso del jugador
            nivel1Button.interactable = true; // El primer nivel siempre está disponible

            // Comprobar si el nivel correspondiente está en la lista de niveles completados
            nivel2Button.gameObject.SetActive(completedLevels.Contains("Nivel")); // Activa el botón del Nivel 2
            nivel3Button.gameObject.SetActive(completedLevels.Contains("Nivel2")); // Activa el botón del Nivel 3
        }
        else
        {
            // Si no hay perfil cargado, solo el botón del primer nivel estará interactivo
            nivel1Button.interactable = true;
            nivel2Button.gameObject.SetActive(false);
            nivel3Button.gameObject.SetActive(false);
        }
    }
}
