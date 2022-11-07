using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DarkcupGames;

public class DragAndDropScene : LevelManager
{
    public bool win = false;

    public GameObject[] checkPosition;
    public GameObject[] objectToCheck;

    public string correctName;

    public AudioSource audioSource;
    public AudioClip wrongSFX;
    public AudioClip rightSFX;
    
    void Start()
    {
        StartCoroutine(IELoadNormalAnims());
    }

    void Update()
    {
        if (win) return;

        foreach(GameObject item in objectToCheck)
        {
            var distance = Vector2.Distance(checkPosition[0].transform.position, item.transform.position);
            if (distance < Constants.DRAG_ITEM_CORRECT_RANGE)
            {
                if (item.name == correctName)
                {
                    win = true;
                    StartCoroutine(IEWin(item));
                }
                else
                {
                    if (audioSource.isPlaying) audioSource.Stop();
                    audioSource.PlayOneShot(wrongSFX);
                }
            }
        }
    }

    IEnumerator IEWin(GameObject item) {
        LeanTween.move(item.gameObject, checkPosition[0].transform.position, 1f);
        yield return new WaitForSeconds(1f);
        item.gameObject.SetActive(false);
        if (audioSource.isPlaying) audioSource.Stop();
        Gameplay.Instance.Win(this);
    }

    IEnumerator IELoadNormalAnims()
    {
        for (int i = 0; i < objectToCheck.Length; i++) {
            objectToCheck[i].transform.localScale = Vector2.zero;
        }

        Spine.Animation normal = animBefore.Skeleton.Data.FindAnimation("normal");
        animBefore.AnimationState.SetAnimation(0, "normal", false);
        yield return new WaitForSeconds(normal.Duration);

        normal = animBefore.Skeleton.Data.FindAnimation("normal_idle");
        animBefore.AnimationState.SetAnimation(0, "normal_idle", true);

        yield return new WaitForSeconds(normal.Duration);

        for (int i = 0; i < objectToCheck.Length; i++) {
            objectToCheck[i].transform.localScale = Vector2.zero;
            EasyEffect.Appear(objectToCheck[i].gameObject, 0f, 1f, speed: 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
        //animBefore.AnimationName = "normal_idle";
        //animBefore.loop = true;
    }

    public override Vector3 GetGuidePosition() {
        return checkPosition[0].transform.position;
    }
}
