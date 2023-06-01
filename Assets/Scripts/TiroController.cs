using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{
    private Rigidbody2D meuRB;

    // Pegando impacto do tiro
    [SerializeField] private GameObject impacto;

    // Pegando os sons dos tiros
    [SerializeField] private AudioClip somTiro;
    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();

        AudioSource.PlayClipAtPoint(somTiro, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checando a tag
        if (collision.CompareTag("Inimigo") || collision.CompareTag("Boss")) collision.GetComponent<InimigoPai>().PerdeVida(1);

        if (collision.CompareTag("Jogador")) collision.GetComponent<PlayerController>().PerdeVida(1);       

        if (!collision.CompareTag("Destruidor") && collision.transform.position.y <= 5) Instantiate(impacto, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
