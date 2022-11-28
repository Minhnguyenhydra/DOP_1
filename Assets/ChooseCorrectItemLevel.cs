using DarkcupGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ChooseCorrectItemLevel : LevelManager {
    [HideInInspector]
    public bool win = false;

    public List<DragableObject> dragObjects;
    public List<Transform> correctPositions;

    public List<string> startAnims;
    public string correctName;
    public List<string> correctAnims;

    public int current = 0;

    public virtual void Start() {
        current = 0;
        StartCoroutine(IELoadNormalAnims());
    }

    void Update() {
        if (win) return;

        for (int i = 0; i < dragObjects.Count; i++) {
            var distance = Vector2.Distance(correctPositions[current].transform.position, dragObjects[i].transform.position);
            if (distance < Constants.DRAG_ITEM_CORRECT_RANGE_CHOOSE_1) {
                ShowAnim(i);

                if (dragObjects[i].name == correctName) {
                    win = true;

                    LeanTween.delayedCall(2f, () => {
                        for (int i = 0; i < dragObjects.Count; i++) {
                            dragObjects[i].gameObject.SetActive(false);
                        }
                        Gameplay.Instance.Win(this);
                    });
                }
            }
        }
    }

    public virtual void ShowAnim(int currentAnim) {
        string animName = correctAnims[currentAnim];

        if (animName.Contains(",")) {
            List<string> animNames = new List<string>();
            animNames.AddRange(animName.Split(','));
            StartCoroutine(IELoadSeriesAnim(animBefore, animNames));
            return;
        }

        animBefore.AnimationName = animName;
    }

    IEnumerator IELoadNormalAnims() {
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
                Debug.LogError($"not found animation named {startAnims[i]} in skeleton {gameObject.name}");
            }
            yield return new WaitForSeconds(anim.Duration);
        }

        for (int i = 0; i < dragObjects.Count; i++) {
            dragObjects[i].transform.localScale = Vector2.zero;
            EasyEffect.Appear(dragObjects[i].gameObject, 0f, 1f, speed: 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator IELoadSeriesAnim(SkeletonAnimation skeleton, List<string> anims) {
        Spine.Animation anim;
        skeleton.AnimationState.Data.DefaultMix = 0;
        for (int i = 0; i < startAnims.Count; i++) {
            anim = skeleton.Skeleton.Data.FindAnimation(anims[i]);
            if (anim != null) {
                skeleton.AnimationState.SetAnimation(0, anims[i], i == anims.Count - 1);
            } else {
                Debug.LogError($"not found animation named {startAnims[i]} in skeleton {gameObject.name}");
            }
            yield return new WaitForSeconds(anim.Duration);
        }
    }

    public override Vector3 GetGuidePosition() {
        return correctPositions[0].transform.position;
    }
}