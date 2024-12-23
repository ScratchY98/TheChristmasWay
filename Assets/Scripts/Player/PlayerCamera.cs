using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("Component's Reference")]
    [SerializeField] private Transform playerHead;

    [Header("Limit's Settings :")]
    [SerializeField] private float maxXRot = 90f;
    [SerializeField] private float minXRot = -75f;

    [Header("Speed settings :")]
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float gamepadSensitivity = 1f;

    [Header("Input Settigs")]
    [SerializeField] private PlayerInput playerInput;

    // Rotations settings
    private Vector2 rotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {

        float sensitivity = playerInput.currentControlScheme == "Keyboard" ? mouseSensitivity : gamepadSensitivity;

        Vector2 lookInput = playerInput.actions["Look"].ReadValue<Vector2>();

        // Appliquer la vitesse de rotation
        Vector2 lookOutput = new Vector2(lookInput.x * sensitivity, lookInput.y * sensitivity) * rotationSpeed * Time.deltaTime;

        rotation.x -= lookOutput.y;
        rotation.y += lookOutput.x;

        rotation.x = Mathf.Clamp(rotation.x, minXRot, maxXRot);

        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);

        transform.position = playerHead.position;
    }

    public void ChangeSensitivity(float newSensitivity, bool isGamepad)
    {
        if (isGamepad) gamepadSensitivity = newSensitivity;
        else mouseSensitivity = newSensitivity;
    }
}

