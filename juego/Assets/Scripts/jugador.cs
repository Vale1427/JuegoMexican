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

    // para la vida
    public float vidaMaxima;
    private float vidaActual;
    private bool estaEnSuelo;

    // Para el sonido
    private AudioSource audioSource;
    public AudioClip sonidoGolpeAlien; // Sonido que se reproducirá al chocar con el alien
    public AudioClip sonidomuerte; // Sonido muerto sin vida
    public AudioClip sonidomuerteAlien; // Sonido cuando muere un alien
    public AudioClip sonidosalto;//sonido para saltal
    private string currentLevel;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // Asigna el componente AudioSource al script

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
                transform.localScale = new Vector3(-1, 1, 1); // izquierda
            }
            else if (movimientoHorizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // derecha
            }
        }
        else
        {
            animator.SetBool("estaCorriendo", false);
            estaMoviendo = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            if (sonidosalto != null)
                {
                audioSource.PlayOneShot(sonidosalto);
                }
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
                rb2d.AddForce(new Vector2(0, fuerzaSalto * 0.8f));
                if (sonidoGolpeAlien != null)
                {
                audioSource.PlayOneShot(sonidomuerteAlien);
                }
            }
            else
            {
                
                // Si el jugador choca con el enemigo pero no desde arriba, puede perder vida
                animator.SetTrigger("golpe");
                // Reproducir sonido del golpe
                if (sonidoGolpeAlien != null)
                {
                audioSource.PlayOneShot(sonidoGolpeAlien);
                }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bandera"))
        {
            
            float vida = 10f;
            vidaActual += vida;
            BarraDeVida.CambiarVidaActual(vidaActual);
            Destroy(other.gameObject);

        }

        if (other.CompareTag("arma"))
        {
            Destroy(other.gameObject);

            animator.SetTrigger("celebrando");


            Invoke("CargarSiguienteEscena", 3f); // despes de 3 segundos
                                                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        {

            // Guarda el nivel completado en el perfil
            if (ProfileStorage.s_currentProfile != null)
            {
                var profile = ProfileStorage.s_currentProfile;
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
    Debug.Log ("Muerte() fue llamado");
    if (sonidomuerte != null)
    {
        Debug.Log("Reproduciendo sonido de muerte");
        audioSource.PlayOneShot(sonidomuerte);
        Invoke("CargarGameOver", sonidomuerte.length); // Esperar hasta que termine el sonido
    }
    else
    {
        Debug.LogError("Clip sonidomuerte no asignado");
        CargarGameOver();
    }
}

private void CargarGameOver()
{
    SceneManager.LoadScene("game-over");
}


    // Verificar si el jugador se está moviendo
    public bool JugadorSeEstaMoviendo()
    {
        return estaMoviendo;
    }
}
