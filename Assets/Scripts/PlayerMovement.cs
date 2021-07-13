using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (CharacterController), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;        

    [Header("Player variables")]
    public float playerSpeed;
    public float jumpHeight = 1.0f;
    public float gravityValue;
    private float rotationSpeed = 10f;
    private float smoothBlend = 0.2f;
    private float walkSpeed = 4f;
    private float sprintSpeed = 7f;
    public float maxDistanceToGround;

    public LayerMask whatIsGround;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction sprintFinishAction;

    private Animator animator;
    private Transform camera;

    Vector3 preMove;
    Vector3 move;

    public bool isSprinting;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["SprintStart"];
        sprintFinishAction = playerInput.actions["SprintFinish"];
        sprintAction.performed += x => SprintStart();
        sprintFinishAction.performed += x => SprintFinish();
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main.transform;
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        move = new Vector3(input.x,0,input.y);

        //Animations hier setzen und nicht weiter runter, da in der nächsten Zeile die Werte etwas geändert werden, sodass der Anim denken würde man würde in eine etwas schräge Richtung immer gehen
        animator.SetFloat("x", move.x, smoothBlend, Time.deltaTime);
        animator.SetFloat("y", move.z, smoothBlend, Time.deltaTime);

        preMove = move;

        move = move.x * camera.right.normalized + move.z * camera.forward.normalized;
        move.y = 0f;

        if (isSprinting) { playerSpeed = sprintSpeed; }
        else playerSpeed = walkSpeed;

        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            Jump();
        }
        if(!groundedPlayer && !jumpAction.triggered)
        {
            animator.SetBool("isGrounded", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Rotation
        Quaternion rot = Quaternion.Euler(0, camera.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation,rot, rotationSpeed * Time.deltaTime);
    }

    private void SprintStart()
    {
        if (groundedPlayer)
        {
            if (preMove.z > 0f && (preMove.x > -0.8 && preMove.x < 0.8) && preMove != Vector3.zero)
            {
                isSprinting = true;
                animator.SetBool("isSprinting", true);
            }
            else SprintFinish();
        }
    }

    private void SprintFinish()
    {
        isSprinting = false;
        animator.SetBool("isSprinting", false);
    }
    private void Jump()
    {
        animator.Play("Jump Up");
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    private void LateUpdate()
    {
        animator.SetBool("isGrounded", groundedPlayer);
    }
    
}