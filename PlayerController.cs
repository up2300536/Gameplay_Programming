using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform head;
    public Camera camera;

    [Header("Configurations")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f;

    private float currentSpeed;
    private float xRotation = 0f;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        MovePlayer();
        HandleMouseLook();
    }

    void MovePlayer()
    {
        // Check if Left Shift is held to run
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Get input axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Determine movement direction relative to camera
        Vector3 direction = camera.transform.forward * vertical + camera.transform.right * horizontal;
        direction.Normalize();

        // Apply velocity
        Vector3 moveVelocity = direction * currentSpeed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}

