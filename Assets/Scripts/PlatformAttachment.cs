using UnityEngine;


public class PlatformAttachment : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Plataforma"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Plataforma"))
        {
            transform.SetParent(null);
        }
    }
}
