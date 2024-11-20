using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class jugador : MonoBehaviour
{
    public float fuerzaSalto;
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool estaMoviendo = false;

    private bool sePuedeMover = true;
    [SerializeField] private Vector2 velocidadRebote;

    [SerializeField] private BarraDeVida BarraDeVida;
  
    //para la vida
    public float vidaMaxima; 
    private float vidaActual;
    private bool estaEnSuelo;

    private string currentLevel;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        animator.SetBool("estaCorriendo", false);
        // Obtener el nombre del nivel actual
        currentLevel = SceneManager.GetActiveScene().name;

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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    if (collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "plataforma")
    {
        animator.SetBool("estaSaltando", false);
        estaEnSuelo = true;
    }

    //---------------------------------------------------------------------------------------------


    if (collision.gameObject.tag == "alien")
    {
        // Verificar si el jugador cayó desde arriba del enemigo
        float puntoDeImpacto = collision.contacts[0].point.y; 
        float posicionJugador = transform.position.y;          
        float posicionEnemigo = collision.transform.position.y; 

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
            rb2d.AddForce(new Vector2(0, fuerzaSalto * 0.8f));//
        }
        else
        {
            // Si el jugador choca con el enemigo pero no desde arriba, puede perder vida
            animator.SetTrigger("golpe");
            float dano = 10f;
            // Rebote en dirección contraria al enemigo
            Vector2 direccionRebote = (transform.position.x > collision.transform.position.x) ? Vector2.right : Vector2.left;
            rb2d.velocity = Vector2.zero; 
            rb2d.AddForce(new Vector2(direccionRebote.x * velocidadRebote.x, velocidadRebote.y), ForceMode2D.Impulse);

            
            vidaActual -= dano;
            BarraDeVida.CambiarVidaActual(vidaActual);
            

            if (vidaActual <= 0)
            {
                Muerte();
            }
        }
    }

    //---------------------------------------------------------------------------------------------

        if (collision.gameObject.tag == "blue")
    {
        // Verificar si el jugador cayó desde arriba del enemigo
        float puntoDeImpacto = collision.contacts[0].point.y; 
        float posicionJugador = transform.position.y;          
        float posicionEnemigo = collision.transform.position.y; 

        // Si la posición del jugador está por encima del enemigo 
        if (puntoDeImpacto > posicionEnemigo && posicionJugador > posicionEnemigo + 0.05f)
        {
            // Ejecutar la animación del enemigo y destruirlo
            DisparoEnemigo blue = collision.gameObject.GetComponent<DisparoEnemigo>();
            if (blue != null)
            {
                blue.AtacarYDestruir();
            }

            // Hacer que el jugador salte después de destruir el enemigo
            rb2d.AddForce(new Vector2(0, fuerzaSalto * 0.8f));//
        }
        else
        {
            // Si el jugador choca con el enemigo pero no desde arriba, puede perder vida
            animator.SetTrigger("golpe");
            float dano = 10f;
            // Rebote en dirección contraria al enemigo
            Vector2 direccionRebote = (transform.position.x > collision.transform.position.x) ? Vector2.right : Vector2.left;
            rb2d.velocity = Vector2.zero; 
            rb2d.AddForce(new Vector2(direccionRebote.x * velocidadRebote.x, velocidadRebote.y), ForceMode2D.Impulse);

            
            vidaActual -= dano;
            BarraDeVida.CambiarVidaActual(vidaActual);
            

            if (vidaActual <= 0)
            {
                Muerte();
            }
        }
    }



    //-----------------------------------------------------------------------------------------------

    if (collision.gameObject.tag == "rey")
{
    // Verificar si el jugador cayó desde arriba del enemigo
    float puntoDeImpacto = collision.contacts[0].point.y; 
    float posicionJugador = transform.position.y;          
    float posicionEnemigo = collision.transform.position.y; 

    if (puntoDeImpacto > posicionEnemigo && posicionJugador > posicionEnemigo + 0.05f)
    {
        // Acceder al script del Rey y registrar un golpe
        ReyController rey = collision.gameObject.GetComponent<ReyController>();
        if (rey != null)
        {
            rey.RecibirGolpe();
        }

        // Hacer que el jugador salte después de golpear al enemigo
        rb2d.AddForce(new Vector2(0, fuerzaSalto * 0.8f));
    }
    else
    {
        // Si no cae desde arriba, el jugador recibe daño
        animator.SetTrigger("golpe");
        float dano = 10f;

        // Rebote en dirección contraria al enemigo
        Vector2 direccionRebote = (transform.position.x > collision.transform.position.x) ? Vector2.right : Vector2.left;
        rb2d.velocity = Vector2.zero; 
        rb2d.AddForce(new Vector2(direccionRebote.x * velocidadRebote.x, velocidadRebote.y), ForceMode2D.Impulse);

        vidaActual -= dano;
        BarraDeVida.CambiarVidaActual(vidaActual);

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }
}


}


    //si choca con la bandera que desaparesca
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("bandera")){
            float vida = 10f;
            vidaActual += vida;
            BarraDeVida.CambiarVidaActual(vidaActual);
            Destroy(other.gameObject);
        }

         if(other.CompareTag("arma") || other.CompareTag("cohete")){
            
            Destroy(other.gameObject);


            animator.SetTrigger("celebrando");

            // Guarda el nivel completado en el perfil
            if (ProfileStorage.s_currentProfile != null)
            {
                var profile = ProfileStorage.s_currentProfile;
                if (!profile.completedLevels.Contains(currentLevel))
                {
                    profile.completedLevels.Add(currentLevel);
                    ProfileStorage.StorePlayerProfile(GameObject.FindGameObjectWithTag("Player"));
                }
            }

            Invoke("CargarSiguienteEscena", 3f);//despes de 3 segundos 
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        if(other.CompareTag("corona")){
            SceneManager.LoadScene("Fin");
            animator.SetTrigger("celebrando");
            Invoke("CargarFin", 3f);
        }

        if(other.CompareTag("bala")){
            animator.SetTrigger("golpe");
            float dano = 10f;
            // Rebote en dirección contraria al enemigo
            //Vector2 direccionRebote = (transform.position.x > collision.transform.position.x) ? Vector2.right : Vector2.left;
            rb2d.velocity = Vector2.zero; 
           // rb2d.AddForce(new Vector2(direccionRebote.x * velocidadRebote.x, velocidadRebote.y), ForceMode2D.Impulse);
            
            vidaActual -= dano;
            BarraDeVida.CambiarVidaActual(vidaActual); 
            Destroy(other.gameObject);

            if (vidaActual <= 0)
            {
                Muerte();
            }
        }

    }

    private void CargarSiguienteEscena()
    {
        SceneManager.LoadScene("Mapa");
    }


    private void CargarFin()
    {
        SceneManager.LoadScene("Fin");
    }


    private void Muerte()
    {
        SceneManager.LoadScene("game-over");
    }

    // Verificar si el jugador se está moviendo
    public bool JugadorSeEstaMoviendo()
    {
        return estaMoviendo;
    }


}
