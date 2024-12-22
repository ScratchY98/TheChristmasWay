using UnityEngine;

public class GiftBoxAnim : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
