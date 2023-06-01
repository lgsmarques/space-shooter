using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo2Controller : InimigoPai
{
    private Rigidbody2D meuRB;

    // Meu tiro
    [SerializeField] private GameObject meuTiro;

    // Pegando a posição do meu tiro
    [SerializeField] private Transform posicaoTiro;

    // Variavel para mudança de direção
    [SerializeField] private float yMudanca = 2.5f;

    // Variacel para indicar se houve mudança de direção
    private bool mudouDirecao = false;

    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();

        meuRB.velocity = Vector2.down * velocidade;
    }

    // Update is called once per frame
    void Update()
    {
        // Checando se o Sprite Renderer está visivel
        // Pegando informações dos "filhos"
        Atirar();

        if (transform.position.y <= yMudanca && !mudouDirecao)
        {
            if (transform.position.x < 0)
            {
                meuRB.velocity += Vector2.right * velocidade;
            }
            else
            {
                meuRB.velocity += Vector2.left * velocidade;
            }

            mudouDirecao = true;
        }
    }

    private void Atirar()
    {
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;

        if (visivel)
        {
            // Time do tiro
            timerTiro -= Time.deltaTime;

            // Encontrando o player na cena
            var player = FindObjectOfType<PlayerController>();

            if (player)
            {
                if (timerTiro <= 0)
                {
                    // Instanciando meu tiro
                    var tiro = Instantiate(meuTiro, posicaoTiro.position, transform.rotation);

                    // Encontrando a direção
                    Vector2 direcao = player.transform.position - tiro.transform.position;
                    direcao.Normalize();

                    // Dando a direção e a velocidade do tiro
                    tiro.GetComponent<Rigidbody2D>().velocity = direcao * velocidadeTiro;

                    // Dando o angulo do tiro
                    float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                    tiro.transform.rotation = Quaternion.Euler(0f, 0f, angulo + 90f);

                    timerTiro = Random.Range(1f, 2f);
                }
            }
        }
    }
}
