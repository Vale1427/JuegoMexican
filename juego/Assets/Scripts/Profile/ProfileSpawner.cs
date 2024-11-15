using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileSpawner : MonoBehaviour
{

    public Transform newGameSpawn;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(ProfileStorage.s_currentProfile == null || ProfileStorage.s_currentProfile.newGame){
            Instantiate(this.playerPrefab, this.newGameSpawn.position, Quaternion.identity);
        }else{
            //carga de partida
            float x = ProfileStorage.s_currentProfile.x;
            float y = ProfileStorage.s_currentProfile.y;

            Vector3 pos = new Vector3(x, y, 0);

            Instantiate(this.playerPrefab, pos, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
