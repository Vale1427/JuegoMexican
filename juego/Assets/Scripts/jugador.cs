using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;


public class jugador : MonoBehaviour
{
    public float fuerzaSalto;
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool estaMoviendo = false;
    [SerializeField] private BarraDeVida BarraDeVida;
  
    //para la vida
    public float vidaMaxima; 
    private float vidaActual;

    // Variables para restringir el movimiento del jugador
    private Camera cam;
    private float minX;
    private float maxX;
    private bool estaEnSuelo;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        animator.SetBool("estaCorriendo", false);

    // Buscar y asignar la barra de vida
        BarraDeVida = FindObjectOfType<BarraDeVida>();
        if (BarraDeVida == null)
        {
            Debug.LogError("BarraDeVida no encontrada en la escena.");
        }
        else
        {
            // Inicializar la vida del jugador y la barra de vida
            vidaActual = vidaMaxima;
            BarraDeVida.InicializarBarraDeVida(vidaMaxima);
        }


        // cam = Camera.main; // Obtener la cámara principal
        // // Calcular los límites en X basados en la posición de la cámara
        // float cameraHeight = 2f * cam.orthographicSize; // Altura de la cámara
        // float cameraWidth = cameraHeight * cam.aspect; // Ancho de la cámara

        // minX = cam.transform.position.x - (cameraWidth / 2); // Límite izquierdo
        // maxX = cam.transform.position.x + (cameraWidth / 2); // Límite derecho
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(movimientoHorizontal) > 0.01f)
        {
            animator.SetBool("estaCorriendo", true);
            estaMoviendo = true;

            // Mover el jugador
            transform.position += new Vector3(movimientoHorizontal, 0, 0) * 3f * Time.deltaTime;
            

            // Girar el sprite 
            if (movimientoHorizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); //izqierda
            }
            else if (movimientoHorizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); //derecha
            }
        }
        else
        {
            animator.SetBool("estaCorriendo", false);
            estaMoviendo = false;
            
        }

        if(Input.GetKeyDown(KeyCode.Space) && estaEnSuelo){
            animator.SetBool("estaSaltando", true);
            rb2d.AddForce(new Vector2(0, fuerzaSalto));
            estaEnSuelo = false;
        }

        // // Restringir el movimiento del jugador dentro de los límites de la cámara
        // Vector3 position = transform.position;
        // position.x = Mathf.Clamp(position.x, minX, maxX); // Limitar la posición X
        // transform.position = position; // Aplicar la posición restringida
        
    }

    // private void OnCollisionEnter2D(Collision2D collision){
    //     if(collision.gameObject.tag == "Suelo"){
    //         animator.SetBool("estaSaltando", false);
    //         estaEnSuelo = true;
    //     }

    //     if (collision.gameObject.tag == "alien")
    //     {

    //     }
    //     else{animator.SetBool("choco", true);
            
    //         // Reducir vida del jugador
    //         float dano = 10f; // Cantidad de vida que se reduce al chocar con el alien
    //         vidaActual -= dano;

    //         // Actualizar la barra de vida
    //         BarraDeVida.CambiarVidaActual(vidaActual);


    //         // Verificar si el jugador ha muerto (opcional)
    //         if (vidaActual <= 0)
    //         {
    //             Muerte();
    //         }
    //     }


    //     if(collision.gameObject.tag == "bandera"){
    //         float vida = 10f;
    //         vidaActual += vida;

    //         // Actualizar la barra de vida
    //         BarraDeVida.CambiarVidaActual(vidaActual);

    //         Destroy(collision.gameObject);

    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    if (collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "plataforma")
    {
        animator.SetBool("estaSaltando", false);
        estaEnSuelo = true;
    }

    if (collision.gameObject.tag == "alien")
    {
        // Verificar si el jugador cayó desde arriba del enemigo
        float puntoDeImpacto = collision.contacts[0].point.y;  // Punto de contacto en Y
        float posicionJugador = transform.position.y;          // Posición en Y del jugador
        float posicionEnemigo = collision.transform.position.y; // Posición en Y del enemigo

        // Si la posición del jugador está por encima del enemigo 
        if (puntoDeImpacto > posicionEnemigo && posicionJugador > posicionEnemigo + 0.05f)
        {
            // Ejecutar la animación del enemigo y destruirlo
            AlienController enemy = collision.gameObject.GetComponent<AlienController>();
            if (enemy != null)
            {
                enemy.AtacarYDestruir();
            }

            // Hacer que el jugador salte después de destruir el enemigo
            rb2d.AddForce(new Vector2(0, fuerzaSalto * 1f)); // Ajusta el factor de salto si es necesario
        }
        else
        {
            // Si el jugador choca con el enemigo pero no desde arriba, puede perder vida
            //animator.SetBool("choco", true);
            float dano = 10f;
            vidaActual -= dano;
            BarraDeVida.CambiarVidaActual(vidaActual);

            if (vidaActual <= 0)
            {
                Muerte();
            }
        }
    }


    // if (collision.gameObject.tag == "alien")
    //     {
    //                     // Reducir vida del jugador
    //         float dano = 10f; // Cantidad de vida que se reduce al chocar con el alien
    //         vidaActual -= dano;

    //         // Actualizar la barra de vida
    //         BarraDeVida.CambiarVidaActual(vidaActual);
    //         animator.SetBool("choco", true);


    //         // Verificar si el jugador ha muerto (opcional)
    //         if (vidaActual <= 0)
    //         {
    //             Muerte();
    //         }

    //     }

}

//si choca con la bandera que desaparesca
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("bandera")){
            float vida = 10f;
            vidaActual += vida;
            BarraDeVida.CambiarVidaActual(vidaActual);
            Destroy(other.gameObject);
        }

         if(other.CompareTag("arma")){
            
            Destroy(other.gameObject);


            animator.SetTrigger("celebrando");


            Invoke("CargarSiguienteEscena", 3f);//despes de 3 segundos 
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

        private void CargarSiguienteEscena()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }


    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     // Si el jugador deja de colisionar con un alien, detener la animación "choco"
    //     if (collision.gameObject.tag == "alien")
    //     {
    //         animator.SetBool("choco", false);
    //     }
    // }

    private void Muerte()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    // Verificar si el jugador se está moviendo
    public bool JugadorSeEstaMoviendo()
    {
        return estaMoviendo;
    }


}
