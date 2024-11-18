using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ProfileList: MonoBehaviour
{
    public Transform profilesHolder;
    public GameObject profileUIBoxPrefab;

    void Start(){
        var index = ProfileStorage.GetProfileIndex();

        foreach (var profileName in index.profileFileNames)
    {
        var go = Instantiate(this.profileUIBoxPrefab);
        var uibox = go.GetComponent<ProfileBoxUI>();

        uibox.nameLabel.text = profileName;

        // Recupera el perfil para mostrar puntos
        ProfileStorage.LoadProfile(profileName); 
        uibox.puntos.text = ProfileStorage.s_currentProfile.points.ToString();

        // Configura botones de cargar y eliminar
        uibox.loadBtn.onClick.AddListener(() =>
        {
            Debug.Log("Cargado!");

            ProfileStorage.LoadProfile(profileName);
            SceneManager.LoadScene("Nivel");
        });

        uibox.deleteBtn.onClick.AddListener(() =>
        {
            Debug.Log("Eliminado");
            ProfileStorage.DeleteProfile(profileName);
            Destroy(go);
        });

        go.transform.SetParent(this.profilesHolder, false);
    }

        // foreach (var profileName in index.profileFileNames)
        // {
        //     var go = Instantiate(this.profileUIBoxPrefab);
        //     var uibox = go.GetComponent<ProfileBoxUI>();

        //     uibox.nameLabel.text = profileName;

        //     //click cargar boton
        //     uibox.loadBtn.onClick.AddListener(()=>{
        //         Debug.Log("cargado!");

        //         ProfileStorage.LoadProfile(profileName);
        //         SceneManager.LoadScene("Nivel");
        //     });

        //     //click eliminar
        //     uibox.deleteBtn.onClick.AddListener(()=>{
        //         Debug.Log("eliminado");
        //         ProfileStorage.DeleteProfile(profileName);
        //         Destroy(go);
        //     });
        //     go.transform.SetParent(this.profilesHolder, false);
            
        // }
    }

}