using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileSpawner : MonoBehaviour
{

    public Transform newGameSpawn;
    public GameObject playerPrefab;


    void Start()
    {
        if (ProfileStorage.s_currentProfile != null)
        {
            // Configurar puntos del perfil actual al controlador
            ControladorPuntos.Instance.cantidadPuntos = ProfileStorage.s_currentProfile.points;
        }

        if (ProfileStorage.s_currentProfile.newGame)
        {
            Instantiate(this.playerPrefab, this.newGameSpawn.position, Quaternion.identity);
        }
        else
        {
            // Cargar posición guardada
            float x = ProfileStorage.s_currentProfile.x;
            float y = ProfileStorage.s_currentProfile.y;

            Vector3 pos = new Vector3(x, y, 0);
            Instantiate(this.playerPrefab, pos, Quaternion.identity);
        }
    }


    // // Start is called before the first frame update
    // void Start()
    // {
    //     if(ProfileStorage.s_currentProfile == null || ProfileStorage.s_currentProfile.newGame){
    //         Instantiate(this.playerPrefab, this.newGameSpawn.position, Quaternion.identity);
    //     }else{
    //         //carga de partida
    //         float x = ProfileStorage.s_currentProfile.x;
    //         float y = ProfileStorage.s_currentProfile.y;

    //         Vector3 pos = new Vector3(x, y, 0);

    //         Instantiate(this.playerPrefab, pos, Quaternion.identity);
    //     }
        
    // }

}
