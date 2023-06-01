using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosaoController : MonoBehaviour
{
    [SerializeField] private AudioClip meuSom;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(meuSom, new Vector3(0f, 0f, 10f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destruindo()
    {
        Destroy(gameObject);
    }
}

