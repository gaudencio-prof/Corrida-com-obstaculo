using UnityEngine;

public class Trampolim : MonoBehaviour
{
    public float upForce = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Rigidbody rb = collision.rigidbody;

        rb.AddForce(transform.up * upForce, ForceMode.Impulse);
    }
}