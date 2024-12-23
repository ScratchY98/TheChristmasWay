using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Respawn playerRespawn;
    private bool isDone = false;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag) || isDone) return;

        isDone = true;
        playerRespawn.SetRespawn(transform.position, transform.rotation);
    }
}
