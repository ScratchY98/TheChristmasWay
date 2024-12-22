using UnityEngine;

public class YLimit : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    [SerializeField] private float limit = -0.1f;

    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (transform.position.y <= limit)
            ResetPositionAndRotation();
    }

    private void ResetPositionAndRotation()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
