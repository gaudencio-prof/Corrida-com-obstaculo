using System.Collections;
using UnityEngine;

public class DesablerColl : MonoBehaviour
{

    public float Intervalo = 1f;

    private Collider colisor;
    private Renderer rend;

    void Start()
    {
        colisor = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        StartCoroutine(ComportamentoPlataforma());
    }



    IEnumerator ComportamentoPlataforma()
    {
        while (true)
        {
            colisor.enabled = false;
            Alternar();
            yield return new WaitForSeconds(Intervalo);
            colisor.enabled = true;
            Alternar();
            yield return new WaitForSeconds(Intervalo);
        }
    }

    public void Alternar()
    {
        if (colisor.enabled)
        {
            rend.enabled = true;
            Color cor = rend.material.color;
            cor.a = 1f;
            rend.material.color = cor;
        }
        else if (!colisor.enabled)
        {
            rend.enabled = false;
            Color cor = rend.material.color;
            cor.a = 0f;
            rend.material.color = cor;
        }
    }

}

