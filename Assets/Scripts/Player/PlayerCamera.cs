using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Component's Reference")]
    [SerializeField] private Transform playerHead;

    [Header("Limit's Settings :")]
    [SerializeField] private float maxXRot = 90f;
    [SerializeField] private float minXRot = -75f;

    [Header("Speed settings :")]
    [SerializeField] private float rotationSpeed = 5.0f;

    // Rotations settings
    private Vector2 rotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Appliquer la vitesse de rotation
        Vector2 lookInput = new Vector2(mouseX, mouseY) * rotationSpeed * Time.deltaTime;

        rotation.x -= lookInput.y;
        rotation.y += lookInput.x;

        rotation.x = Mathf.Clamp(rotation.x, minXRot, maxXRot);

        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);

        transform.position = playerHead.position;
    }
}

