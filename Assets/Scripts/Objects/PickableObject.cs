using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour
{
    [Header("Components's Reference :")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Rigidbody rb;

    [Header("Offset's Settings:")]
    [SerializeField] private Transform offset;
    [SerializeField] private bool isOffsetPos;


    [Header("Others's :")]
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private Collider objectCollider;
    [SerializeField] private Transform player;
    [SerializeField] private Transform originalParent;
    [SerializeField] bool IsDropedWhenTouchSomething = true;

    private bool isPlayerNear = false;
    private bool isTaken = false;
    private bool isAnyObjectTaken = false;
    private bool isTouchSomething = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isTaken && other.transform != player)
            isTouchSomething = true;
    }


    private void Update()
    {
        isPlayerNear = Vector3.Distance(transform.position, player.position) <= minDistance;

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isAnyObjectTaken)
        {
            isAnyObjectTaken = true;
            isTouchSomething = false;
            rb.isKinematic = true;
            objectCollider.isTrigger = true;
            transform.parent = offset;
            SetOffset();
            isTaken = true;
        }

        if (!isTaken) return;

        if (isTouchSomething && IsDropedWhenTouchSomething)
        {
            SetDownObject();
            isTouchSomething = false;
        }
        if (Input.GetMouseButtonDown(0))
            SetDownObject();
    }

    private void SetOffset()
    {
        if (!isOffsetPos) return;

        transform.position = offset.position;
        transform.rotation = offset.rotation;
    }

    private void SetDownObject()
    {
        isAnyObjectTaken = false;
        objectCollider.isTrigger = false;
        rb.isKinematic = false;
        transform.parent = originalParent;
        isTaken = false;
    }
}
