using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inscripted : MonoBehaviour
{
    public float forca = 15f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colidiu com: " + other.name);
    }
}

