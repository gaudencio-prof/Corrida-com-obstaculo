using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Pulo")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Dash")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;

    private CharacterController controller;

    private Vector3 velocity;
    private Vector3 moveDirection;

    private bool isDashing = false;
    private float dashTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Verifica se está no chão
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Rotaciona na direção do movimento
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }

        float currentSpeed = isDashing ? dashSpeed : moveSpeed;

        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
                isDashing = false;
        }

        // Pulo
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}