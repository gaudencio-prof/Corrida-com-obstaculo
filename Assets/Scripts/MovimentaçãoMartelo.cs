using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaçãoMartelo : MonoBehaviour
{
    public float angulo = 65f;
    public float velocidade = 2f;

    private Quaternion rotacaoInicial;

    void Start()
    {
        rotacaoInicial = transform.rotation;
    }

    void Update()
    {
        float rotacao = Mathf.Sin(Time.time * velocidade) * angulo;
        transform.rotation = rotacaoInicial * Quaternion.Euler(0, 0, rotacao);
    }
}
