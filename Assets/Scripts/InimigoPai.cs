using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPai : MonoBehaviour
{
    [Header("Informações básicas")]
    // Atributos que todos os inimigos devem ter
    [SerializeField] protected float velocidade = 3f;
    [SerializeField] protected int vida;
    [SerializeField] protected GameObject explosao;
    [SerializeField] protected float timerTiro = 1f;
    [SerializeField] protected float velocidadeTiro = 3f;
    [SerializeField] protected int pontos = 10;

    [SerializeField] protected float drop = 10f;
    [SerializeField] protected GameObject powerUp;
    [SerializeField] protected float timerDrop = 3f;

    // Dropando Shield e Vida (não tem no curso)
    [SerializeField] protected GameObject dropEscudo;
    [SerializeField] protected GameObject dropVida;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Criando o método perde vida
    public void PerdeVida(int dano)
    {
        if (transform.position.y <= 5)
        {
            vida -= dano;

            if (vida <= 0)
            {
                Morrendo();
            }
        }
    }

    public void Morrendo()
    {
        Destroy(gameObject);
        Instantiate(explosao, transform.position, transform.rotation);
        Dropando();

        // Ganhando pontos
        var gerador = FindObjectOfType<GeradorInimigo>();
        gerador.GanhaPontos(pontos);
    }

    private void Dropando()
    {
        if (Random.Range(1, 100) <= drop)
        {
            GameObject pU = Instantiate(powerUp, transform.position, transform.rotation);

            Destroy(pU, 3f);

            pU.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        else if (Random.Range(1, 100) <= drop)
        {
            GameObject vidaDrop = Instantiate(dropVida, transform.position, transform.rotation);

            Destroy(vidaDrop, 3f);

            vidaDrop.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        else if (Random.Range(1, 100) <= drop)
        {
            GameObject shield = Instantiate(dropEscudo, transform.position, transform.rotation);

            Destroy(shield, 3f);

            shield.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destruidor"))
        {
            Destroy(gameObject);
        }
        // Colidindo com escudo
        else if (collision.gameObject.CompareTag("Escudo"))
        {
            if (!gameObject.CompareTag("Boss")) 
            {
                Morrendo();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            collision.gameObject.GetComponent<PlayerController>().PerdeVida(1);

            Morrendo();
        }
    }

    private void OnDestroy()
    {
        var gerador = FindObjectOfType<GeradorInimigo>();

        if (gerador)
        {
            gerador.DiminuindoInimigos();
        }
    }
}
