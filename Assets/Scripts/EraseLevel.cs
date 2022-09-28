using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DarkcupGames;

public class EraseLevel : LevelManager
{
    public DrawController draw;

    public SkeletonAnimation skeletonAnimation;

    private void Start() {
        StartCoroutine(IELevel1());
    }

    public IEnumerator IELevel1() {
        skeletonAnimation.AnimationName = "normal";

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
        Gameplay.Instance.PlayCongratulationEffect();

        draw.gameObject.SetActive(false);
        skeletonAnimation.AnimationName = "win";
    }
}
