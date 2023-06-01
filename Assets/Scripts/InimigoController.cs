using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : InimigoPai
{
    private Rigidbody2D meuRB;

    // Meu tiro
    [SerializeField] private GameObject meuTiro;

    // Pegando a posição do meu tiro
    [SerializeField] private Transform posicaoTiro;

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

    }

    private void Atirar()
    {
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;

        if (visivel)
        {
            // Time do tiro
            timerTiro -= Time.deltaTime;

            if (timerTiro <= 0)
            {
                // Instanciando meu tiro
                var tiro = Instantiate(meuTiro, posicaoTiro.position, transform.rotation);

                tiro.GetComponent<Rigidbody2D>().velocity = Vector2.down * velocidadeTiro;

                timerTiro = Random.Range(1.5f, 3f);
            }
        }
    }    
}
