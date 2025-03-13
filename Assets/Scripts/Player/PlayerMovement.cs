using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveRunningSpeed;
    [SerializeField] float gravity = -9.81f;

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();    
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        MoveAndRotate();

        SetAnimation();
    }

    void SetAnimation()
    {
        float targetSpeed = (playerInput.movement != Vector2.zero) ? 
            (playerInput.isRunning ? 1 : 0.5f) : 0;

        animator.SetFloat("MoveSpeed", targetSpeed, 0.15f, Time.deltaTime);
    }

    void MoveAndRotate()
    {
        // Dirección de movimiento
        Vector3 movement = new Vector3(playerInput.movement.x, 0, playerInput.movement.y);

        // Traslación horizontal del jugador
        float speed = playerInput.isRunning ? moveRunningSpeed : moveSpeed;
        characterController.Move(movement * speed * Time.deltaTime);

        // Gravedad
        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.up * gravity * Time.deltaTime);
        }

        // Rotación del jugador
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
