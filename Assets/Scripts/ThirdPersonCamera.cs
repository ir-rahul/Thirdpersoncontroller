using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;           // Player
    public Vector3 offset = new Vector3(0f, 2f, -6f);
    public float followSpeed = 10f;
    public float rotationSpeed = 5f;

    public float mouseSensitivity = 3f;
    public float minY = -20f;
    public float maxY = 50f;

    private float yaw = 0f;  // Horizontal rotation
    private float pitch = 15f; // Vertical rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Calculate target position based on rotation
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Always look at the player
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }


}
