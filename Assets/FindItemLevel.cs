using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DarkcupGames;

public class FindItemLevel : LevelManager {
    public GameObject magnify;
    public List<SpriteRenderer> findObjects;
    

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
        yield return new WaitForSeconds(1f);
        Win();
    }

    public override void Win() {
        Gameplay.Instance.Win(this);
        //skeletonAnimation.AnimationName = "win";
        //skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
    }

    private void Update() {
        for (int i = 0; i < findObjects.Count; i++) {
            float distance = Vector2.Distance(findObjects[i].transform.position, magnify.transform.GetChild(0).position);

            if (distance < Constants.FIND_MANY_ITEM_FLY_TO_BOX_RANGE) {
                Found(findObjects[i]);
            }
        }

        CheckWin();
    }

    public virtual void Found(SpriteRenderer spriteRenderer) {
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
            LeanTween.scale(magnify.gameObject, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeInCubic);
            isWin = true;
            LeanTween.delayedCall(2f, () => {
                Win();
            });
        }
    }

    public override Vector3 GetGuidePosition() {
        for (int i = 0; i < findObjects.Count; i++) {
            if (founds.ContainsKey(findObjects[i]) == false || founds[findObjects[i]] == false) {
                return findObjects[i].transform.position;
            }
        }
        return findObjects[0].transform.position;
    }
}
