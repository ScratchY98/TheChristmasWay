using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private Transform offset;
    [SerializeField] private Vector3 spikeVelocity = new Vector3(0, 0, -2);
    [SerializeField] private float delay = 2f;

    private void Start()
    {
        Invoke("GenerateSpike", delay);
    }

    private void GenerateSpike()
    {
        // Create the spike
        Transform spike = Instantiate(spikePrefab).transform;
        
        // Set his offset
        spike.position = offset.position;
        spike.rotation = offset.rotation;

        spike.parent = offset;

        // Get the spike's Rigidbody
        Rigidbody spikeRb = spike.gameObject.GetComponent<Rigidbody>();

        // Put the velocity of the spike
        spikeRb.linearVelocity = spikeVelocity;

        // Create another sprite after an delay
        Invoke("GenerateSpike", delay);
    }
}
