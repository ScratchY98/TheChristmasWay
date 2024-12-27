using UnityEngine;

public class MobileUI : MonoBehaviour
{
    [SerializeField] private GameObject mobileUI;
    /*[HideInInspector]*/ public bool isMobile;
    private void Start()
    {
        isMobile = false;

        if (IsMobile.staticIsWebGL)
            isMobile = IsMobile.isMobile;
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            isMobile = true;

        mobileUI.SetActive(isMobile);
    }
}
