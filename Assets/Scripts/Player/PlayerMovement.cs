using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;
    private float speed;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxVelocity = 10f;
    private float velocityMultiplier;
    [SerializeField] private float airVelocityMultiplier;
    [SerializeField] private float groundVelocityMultiplier;
    private bool isRunning = false;

    [Header("Ground Check")]
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airDrag = 1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask, platformMask;
    [SerializeField] private float maxSlopeAngle = 50f;
    [SerializeField] private float slopeHelpY;

    [Header("Mobile")]
    [SerializeField] private MobileUI mobileUI;
    [SerializeField] private Joystick joystick;


    private Vector3 inputDirection;
    private bool canMove;
    private Rigidbody rb;
    private PlayerInput playerInput;
    private bool isGrounded;


    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        IsGrounded();

        velocityMultiplier = isGrounded ? groundVelocityMultiplier : airVelocityMultiplier;
        rb.linearDamping = isGrounded ? groundDrag : airDrag;

        isRunning = playerInput.actions["Sprint"].IsPressed();
        speed = isRunning ? sprintSpeed : walkSpeed;

        if (playerInput.actions["Jump"].triggered)
            Jump();
    }

    public void Jump()
    {
        if (isGrounded)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void IsGrounded()
    {
        isGrounded = false;
        transform.parent = null;

        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, groundDistance);

        foreach (var hitCollider in hitColliders)
        {
            if (IsInLayerMask(hitCollider.gameObject, groundMask))
                isGrounded = true;
            if (IsInLayerMask(hitCollider.gameObject, platformMask))
                transform.parent = hitCollider.transform;
            if (isGrounded && transform.parent != null)
                break;
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        Vector2 input = mobileUI.isMobile ? new Vector2(joystick.Horizontal, joystick.Vertical) : playerInput.actions["Move"].ReadValue<Vector2>();
        inputDirection = new Vector3(input.x, 0f, input.y).normalized;

        if (inputDirection.magnitude < 0.1f)
            return;

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        moveDirection.Normalize();

        Quaternion newRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10.0f);

        Vector3 adjustedDirection = AdjustDirectionForSlope(moveDirection);
        rb.AddForce(adjustedDirection * speed * velocityMultiplier, ForceMode.Force);

        if (rb.linearVelocity.magnitude > maxVelocity)
            rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
    }

    private Vector3 AdjustDirectionForSlope(Vector3 direction)
    {
        if (Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit slopeHit, groundDistance, groundMask))
        {
            Vector3 slopeNormal = slopeHit.normal;
            float slopeAngle = Vector3.Angle(Vector3.up, slopeNormal);

            if (slopeAngle > 0 && slopeAngle <= maxSlopeAngle)
                return direction + Vector3.up * slopeHelpY;
        }
        return direction;
    }
    bool IsInLayerMask(GameObject obj, LayerMask mask) { return (mask.value & (1 << obj.layer)) != 0; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
