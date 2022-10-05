using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DarkcupGames;

public class FindItemLevel : LevelManager {
    public GameObject magnify;
    public List<SpriteRenderer> findObjects;
    //public SkeletonAnimation skeletonAnimation;

    Dictionary<SpriteRenderer, bool> founds;

    bool isWin = false;

    public void Start() {
        //Hint();
        founds = new Dictionary<SpriteRenderer, bool>();
        for (int i = 0; i < findObjects.Count; i++) {
            founds.Add(findObjects[i], false);
        }
    }

    public override void Hint() {
        StartCoroutine(IEHint());
    }

    public IEnumerator IEHint() {
        //for (int i = 0; i < positions.Count; i++) {
        //    LeanTween.move(magnify, positions[i], 1f).setEaseOutCubic();
        //    yield return new WaitForSeconds(1f);
        //}
        yield return new WaitForSeconds(1f);
        Win();
    }

    public override void Win() {
        Gameplay.Instance.Win();
        skeletonAnimation.AnimationName = "win";
        skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
    }

    private void Update() {
        for (int i = 0; i < findObjects.Count; i++) {
            float distance = Vector2.Distance(findObjects[i].transform.position, magnify.transform.position);

            if (distance < Constants.FIND_ITEM_RANGE) {
                Found(findObjects[i]);
            }
        }

        CheckWin();
    }

    public void Found(SpriteRenderer spriteRenderer) {
        if (founds[spriteRenderer] == false) {
            founds[spriteRenderer] = true;
            spriteRenderer.gameObject.SetActive(false);
            Gameplay.Instance.FoundItem(spriteRenderer);
        }
    }

    public void CheckWin() {
        if (isWin) return;

        bool win = true;

        foreach (var item in founds) {
            if (item.Value == false) {
                win = false;
                break;
            }
        }

        if (win) {
            isWin = true;
            LeanTween.delayedCall(2f, () => {
                Win();
            });
        }
    }
}
