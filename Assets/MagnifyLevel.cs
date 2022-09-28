using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class MagnifyLevel : LevelManager
{
    public GameObject magnify;
    public List<Vector2> positions;
    public SkeletonAnimation skeletonAnimation;

    public void Start() {
        Hint();
    }

    public override void Hint() {
        StartCoroutine(IEHint());
    }

    public IEnumerator IEHint() {
        for (int i = 0; i < positions.Count; i++) {
            LeanTween.move(magnify, positions[i], 1f).setEaseOutCubic();
            yield return new WaitForSeconds(1f);
        }
    }

    public override void Win() {
        Gameplay.Instance.PlayCongratulationEffect();

        //draw.gameObject.SetActive(false);
        skeletonAnimation.AnimationName = "win";
    }
}
