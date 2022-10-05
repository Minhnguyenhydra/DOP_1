using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAndWinLevel : LevelManager
{
    public GameObject magnify;
    public SpriteRenderer findObject;

    bool isWin = false;

    public void Start() {
        //Hint();
        //founds = new Dictionary<SpriteRenderer, bool>();
        //for (int i = 0; i < findObjects.Count; i++) {
        //    founds.Add(findObjects[i], false);
        //}
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
        CheckWin();
    }

    public void CheckWin() {
        if (isWin) return;

        float distance = Vector2.Distance(findObject.transform.position, magnify.transform.position);

        isWin = distance < Constants.FIND_ITEM_RANGE;

        if (isWin) {
            Win();
        }
    }
}
