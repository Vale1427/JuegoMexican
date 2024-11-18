using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{

    public float velocidad;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * velocidad * Vector2.left);
        
    }

}
