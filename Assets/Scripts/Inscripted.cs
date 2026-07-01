using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inscripted : MonoBehaviour
{
    public float bounceForce = 25f;
    public float squashAmount = 0.2f;
    public float animationSpeed = 0.08f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 vel = rb.velocity;
            vel.y = 0;
            rb.velocity = vel;

            rb.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);

            StartCoroutine(BounceAnimation());
        }
    }

    IEnumerator BounceAnimation()
    {
        transform.localScale = new Vector3(
            originalScale.x + squashAmount,
            originalScale.y - squashAmount,
            originalScale.z + squashAmount
        );

        yield return new WaitForSeconds(animationSpeed);

        transform.localScale = originalScale;
    }
}


