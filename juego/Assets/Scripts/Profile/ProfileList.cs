using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileList : MonoBehaviour
{
    public Transform profilesHolder;
    public GameObject profileUIBoxPrefab;

    void Start()
    {
        // Obtener el índice de perfiles
        var index = ProfileStorage.GetProfileIndex();

        // Crear una lista para almacenar los perfiles con sus puntos
        var profilesWithPoints = new List<(string name, int points)>();

        // Cargar perfiles y obtener sus puntos
        foreach (var profileName in index.profileFileNames)
        {
            ProfileStorage.LoadProfile(profileName);
            var points = ProfileStorage.s_currentProfile.points;
            profilesWithPoints.Add((profileName, points));
        }

        // Ordenar la lista por puntos en orden descendente
        profilesWithPoints.Sort((a, b) => b.points.CompareTo(a.points));

        // Generar la UI para cada perfil en el orden correcto
        foreach (var profile in profilesWithPoints)
        {
            var go = Instantiate(this.profileUIBoxPrefab);
            var uibox = go.GetComponent<ProfileBoxUI>();

            uibox.nameLabel.text = profile.name;
            uibox.puntos.text = profile.points.ToString();

            // Configurar el botón de cargar
            uibox.loadBtn.onClick.AddListener(() =>
            {
                Debug.Log("Cargado!");
                ProfileStorage.LoadProfile(profile.name);
                SceneManager.LoadScene("Nivel");
            });

            // Configurar el botón de eliminar
            uibox.deleteBtn.onClick.AddListener(() =>
            {
                Debug.Log("Eliminado");
                ProfileStorage.DeleteProfile(profile.name);
                Destroy(go);
            });

            go.transform.SetParent(this.profilesHolder, false);
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;


// public class ProfileList: MonoBehaviour
// {
//     public Transform profilesHolder;
//     public GameObject profileUIBoxPrefab;

//     void Start(){
//         var index = ProfileStorage.GetProfileIndex();

//         foreach (var profileName in index.profileFileNames)
//     {
//         var go = Instantiate(this.profileUIBoxPrefab);
//         var uibox = go.GetComponent<ProfileBoxUI>();

//         uibox.nameLabel.text = profileName;

//         // Recupera el perfil para mostrar puntos
//         ProfileStorage.LoadProfile(profileName); 
//         uibox.puntos.text = ProfileStorage.s_currentProfile.points.ToString();

//         // Configura botones de cargar y eliminar
//         uibox.loadBtn.onClick.AddListener(() =>
//         {
//             Debug.Log("Cargado!");

//             ProfileStorage.LoadProfile(profileName);
//             SceneManager.LoadScene("Nivel");
//         });

//         uibox.deleteBtn.onClick.AddListener(() =>
//         {
//             Debug.Log("Eliminado");
//             ProfileStorage.DeleteProfile(profileName);
//             Destroy(go);
//         });

//         go.transform.SetParent(this.profilesHolder, false);
//     }

//         // foreach (var profileName in index.profileFileNames)
//         // {
//         //     var go = Instantiate(this.profileUIBoxPrefab);
//         //     var uibox = go.GetComponent<ProfileBoxUI>();

//         //     uibox.nameLabel.text = profileName;

//         //     //click cargar boton
//         //     uibox.loadBtn.onClick.AddListener(()=>{
//         //         Debug.Log("cargado!");

//         //         ProfileStorage.LoadProfile(profileName);
//         //         SceneManager.LoadScene("Nivel");
//         //     });

//         //     //click eliminar
//         //     uibox.deleteBtn.onClick.AddListener(()=>{
//         //         Debug.Log("eliminado");
//         //         ProfileStorage.DeleteProfile(profileName);
//         //         Destroy(go);
//         //     });
//         //     go.transform.SetParent(this.profilesHolder, false);
            
//         // }
//     }

// }