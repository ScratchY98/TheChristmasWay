using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class GiftBox : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    [SerializeField] private Animator animator;
    [SerializeField] private string DestroyAnimationName = "DestroyBox";
    private bool isDestroy = false;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag) || isDestroy)
            return;

        GameManager.instance.UpgradeGiftScore();
        isDestroy = true;
        StartCoroutine(Destroy());
    }


    private IEnumerator Destroy()
    {
        animator.SetTrigger("DestroyBox");

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(DestroyAnimationName)){
            yield return null; }


        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) {
            yield return null; }

        Destroy(this.gameObject);
    }

}
