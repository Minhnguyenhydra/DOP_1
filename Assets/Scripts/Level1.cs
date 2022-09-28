using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DarkcupGames;

public class Level1 : MonoBehaviour
{
    public DrawController draw;

    public SkeletonAnimation skeletonAnimation;

    private void Start() {
        StartCoroutine(IELevel1());
    }

    public IEnumerator IELevel1() {
        yield return new WaitUntil(() => {
            return draw.IsDrawFinished();
        });

        Gameplay.Instance.PlayCongratulationEffect();

        draw.gameObject.SetActive(false);
        skeletonAnimation.AnimationName = "win";

        yield return new WaitForSeconds(2f);    }
}
