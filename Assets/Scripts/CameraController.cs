using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;
        public float smoothTime = 0.2f;

        private Vector3 offset;
        private Vector3 velocity = Vector3.zero;

        private void Start()
        {
            offset = transform.position - player.position;
        }

        void LateUpdate()
        {
            if (player == null)
                return;

            Vector3 targetPosition = player.position + offset;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                smoothTime
            );
        }
    }
}