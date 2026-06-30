using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesablerColl : MonoBehaviour
{
 {
    public float Intervalo = 1f;

    private Collider colisor;
    private Renderer rend;

    void Start()
    {
        colisor = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
       colisor.enabled;
       Alternar();
       yield return new WaitForSeconds(Intervalo);
       colisor.disabled;
       Alternar();
       yield return new WaitForSeconds(Intervalo); 
    }

    public void Alternar()
    {
        if(colisor.enabled)
        {
            rend.enabled = true;
            Color cor = rend.material.color;
            cor.a = 1f;
            rend.material.color = cor;         
        }
            else if(!colisor.enabled)
            {
                rend.enabled = false;
                Color cor = rend.material.color;
                cor.a = 0f;
                rend.material.color = cor;
            }       
    }
 }
}

