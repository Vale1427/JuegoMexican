using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{

    public GameObject alejandro;

    // Update is called once per frame
    void Update()
    {

        Vector3 position = transform.position;
        position.x = alejandro.transform.position.x;
        transform.position = position;
        
    }
}
