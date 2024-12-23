using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 respawnPos;
    private Quaternion respawnRot;
    [SerializeField] private float limit = -0.1f;
    

    private void Start()
    {
        SetRespawn(transform.position, transform.rotation);
    }

    private void Update()
    {
        if (transform.position.y <= limit)
            GoToRespawnPoint();
    }

    private void GoToRespawnPoint()
    {
        transform.position = respawnPos;
        transform.rotation = respawnRot;
    }

   public void SetRespawn(Vector3 newPos, Quaternion newRot)
    {
        respawnPos = newPos;
        respawnRot = newRot;
    }
}
