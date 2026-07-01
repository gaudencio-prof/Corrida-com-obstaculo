using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public GameObject player;
    public GameObject plataforma;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detectado");
            collision.transform.SetParent(transform);
        }
    }
}
