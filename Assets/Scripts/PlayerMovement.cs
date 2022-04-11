using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float gravity = 2f;
    public float velocity = 1f;

    public Vector2 mouseCoordinates;
    public float xSensitivity = 2.0f;
    public float ySensitivity = 3.0f;

    private CharacterController controller;
    private Vector3 direction;
    private bool inspecting;

    private const float maxXRotation = 360f;
    private const float minXRotation = -360f;
    private const float maxYRotation = 90f;
    private const float minYRotation = -90f;
    private const float defaultRotation = 0f;
    private const float jumpHeight = 0.5f;

    public AudioSource m_AudioSource;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Lock the mouse to the game window. Leave window in editor by hitting 'Esc'.
        //Cursor.lockState = CursorLockMode.Locked;
        inspecting = false;
        mouseCoordinates.x = 180f;
    }

    void Update()
    {
        if (!inspecting)
        {
            // Handle player rotation based on mouse coordinates.
            mouseCoordinates.x += Input.GetAxis("Mouse X") * xSensitivity;

            // Lock player y rotation to being able to only look straight up and down, +- 90 degrees. No flipping.
            if (mouseCoordinates.y <= maxYRotation || mouseCoordinates.y >= minYRotation)
            {
                mouseCoordinates.y += Input.GetAxis("Mouse Y") * ySensitivity;

                if (mouseCoordinates.y >= maxYRotation)
                {
                    mouseCoordinates.y = maxYRotation;
                }
                else if (mouseCoordinates.y <= minYRotation)
                {
                    mouseCoordinates.y = minYRotation;
                }
            }

            transform.localRotation = Quaternion.Euler(-mouseCoordinates.y, mouseCoordinates.x, 0);

            // Keep player horizontal rotation to a value between 0 and 360 degrees.
            if (mouseCoordinates.x >= maxXRotation || mouseCoordinates.x <= minXRotation)
            {
                mouseCoordinates.x = defaultRotation;
            }

            if (mouseCoordinates.y >= maxXRotation || mouseCoordinates.y <= minXRotation)
            {
                mouseCoordinates.y = defaultRotation;
            }
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        Vector3 transformDirection = transform.TransformDirection(inputDirection);

        inputDirection.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;


        Vector3 movement = speed * (Time.deltaTime * velocity) * transformDirection;
        direction = new Vector3(movement.x, direction.y, movement.z);

        if (m_AudioSource != null)
        {
            //Handle Walking sound effect
            if (isWalking)
            {
                if (!m_AudioSource.isPlaying)
                {
                    m_AudioSource.Play();
                }
            }
            else
            {
                m_AudioSource.Stop();
            }
        }
        // Handle jumping and falling (gravity) behaviour.
        if (Jump)
        {
            direction.y = jumpHeight;
        }
        else if (controller.isGrounded)
        {
            direction.y = 0f;
        }
        else
        {
            direction.y -= gravity * (Time.deltaTime * velocity);
        }

        controller.Move(direction);
    }

    public void flipInspecting()
    {
        inspecting = !inspecting;
    }

    // Tell character when it is jumping.
    private bool Jump => controller.isGrounded && Input.GetKey(KeyCode.Space);
}