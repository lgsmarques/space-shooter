using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMorte : MonoBehaviour
{
    [SerializeField] private GameObject explosao;
    [SerializeField] private float timer = 0f;
    [SerializeField] private GameObject sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Explosao();
    }

    private void Explosao()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Vector3 posicao = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-1f, 2f));
            Instantiate(explosao, sprite.transform.position + posicao, sprite.transform.rotation);

            posicao = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-1f, 2f));
            Instantiate(explosao, sprite.transform.position + posicao, sprite.transform.rotation);

            timer = 0.05f;
        }
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject explosao2 = explosao;

        explosao2.transform.localScale *= 10;

        Instantiate(explosao2, sprite.transform.position, sprite.transform.rotation);

        explosao2.transform.localScale /= 10;
    }
}
