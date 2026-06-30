using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public float altura = 2f;
    public float velocidade = 2f;

    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * velocidade) * altura;
        transform.position = posicaoInicial + new Vector3(0, y, 0);
    }
    public float forca = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direcao = (collision.transform.position - transform.position).normalized;
                rb.AddForce(direcao * forca, ForceMode.Impulse);
            }
        }
    }
}