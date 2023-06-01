using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    [SerializeField] private GameObject boss;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CriaBoss()
    {
        Instantiate(boss, transform.position, transform.rotation);
    }

    private void OnDestroy()
    {
        CriaBoss();
    }
}
