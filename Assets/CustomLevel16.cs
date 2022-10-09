using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevel16 : FindAndWinLevel
{
    public void Win(LevelManager level)
    {
        if (level.animBefore != null)
        {
            level.animBefore.gameObject.SetActive(false);
        }
        StartCoroutine(IEWin());
    }

    IEnumerator IEWin()
    {
        animAfter.maskInteraction = SpriteMaskInteraction.None;

        var animations = animAfter.Skeleton.Data.Animations;
        animAfter.AnimationState.Data.DefaultMix = 0;



        foreach (Spine.Animation item in animations)
        {
            Spine.Animation win = animAfter.Skeleton.Data.FindAnimation("win");
            if (win != null)
            {
                animAfter.AnimationName = "win";

                yield return new WaitForSeconds(win.Duration);
            }

            Spine.Animation win1 = animAfter.Skeleton.Data.FindAnimation("win1");
            if (win1 != null)
            {
                animAfter.AnimationName = "win1";
                yield return new WaitForSeconds(win1.Duration);
            }

            Spine.Animation win2 = animAfter.Skeleton.Data.FindAnimation("win2");
            if (win2 != null)
            {
                animAfter.AnimationName = "win2";
                yield return new WaitForSeconds(win2.Duration);
            }

            //if (item.Name == "win") {
            //    skeletonAnimation.AnimationName = "win";
            //    yield return new WaitForSeconds(item.Duration);
            //    break;
            //}

            //if (item.Name == "win1") {
            //    skeletonAnimation.AnimationState.SetAnimation(0, "win1", false);
            //    //skeletonAnimation.AnimationName = "win1";

            //    yield return new WaitForSeconds(item.Duration);

            //    Spine.Animation win2 = skeletonAnimation.Skeleton.Data.FindAnimation("win2");

            //    if (win2 != null) {
            //        skeletonAnimation.AnimationName = "win2";
            //        yield return new WaitForSeconds(win2.Duration);
            //    }
            //}
        }

        //StartCoroutine(IEPlayCongratulation());
        yield return new WaitForSeconds(1f);

        //winPopup.DoEffect();
    }
}
