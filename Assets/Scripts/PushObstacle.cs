using UnityEngine;

public class PushObstacle : MonoBehaviour
{
    public float force = 7.5f;
    public float upwardForce = 0.4f;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if (player == null)
            return;

        Vector3 dir = collision.transform.position - transform.position;

        dir.y = 0;

        dir.Normalize();

        dir = Vector3.up * upwardForce + dir * force;

        player.Impulsionar(dir * force);
    }
}