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
    public AudioClip sonidoGolpeAlien; 
    public AudioClip sonidomuerte; 
    public AudioClip sonidomuerteAlien; 
    public AudioClip sonidosalto;
    private string currentLevel;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        animator.SetBool("estaCorriendo", false);
        currentLevel = SceneManager.GetActiveScene().name;

        BarraDeVida = FindObjectOfType<BarraDeVida>();
        if (BarraDeVida == null)
        {
            Debug.LogError("BarraDeVida no encontrada en la escena.");
        }
        else
        {
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

            transform.position += new Vector3(movimientoHorizontal, 0, 0) * 3f * Time.deltaTime;

            if (movimientoHorizontal < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (movimientoHorizontal > 0)
                transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            animator.SetBool("estaCorriendo", false);
            estaMoviendo = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            if (sonidosalto != null)
                audioSource.PlayOneShot(sonidosalto);

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
            float puntoDeImpacto = collision.contacts[0].point.y;
            float posicionJugador = transform.position.y;
            float posicionEnemigo = collision.transform.position.y;

            if (puntoDeImpacto > posicionEnemigo && posicionJugador > posicionEnemigo + 0.05f)
            {
                AlienController enemy = collision.gameObject.GetComponent<AlienController>();
                if (enemy != null) enemy.AtacarYDestruir();

                rb2d.AddForce(new Vector2(0, fuerzaSalto * 0.8f));
                if (sonidoGolpeAlien != null)
                    audioSource.PlayOneShot(sonidomuerteAlien);
            }
            else
            {
                animator.SetTrigger("golpe");
                if (sonidoGolpeAlien != null)
                    audioSource.PlayOneShot(sonidoGolpeAlien);

                float dano = 10f;
                Vector2 direccionRebote = (transform.position.x > collision.transform.position.x) ? Vector2.right : Vector2.left;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(direccionRebote.x * velocidadRebote.x, velocidadRebote.y), ForceMode2D.Impulse);

                vidaActual -= dano;
                BarraDeVida.CambiarVidaActual(vidaActual);

                if (vidaActual <= 0)
                    Muerte();
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
            Invoke("CargarSiguienteEscena", 3f);
        }

        if (ProfileStorage.s_currentProfile != null)
        {
            var profile = ProfileStorage.s_currentProfile;
            Destroy(other.gameObject);

            if (vidaActual <= 0)
                Muerte();
        }
    }

    private void CargarSiguienteEscena() => SceneManager.LoadScene("Mapa");

    private void CargarFin() => SceneManager.LoadScene("Fin");

    private void Muerte()
    {
        Debug.Log("Muerte() fue llamado");
        if (sonidomuerte != null)
        {
            Debug.Log("Reproduciendo sonido de muerte");
            audioSource.PlayOneShot(sonidomuerte);
            Invoke("CargarGameOver", sonidomuerte.length);
        }
        else
        {
            Debug.LogError("Clip sonidomuerte no asignado");
            CargarGameOver();
        }
    }

    private void CargarGameOver() => SceneManager.LoadScene("game-over");

    public bool JugadorSeEstaMoviendo() => estaMoviendo;
}
