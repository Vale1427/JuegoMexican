using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Importa TextMeshPro

public class NewGame : MonoBehaviour
{
    public TMP_InputField profileInput; // Cambia InputField a TMP_InputField

    public void Generate()
    {
        string profileName = this.profileInput.text; // Se recoge el nombre del input
        ProfileStorage.CreateNewGame(profileName);

        SceneManager.LoadScene("P_Game"); // Cargar escena de partidas
    }
}
