using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseMultipleLevel : LevelManager
{
    public PaintChecker[] checkers;

    private void Start() {
        checkers = GetComponentsInChildren<PaintChecker>();
    }

    public override void Win() {
        //base.Win();
        for (int i = 0; i < checkers.Length; i++) {
            checkers[i].draw.isDrawing = false;
            checkers[i].gameObject.SetActive(false);
        }

        Gameplay.Instance.Win();
        skeletonAnimation.AnimationName = "win";
        skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
    }

    public void Reset() {
        for (int i = 0; i < checkers.Length; i++) {
            checkers[i].draw.ClearDraw();
            checkers[i].StartChecking();
            //checkers[i].gameObject.SetActive(false);
        }
    }
}
