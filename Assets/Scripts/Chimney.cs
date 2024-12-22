using UnityEngine;

public class Chimney : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject finishUI;
    [SerializeField] private bool canFinishTheLevel;

    private void Start()
    {
        finishUI.SetActive(false);
        canFinishTheLevel = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag) || !GameManager.instance.isPlayerHasAllGifts)
            return;

        canFinishTheLevel = true;
        finishUI.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        canFinishTheLevel = false;
        finishUI.SetActive(false);
    }

    private void Update()
    {
        if (!canFinishTheLevel)
            return;


        if (Input.GetKeyDown(KeyCode.E))
            GameManager.instance.GoToNextLevel();
    }
}
