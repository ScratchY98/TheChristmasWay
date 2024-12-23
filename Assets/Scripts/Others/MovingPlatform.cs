using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3[] waypoints;

    private Vector3 target;
    private int destPoint = 0;

    private void Start()
    {
        target = waypoints[0];
    }

    private void Update()
    {
        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target) >= 0.3f)
            return;

        destPoint = (destPoint + 1) % waypoints.Length;
        target = waypoints[destPoint];
    }
}