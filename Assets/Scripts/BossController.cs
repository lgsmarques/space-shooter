using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : InimigoPai
{
    [Header("Informacoes especificas")]
    // Vari�vel para definir o estado
    [SerializeField] private string estado = "estado1";
    [SerializeField] private string[] estados;
    private float timerEstado = 5f;
    [SerializeField] int vidaMax;

    // Movimentação do boss
    private Rigidbody2D meuRB;
    private bool movDireita = true;

    [Header("Informacoes dos tiros")]
    [SerializeField] private GameObject meuTiro1;
    [SerializeField] private GameObject meuTiro2;
    [SerializeField] private Transform posicaoCentro;
    [SerializeField] private Transform posicaoDireita;
    [SerializeField] private Transform posicaoEsquerda;

    // Barra de vida
    [Header("Informacoes de UI")]
    [SerializeField] private Image barraVida;
        
    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        vida = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {   
        Debug.Log(vida);

        TrocaEstado();

        timerTiro -= Time.deltaTime;

        switch (estado)
        {
            case "estado1":
                Estado1();
                break;
            case "estado2":
                Estado2();
                break;
            case "estado3":
                Estado3();
                break;
        }

        // Atualizando a vida do boss
        barraVida.fillAmount = (float) vida / (float) vidaMax;
        // Alterando a cor conforme a vida cai

        barraVida.color = new Color32((byte) ((1 - barraVida.fillAmount) *171), 0, (byte) (barraVida.fillAmount * 171), 255);
    }

    private void AumentandoDificuldade()
    {
        timerTiro = 0.4f + 0.6f * ((float) vida /(float) vidaMax);
        Debug.Log(timerTiro);
    }

    private void Estado1()
    {
        if (movDireita)
        {
            meuRB.velocity = Vector2.right * velocidade;
        }
        else
        {
            meuRB.velocity = Vector2.left * velocidade;
        }

        if (transform.position.x >= 6)
        {
            movDireita = false;
        }
        else if (transform.position.x <= -6)
        {
            movDireita = true;
        }

        if (timerTiro <= 0f)
        {
            Atirando1();
            AumentandoDificuldade();
        }
    }

    private void Estado2()
    {
        meuRB.velocity = Vector2.zero;

        if (timerTiro <= 0f)
        {
            Atirando2();
            AumentandoDificuldade();
        }
    }

    private void Estado3()
    {
        meuRB.velocity = Vector2.zero;

        if (timerTiro <= 0f)
        {
            Atirando1();
            Atirando2();
            AumentandoDificuldade();
        }
    }

    private void Atirando2()
    {
        var player = FindObjectOfType<PlayerController>();

        if (player)
        {
            // Instanciando meu tiro
            var tiro = Instantiate(meuTiro2, posicaoCentro.position, transform.rotation);

            // Encontrando a dire��o
            Vector2 direcao = player.transform.position - tiro.transform.position;
            direcao.Normalize();

            // Dando a dire��o e a velocidade do tiro
            tiro.GetComponent<Rigidbody2D>().velocity = direcao * velocidadeTiro;

            // Dando o angulo do tiro
            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            tiro.transform.rotation = Quaternion.Euler(0f, 0f, angulo + 90f);
        }
    }

    private void Atirando1()
    {
        var tiro = Instantiate(meuTiro1, posicaoDireita.position, transform.rotation);
        tiro.GetComponent<Rigidbody2D>().velocity = Vector2.down * velocidadeTiro;

        tiro = Instantiate(meuTiro1, posicaoEsquerda.position, transform.rotation);
        tiro.GetComponent<Rigidbody2D>().velocity = Vector2.down * velocidadeTiro;
    }    

    private void TrocaEstado()
    {        
        if (timerEstado <= 0f)
        {
            int indiceEstado = Random.Range(0, estados.Length);

            estado = estados[indiceEstado];

            timerEstado = 5f;
        }
        else
        {
            timerEstado -= Time.deltaTime;
        }
    }
}
