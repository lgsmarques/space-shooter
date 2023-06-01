using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeradorInimigo : MonoBehaviour
{
    [SerializeField] private GameObject[] inimigos;

    [SerializeField] private int pontos = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int baseLevel = 100;

    [SerializeField] private float esperaInimigos = 0f;
    [SerializeField] float tempoEspera = 5f;

    [SerializeField] private int qntInimigos = 0;

    [SerializeField] private GameObject bossAnimation;
    [SerializeField] private bool bossGerado = false;

    [SerializeField] private Text pontosTexto;

    // Audio Boss
    [SerializeField] private AudioClip musicaBoss;
    [SerializeField] private AudioSource musicaJogo;
    
    // Start is called before the first frame update
    void Start()
    {
        pontosTexto.text = pontos.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (level < 10)
        {
            GeraInimigo();
        }
        else if (!bossGerado)
        {
            GeraBoss();
        }
    }

    // Ganhando pontos
    public void GanhaPontos (int pontos)
    {
        this.pontos += pontos * level;

        pontosTexto.text = this.pontos.ToString();

        if (this.pontos >= baseLevel)
        {
            level++;

            baseLevel *= 4;
        }
    }

    public void DiminuindoInimigos()
    {
        qntInimigos--;
    }

    private bool ChecaPosicao(Vector3 posicao, Vector3 size)
    {
        // Checando se tem alguém na posição
        Collider2D hit = Physics2D.OverlapBox(posicao, size, 0f);

        if (hit == null)
        {
            return true;
        }

        return false;
    }

    private void GeraInimigo()
    {
        if (qntInimigos <= 0)
        {
            esperaInimigos -= Time.deltaTime;
        }

        if (esperaInimigos <= 0 && qntInimigos <= 0)
        {
            int quantidade = level * 3;

            int tentativas = 0;

            while (qntInimigos < quantidade)
            {
                // Fazendo ele sair do laço SE ele repetir muitas vezes
                tentativas++;
                if (tentativas > 200)
                {
                    break;
                }

                GameObject inimigoCriado;

                // Decidindo qual inimigo será criado pelo level
                float chance = Random.Range(0f, level);
                if (chance > 3f)
                {
                    inimigoCriado = inimigos[1];
                }
                else
                {
                    inimigoCriado = inimigos[0];
                }

                Vector3 posicao = new Vector3(Random.Range(-8f, 8f), Random.Range(6f, 9f), 0f);

                bool posicaoLivre = ChecaPosicao(posicao, inimigoCriado.transform.localScale);

                if (!posicaoLivre)
                {
                    // Continue faz o laço de repetição ir para a próxima repetição
                    continue;
                }
 
                // Criando inimigo
                Instantiate(inimigoCriado, posicao, transform.rotation);

                qntInimigos++;
            }

            esperaInimigos = tempoEspera;
        }
       
    }

    private void GeraBoss()
    {
        if (qntInimigos <= 0 && esperaInimigos > 0)
        {
            esperaInimigos -= Time.deltaTime;
        }

        if (esperaInimigos <= 0 && qntInimigos <= 0)
        {
            GameObject bossEntrada = Instantiate(bossAnimation, Vector3.zero, transform.rotation);

            Destroy(bossEntrada, 4f);

            bossGerado = true;

            musicaJogo.clip = musicaBoss;
            musicaJogo.Play();
        }
    }
}
