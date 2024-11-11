using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("MovementSpeed")]
    public CharacterController controller;
    public float speed;
    public float walkSpeed;
    public float sprintSpeed = 12f;
    [Header("Jumping")]
    public float gravity = -9.81f * 2;
    Vector3 velocity;
    public float jumpHeight = 3f;
    // reference what to check
    public Transform groundCheck;
    bool isGrounded;
    // Radius of checking Sphere
    public float groundDistance = 0.4f;
    // control what objects it should check for
    public LayerMask groundMask;
    public MovementState state;
    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    private void StateHandler()
    {
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            speed = crouchSpeed;
        }
        // Mode - Sprinting
        else if (isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            speed = sprintSpeed;
        }
        // Mode - Walking
        else if (isGrounded)
        {
            state = MovementState.walking;
            speed = walkSpeed;
        }
        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void Start()
    {
        startYScale = transform.localScale.y;
    }
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }
    void Update()
    {
        StateHandler();
        inputManager();
        // creates tiny sphere to check what it collides with
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        controller.Move(move() * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);



    }
    void inputManager()

    {
        //jumping
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            // v = Wurzel aus h*-2*g     h = wie hoch man springen soll g ist ein Gravitationskraft
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //crouching
        if (Input.GetKeyDown(crouchKey))
        {
            controller.height = controller.height - crouchSpeed * Time.deltaTime;

        }
        //stop crouching
        if (Input.GetKeyUp(crouchKey))
        {
            controller.height = controller.height + crouchSpeed * Time.deltaTime;
        }
    }
    Vector3 move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        return move;
    }
}