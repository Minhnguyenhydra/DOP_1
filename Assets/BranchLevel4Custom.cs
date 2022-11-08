using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class BranchLevel4Custom : LevelManager
{
    public Transform correctPos;
    public List<DragableObject> dragObjects;
    public List<SkeletonAnimation> wrongAnims;
    public string correctName;

    AudioSource audioSource;
    public AudioClip wrongSFX;
    public AudioClip rightSFX;
    int wrongCount = 1;
    public bool isShowed;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        foreach(DragableObject dragObject in dragObjects)
        {
            if (Vector2.Distance(dragObject.transform.position, correctPos.transform.position) < Constants.DRAG_ITEM_CORRECT_RANGE)
            {
                if (dragObject.name == correctName)
                {
                    animBefore.gameObject.SetActive(false);
                    foreach(SkeletonAnimation wrongAnim in wrongAnims)
                    {
                        wrongAnim.gameObject.SetActive(false);
                    }
                    dragObject.gameObject.SetActive(false);
                    animAfter.gameObject.SetActive(true);
                    StartCoroutine(IELoadWindsAnim());
                }
                else
                {
                    WrongChoiceSelection(dragObject);
                }
            }
        }
    }

    private void WrongChoiceSelection(DragableObject dragObject)
    {
        wrongCount = isShowed ? 2 : 1;
        dragObject.gameObject.SetActive(false);
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(wrongSFX);
        switch (dragObject.name)
        {
            case "Dog":
                foreach (SkeletonAnimation anim in wrongAnims)
                {
                    if (anim.name == dragObject.name)
                    {
                        anim.gameObject.GetComponent<MeshRenderer>().sortingOrder = wrongCount;

                    }
                }
                if (!isShowed)
                {
                    isShowed = true;
                }
                break;
            case "Gun":
                foreach (SkeletonAnimation anim in wrongAnims)
                {
                    if (anim.name == dragObject.name)
                    {
                        anim.gameObject.GetComponent<MeshRenderer>().sortingOrder = wrongCount;
                    }
                }
                if (!isShowed)
                {
                    isShowed = true;
                }
                break;

        }
    }

    IEnumerator IELoadWindsAnim()
    {
        float duration = gameObject.transform.Find("after").GetComponent<SkeletonAnimation>().skeleton.Data.FindAnimation("lose3").Duration;
        Debug.Log(duration);
        yield return new WaitForSeconds(duration);
        LeanTween.delayedCall(0f, () =>
        {
            animAfter.AnimationName = "win";
        }).setOnComplete(() =>
        {
            if (audioSource.isPlaying) audioSource.Stop();
            audioSource.PlayOneShot(rightSFX);
            Gameplay.Instance.Win(this);
        });
    }


}
