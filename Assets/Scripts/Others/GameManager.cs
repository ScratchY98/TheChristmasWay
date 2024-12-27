using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header ("Gifts")]
    public int maxGifts = 3;
    [SerializeField] private int currentGift;
    public bool isPlayerHasAllGifts;

    [Header ("Gifts UI")]
    [SerializeField] private GameObject giftUIPrefab;
    [SerializeField] private Transform giftUIParent;
    private List<Image> giftUIFill = new List<Image>();
    private int giftUIFIllIndex;

    [Header("Scene Changements Settings")]
    [SerializeField] private string nextLevelName = "Level";

    [SerializeField] Respawn playerRespawn;

    // Singelton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        isPlayerHasAllGifts = false;

        for (int i = 0; i < maxGifts; i++)
        {
            GameObject newGiftUI = Instantiate(giftUIPrefab);
            Transform newGiftUITransform = newGiftUI.transform;
            newGiftUITransform.SetParent(giftUIParent);
            newGiftUI.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            GameObject newGiftUIChild = newGiftUITransform.GetChild(0).gameObject;
            giftUIFill.Add(newGiftUIChild.GetComponent<Image>());
        }
    }

    public void UpgradeGiftScore()
    {
        giftUIFill[giftUIFIllIndex].fillAmount = 1;
        currentGift ++;
        giftUIFIllIndex++;

        if (currentGift == maxGifts)
            isPlayerHasAllGifts = true;
    }

    public void GoToNextLevel()
    {
        StartCoroutine(LoadSceneAsync(nextLevelName));
    }


    // Coroutine pour charger la scène asynchrone
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        Debug.Log("Scène chargée avec succès !");
    }

    public void Respawn() { playerRespawn.GoToRespawnPoint(); }
}
