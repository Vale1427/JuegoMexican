using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject top;//lo que tenemos en prefabs
    public GameObject plataforma;
    public Renderer fondo;
    public List<GameObject> cols;
    public List<GameObject> plataformas;
    public float velocidad = 5f;
    public jugador jugador; // Referencia al script del jugador

    public GameObject alien;//lo que esta en prefabs

    public GameObject arma;
    public List<GameObject> enemigos;
    



    void Start()
    {
        // Crear el mapa
        for (int i = 0; i < 100; i++)
        {
            cols.Add(Instantiate(top, new Vector2(-21 + i, -8), Quaternion.identity));
        }

        //crear plataformas
        plataformas.Add(Instantiate(plataforma, new Vector2(-15, -4), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(-9, -2), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(-11, 3), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(-5, 1), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(1, -1), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(5, 2), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(7, 2), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(9, 2), Quaternion.identity));

        plataformas.Add(Instantiate(plataforma, new Vector2(12, -2), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(16, 2), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(18, 2), Quaternion.identity));

        plataformas.Add(Instantiate(plataforma, new Vector2(22, -2), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(20, -5), Quaternion.identity));

        plataformas.Add(Instantiate(plataforma, new Vector2(27, -4), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(29, -4), Quaternion.identity));
        
        plataformas.Add(Instantiate(plataforma, new Vector2(33, -2), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(37, 1), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(39, 1), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(41, 1), Quaternion.identity));

        plataformas.Add(Instantiate(plataforma, new Vector2(47, -2), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(50, -4), Quaternion.identity));
        plataformas.Add(Instantiate(plataforma, new Vector2(57, -4), Quaternion.identity));



        // Crear enemigos 
        //en plataformas
        enemigos.Add(Instantiate(alien, new Vector2(7, 3), Quaternion.identity));
        enemigos.Add(Instantiate(alien, new Vector2(39, 2), Quaternion.identity));
        enemigos.Add(Instantiate(alien, new Vector2(16, 3), Quaternion.identity));

//aparte
        enemigos.Add(Instantiate(alien, new Vector2(3, -7), Quaternion.identity));

        //
        //plataformas.Add(Instantiate(top, new Vector2(, -3), Quaternion.identity));
        enemigos.Add(Instantiate(alien, new Vector2(14, -7), Quaternion.identity));


    }


    void Update(){
        
        // Obtener el movimiento  del jugador
        //  enemigos 
        // for (int i = 0; i < enemigos.Count; i++)
        // {
        //     float desplazamientoEnemigo = -1 * Time.deltaTime * velocidad;

        //     if (movimientoHorizontal < 0)
        //     {
        //         desplazamientoEnemigo = 1 * Time.deltaTime * velocidad;
        //     }
        //     enemigos[i].transform.position += new Vector3(desplazamientoEnemigo, 0, 0);
        // }


    if (jugador.JugadorSeEstaMoviendo())
    {
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

        //Mover el mapa 
        for (int i = 0; i < cols.Count; i++) //calar con el movimiento de enemigos
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


        //plataformas
        for (int i = 0; i < plataformas.Count; i++)
        {
            float desplazamientoPlataforma = -1 * Time.deltaTime * velocidad;

            if (movimientoHorizontal < 0)
            {
                desplazamientoPlataforma = 1 * Time.deltaTime * velocidad;
            }

            plataformas[i].transform.position += new Vector3(desplazamientoPlataforma, 0, 0);
        }

    }
}

    
}

