using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CustomLevel42 : EraseManyPositionInOneLevel
{
    public SkeletonAnimation aterAlternative;

    public override void Win() {
        animAfter.gameObject.SetActive(false);
        aterAlternative.gameObject.SetActive(true);

        for (int i = 0; i < checkers.Length; i++) {
            checkers[i].draw.isDrawing = false;
            checkers[i].gameObject.SetActive(false);
        }

        StartCoroutine(Gameplay.Instance.IEWin(aterAlternative, winAnims));
    }
}
