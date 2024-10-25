using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f *3;
    Vector3 velocity;
    public float jumpHeight = 3f;
    // reference what to check
    public Transform groundCheck;
    // Radius of checking Sphere
    public float groundDistance = 0.4f;
    // control what objects it should check for
    public LayerMask groundMask;

    bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        // creates tiny sphere to check what it collides with
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0 )
        {
            velocity.y = -2f;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

        controller.Move(move * speed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            // v = Wurzel aus h*-2*g     h = wie hoch man springen soll g ist ein Gravitationskraft
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
