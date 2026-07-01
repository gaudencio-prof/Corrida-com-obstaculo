using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float maxAcceleration = 25f;
    [SerializeField] private float brakingAcceleration = 35f;
    [SerializeField] private float rotationSpeed = 15f;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Dash")]
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashCooldown = 0.5f;

    private Rigidbody rb;

    private Vector3 inputDirection;

    private int groundContacts;
    private float dashTimer;

    public bool Grounded => groundContacts > 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        ReadInput();
        Rotate();

        dashTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && Grounded)
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0f)
            Dash();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(h, 0f, v).normalized;
    }

    private void Rotate()
    {
        if (inputDirection.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(inputDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 horizontalVelocity = new Vector3(
            rb.velocity.x,
            0f,
            rb.velocity.z);

        // Sem input -> freia
        if (inputDirection.sqrMagnitude < 0.01f)
        {
            ApplyAcceleration(
                -horizontalVelocity,
                brakingAcceleration);

            return;
        }

        // Velocidade desejada
        Vector3 desiredVelocity = inputDirection * moveSpeed;

        // Erro de velocidade
        Vector3 velocityError = desiredVelocity - horizontalVelocity;

        ApplyAcceleration(
            velocityError,
            maxAcceleration);
    }

    private void ApplyAcceleration(Vector3 velocityError, float maxAccel)
    {
        Vector3 acceleration = Vector3.ClampMagnitude(
            velocityError / Time.fixedDeltaTime,
            maxAccel);

        rb.AddForce(acceleration * rb.mass, ForceMode.Force);
    }

    private void Jump()
    {
        Vector3 velocity = rb.velocity;
        velocity.y = 0f;
        rb.velocity = velocity;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Dash()
    {
        dashTimer = dashCooldown;

        Vector3 dashDirection = inputDirection.sqrMagnitude > 0.01f
            ? inputDirection
            : transform.forward;

        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }

    public void Impulsionar(Vector3 impulso)
    {
        rb.AddForce(impulso, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsGround(other.gameObject))
            return;

        groundContacts++;
    }

    private void OnCollisionExit(Collision other)
    {
        if (!IsGround(other.gameObject))
            return;

        groundContacts = Mathf.Max(0, groundContacts - 1);
    }

    private bool IsGround(GameObject obj)
    {
        return (groundLayer.value & (1 << obj.layer)) != 0;
    }
}