using DarkcupGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropLevel : LevelManager
{
    [HideInInspector]
    public bool win = false;

    public List<DragableObject> dragObjects;
    public List<Transform> correctPositions;

    public List<string> startAnims;
    public List<string> correctNames;
    public List<string> correctAnims;

    public AudioSource audioSource;
    public AudioClip wrongSFX;
    public AudioClip rightSFX;

    Dictionary<SpriteRenderer, bool> founds;

    public int current = 0;
    public bool changeSkin = false;
    
    public virtual void Start()
    {
        current = 0;
        founds = new Dictionary<SpriteRenderer, bool>();
        StartCoroutine(IELoadNormalAnims());
    }

    void Update()
    {
        if (win) return;

        if (current >= correctNames.Count) return;

        foreach (DragableObject dragObj in dragObjects)
        {
            if (current >= correctNames.Count) continue;
            if (dragObj.isCorrect) continue;

            var distance = Vector2.Distance(correctPositions[current].transform.position, dragObj.transform.position);
            if (distance < Constants.DRAG_ITEM_CORRECT_RANGE)
            {
                if (dragObj.name == correctNames[current])
                {
                    LeanTween.move(dragObj.gameObject, correctPositions[current].transform.position, 1f).setOnComplete(() => {
                        dragObj.gameObject.SetActive(false);
                    });
                    dragObj.isCorrect = true;
                    dragObj.enabled = false;
                    ShowCorrectAnim(current);
                    current++;
                }
                else
                {
                    if (audioSource.isPlaying) audioSource.Stop();
                    audioSource.PlayOneShot(wrongSFX);
                }
            }
        }

        if (current == correctNames.Count) {
            win = true;

            LeanTween.delayedCall(2f, () => {
                if (audioSource.isPlaying) audioSource.Stop();
                for (int i = 0; i < dragObjects.Count; i++) {
                    dragObjects[i].gameObject.SetActive(false);
                }
                Gameplay.Instance.Win(this);
            });
        }
    }

    public virtual void ShowCorrectAnim(int currentAnim) {
        if (current < correctAnims.Count) {
            if (changeSkin) {
                //change skin
                animBefore.Skeleton.SetSkin(correctAnims[current]);
                animBefore.Skeleton.SetSlotsToSetupPose();
                animBefore.LateUpdate();
            } else {
                //change anim
                animBefore.AnimationName = correctAnims[current];
            }
        } else {
            Debug.LogError($"Not enough anim to change in {gameObject.name}");
        }
    }

    IEnumerator IELoadNormalAnims()
    {
        for (int i = 0; i < dragObjects.Count; i++) {
            dragObjects[i].transform.localScale = Vector2.zero;
        }

        Spine.Animation anim;
        animBefore.AnimationState.Data.DefaultMix = 0;
        for (int i = 0; i < startAnims.Count; i++) {
            anim = animBefore.Skeleton.Data.FindAnimation(startAnims[i]);
            if (anim != null) {
                animBefore.AnimationState.SetAnimation(0, startAnims[i], i == startAnims.Count - 1);
            } else {
                Debug.LogError($"not found animation named {startAnims[i]} in skeleton {animBefore.gameObject.name}");
            }
            yield return new WaitForSeconds(anim.Duration);
        }

        for (int i = 0; i < dragObjects.Count; i++) {
            dragObjects[i].transform.localScale = Vector2.zero;
            EasyEffect.Appear(dragObjects[i].gameObject, 0f, 1f, speed: 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public override Vector3 GetGuidePosition() {
        if (current >= correctNames.Count) {
            return correctPositions[0].transform.position;
        }
        return correctPositions[current].transform.position;
    }
}