using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseMultipleLevel : LevelManager
{
    public List<PaintChecker> checkers;

    public override void Win() {
        //base.Win();
        for (int i = 0; i < checkers.Count; i++) {
            checkers[i].draw.isDrawing = false;
            checkers[i].gameObject.SetActive(false);
        }

        Gameplay.Instance.Win();

        //draw.isDrawing = false;
        //draw.gameObject.SetActive(false);

        //EraserShowPosition erase = draw.GetComponent<EraserShowPosition>();
        //if (erase != null) {
        //    erase.eraser.SetActive(false);
        //}

        skeletonAnimation.AnimationName = "win";
        skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
    }

    public void Reset() {
        for (int i = 0; i < checkers.Count; i++) {
            //checkers[i].draw.
            checkers[i].gameObject.SetActive(false);
        }

    }
}
