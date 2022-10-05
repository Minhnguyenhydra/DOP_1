using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DarkcupGames;

public class EraseLevel : LevelManager
{
    public PaintToSpriteMaskController draw;

    //public SkeletonAnimation skeletonAnimation;

    private void Start() {
        StartCoroutine(IELevel1());
    }

    public IEnumerator IELevel1() {
        animAfter.AnimationName = "normal";

        yield return new WaitUntil(() => {
            return draw.IsDrawFinished();
        });

        Win();

        yield return new WaitForSeconds(2f);    
    }

    public override void Hint() {
        throw new System.NotImplementedException();
    }

    public override void Win() {
        draw.isDrawing = false;
        draw.gameObject.SetActive(false);

        EraserShowPosition erase = draw.GetComponent<EraserShowPosition>();
        if (erase != null) {
            erase.eraser.SetActive(false);
        }
        Gameplay.Instance.Win(this);

        //skeletonAnimation.AnimationName = "win";
        //skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
    }
}
