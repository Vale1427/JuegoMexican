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

            //click cargar boton
            uibox.loadBtn.onClick.AddListener(()=>{
                Debug.Log("cargado!");

                ProfileStorage.LoadProfile(profileName);
                SceneManager.LoadScene("Nivel");
            });

            //click eliminar
            uibox.deleteBtn.onClick.AddListener(()=>{
                Debug.Log("eliminado");
                ProfileStorage.DeleteProfile(profileName);
                Destroy(go);
            });
            go.transform.SetParent(this.profilesHolder, false);
            
        }
    }

}