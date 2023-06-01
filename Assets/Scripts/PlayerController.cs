using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D meuRB;
    [SerializeField] private float velocidade = 5f;

    // Pegando tiro e velocidade do tiro
    [SerializeField] private GameObject meuTiro;
    [SerializeField] private GameObject meuTiro2;
    [SerializeField] private float velocidadeTiro = 10f;
    [SerializeField] private float timerTiro = 0.3f;
    [SerializeField] private float esperaTiro = 0f;

    // Pegando posição do tiro
    [SerializeField] private Transform posicaoTiro;

    // Criando a vida do jogador
    [SerializeField] private int vida = 5;

    // Pegando explosão
    [SerializeField] private GameObject explosao;

    // Variaveis para impedir o jogador de sair da tela
    [SerializeField] private float xMax;
    [SerializeField] private float yMax;

    // Tiro por level
    [SerializeField] private int levelTiro = 1;

    // Pegando o escudo
    [SerializeField] private GameObject escudo;
    private GameObject shield;
    [SerializeField] private int qntEscudo = 3;

    // Passando texto
    [SerializeField] private Text vidaTexto;
    [SerializeField] private Text escudoTexto;

    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();

        vidaTexto.text = vida.ToString();
        escudoTexto.text = qntEscudo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Movendo();
        Atirando();
        CriandoEscudo();

        esperaTiro -= Time.deltaTime;
    }

    private void Movendo()
    {
        // Pegando input horizontal e vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 minhaVelocidade = new Vector2(horizontal, vertical).normalized;
        // Passando minha velocidade para o RB
        meuRB.velocity = minhaVelocidade * velocidade;

        // Limitando jogar dentro da tela
        // Clamp
        float meuX = Mathf.Clamp(transform.position.x, - xMax, xMax);
        float meuY = Mathf.Clamp(transform.position.y, - yMax, yMax);

        // Aplicando meuX e meuY na posição do jogador
        transform.position = new Vector3(meuX, meuY, transform.position.z);
    }

    private void Atirando()
    {              
        // Atirando
        if (Input.GetButton("Fire1") && esperaTiro <= 0)
        {
            Vector3 posicaoDireita = transform.position + new Vector3(0.5f, 0.25f);
            Vector3 posicaoEsquerda = transform.position + new Vector3(-0.5f, 0.25f);

            switch (levelTiro)
            {
                case 1:
                    CriaTiro(meuTiro, posicaoTiro.position);
                    break;
                case 2:
                    CriaTiro(meuTiro2, posicaoDireita);
                    CriaTiro(meuTiro2, posicaoEsquerda);
                    break;
                case 3:
                    CriaTiro(meuTiro, posicaoTiro.position);
                    CriaTiro(meuTiro2, posicaoDireita);
                    CriaTiro(meuTiro2, posicaoEsquerda);
                    break;
            }

            esperaTiro = timerTiro;
        }
    }

    private void CriandoEscudo()
    {
        if (Input.GetButtonDown("Shield") && qntEscudo > 0 && !shield)
        {
            shield = Instantiate(escudo, transform.position, transform.rotation);

            Destroy(shield, 5.2f);

            qntEscudo--;

            escudoTexto.text = qntEscudo.ToString();
        }

        if (shield)
        { 
            shield.transform.position = transform.position;
        }
    }


    private void CriaTiro (GameObject tiroCriado, Vector3 posicao)
    {
        GameObject tiro = Instantiate(tiroCriado, posicao, transform.rotation);

        // Dar direção e velocidade para o tiro
        tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, velocidadeTiro);
    }

    public void PerdeVida(int dano)
    {
        vida -= dano;

        vidaTexto.text = vida.ToString();

        if (vida <= 0)
        {
            Destroy(gameObject);

            Instantiate(explosao, transform.position, transform.rotation);

            // Carregando a tela inicial do jogo
            var gameManager = FindObjectOfType<GameManager>();
            // Metodo de iniciar o jogo
            if (gameManager) 
            { 
                gameManager.Inicio();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {            
            if (levelTiro < 3)
            {
                levelTiro++;
            }

            // Ganhando pontos
            var gerador = FindObjectOfType<GeradorInimigo>();
            gerador.GanhaPontos(50);

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("EscudoDrop"))
        {
            if (qntEscudo < 3)
            {
                qntEscudo++;
            }

            escudoTexto.text = qntEscudo.ToString();

            // Ganhando pontos
            var gerador = FindObjectOfType<GeradorInimigo>();
            gerador.GanhaPontos(50);

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("VidaDrop"))
        {
            vida++;

            vidaTexto.text = vida.ToString();

            // Ganhando pontos
            var gerador = FindObjectOfType<GeradorInimigo>();
            gerador.GanhaPontos(50);

            Destroy(other.gameObject);
        }
    }
}
