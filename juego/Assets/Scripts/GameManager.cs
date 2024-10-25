using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject top;//lo que tenemos en prefabs
    public Renderer fondo;
    public List<GameObject> cols;
    public float velocidad = 5f;
    public jugador jugador; // Referencia al script del jugador

    // Start is called before the first frame update
    void Start()
    {
        // Crear el mapa
        for (int i = 0; i < 300; i++)
        {
            cols.Add(Instantiate(top, new Vector2(-15 + i, -6), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Solo mover el mapa y el fondo si el jugador se está moviendo
        if (jugador.JugadorSeEstaMoviendo())
        {
            // Obtener el movimiento horizontal del jugador
            float movimientoHorizontal = Input.GetAxis("Horizontal");

            // Mover el fondo
            if (movimientoHorizontal > 0)
            {
                fondo.material.mainTextureOffset += new Vector2(0.015f, 0) * Time.deltaTime; 
            }
            else if (movimientoHorizontal < 0)
            {
                fondo.material.mainTextureOffset += new Vector2(-0.015f, 0) * Time.deltaTime; 
            }

            // Mover el mapa en función del movimiento del jugador
            for (int i = 0; i < cols.Count; i++)
            {
                // Mover el mapa hacia la izquierda por defecto
                float desplazamientoMapa = -1 * Time.deltaTime * velocidad;

                // Ajustar la dirección del mapa en función del movimiento del jugador
                if (movimientoHorizontal < 0)
                {
                    // Si el jugador se mueve a la izquierda, mover el mapa a la derecha
                    desplazamientoMapa = 1 * Time.deltaTime * velocidad;
                }

                // Aplicar el desplazamiento al mapa
                cols[i].transform.position += new Vector3(desplazamientoMapa, 0, 0);
            }
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GameManager : MonoBehaviour
// {
//     public GameObject top;
//     public Renderer fondo;
//     public List<GameObject> cols;
//     public float velocidad = 2;
//     public jugador jugador; // Referencia al script del jugador

//     // Start is called before the first frame update
//     void Start()
//     {
//         // Crear el mapa
//         for (int i = 0; i < 300; i++)
//         {
//             cols.Add(Instantiate(top, new Vector2(-15 + i, -6), Quaternion.identity));
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Solo mover el mapa y el fondo si el jugador se está moviendo
//         if (jugador.JugadorSeEstaMoviendo())
//         {
//             // Mover el fondo
//             fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.015f, 0) * Time.deltaTime;

//             // Mover el mapa
//             for (int i = 0; i < cols.Count; i++)
//             {
//                 cols[i].transform.position = cols[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
//             }
//         }
//     }
// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GameManager : MonoBehaviour
// {

//     public GameObject top;
//     public Renderer fondo; 

//     public List<GameObject> cols;
//     public float velocidad = 2;
//     // Start is called before the first frame update
//     void Start()
//     {
//         //creae mapa
//         for(int i=0; i<300;  i++){
//             //crear nuevos objetos
//             cols.Add(Instantiate(top, new Vector2(-15 + i, -6), Quaternion.identity));
//         }
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         fondo.material.mainTextureOffset =  fondo.material.mainTextureOffset + new Vector2(0.015f,0) * Time.deltaTime;
        
//         //mover mapa
//         for(int i=0; i<cols.Count;  i++){
//             cols[i].transform.position = cols[i].transform.position + new Vector3(-1,0,0) * Time.deltaTime * velocidad;
//         }

//     }
// }
