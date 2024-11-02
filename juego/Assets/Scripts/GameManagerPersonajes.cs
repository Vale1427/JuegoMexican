using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPersonajes : MonoBehaviour
{
    public static GameManagerPersonajes Instance;

    public List<PersonajeSeleccionado> personajes;

    private void Awake()
    {
        if (GameManagerPersonajes.Instance == null) // Aquí se asegura que estamos usando GameManagerPersonajes.Instance
        {
            GameManagerPersonajes.Instance = this;
            DontDestroyOnLoad(this.gameObject); // Evita que se destruya entre escenas
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruye el objeto duplicado
        }
    }
}
