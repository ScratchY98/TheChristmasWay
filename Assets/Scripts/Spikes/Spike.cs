using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string destroySpikeTag = "DestroySpike";


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            GameManager.instance.Respawn();
        else if (other.CompareTag(destroySpikeTag))
            Destroy(gameObject, 0f);
    }
}
