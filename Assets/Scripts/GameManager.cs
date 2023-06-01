using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        // Garantindo que só existe um game manager por vez
        int contagem = FindObjectsOfType<GameManager>().Length;

        if (contagem > 1)
        {
            Destroy(gameObject);
        }

        // Eu não vou ser destruido quando mudar de cena
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Criando o metodo para entra no jogo
    public void IniciaJogo()
    {
        // Carregar a cena do jogo
        SceneManager.LoadScene(1);
    }

    // Criando um metodo que roda depois de um certo tempo
    IEnumerator PrimeiraCena()
    {
        yield return new WaitForSeconds(2f);
        //Todo código só vai rodar depois de 2 segundos
        SceneManager.LoadScene(0);

    }

    // Criando o metodo para ir para tela inicio
    public void Inicio()
    {
        // Iniciando minha co-rotina
        StartCoroutine(PrimeiraCena());
    }

    public void Saindo()
    {
        Application.Quit();
    }
}
