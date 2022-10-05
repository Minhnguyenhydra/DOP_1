using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class DrawLevel : LevelManager
{
    public PaintToSpriteController draw;
    public Collider2D checkArea;
    //public SkeletonAnimation skeletonAnimation;
    public string winAnimationName;

    private void Start() {
        StartCoroutine(IEGameplay());
    }

    public IEnumerator IEGameplay() {
        while (true) {
            yield return new WaitUntil(() => {
                return draw.isDrawing == true;
            });

            yield return new WaitUntil(() => {
                return draw.isDrawing == false;
            });

            Debug.Log("Draw finished!");

            int insideCount = 0;
            for (int i = 0; i < draw.drawPoints.Count; i++) {
                if (checkArea.OverlapPoint(draw.drawPoints[i])) {
                    insideCount++;
                }
            }

            float percent = ((float)insideCount) / draw.drawPoints.Count;

            Debug.Log("Percent = " + percent);

            if (percent > Constants.DRAW_PERCENT_REQUIRE) {
                Win();
            } else {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                draw.ClearDraw();
            }
            //if (item.Value.GetComponent<PolygonCollider2D>().OverlapPoint(CurrentCar.transform.position)) {
            //    inside = true;
            //}
        }
    }

    public override void Win() {
        //base.Win();


        //if (winAnimationName != "") {
        //    skeletonAnimation.AnimationName = winAnimationName;

        //} else {

        //}
        StartCoroutine(IEWin());
    }

    IEnumerator IEWin() {
        draw.isDrawing = false;
        draw.gameObject.SetActive(false);

        EraserShowPosition erase = draw.GetComponent<EraserShowPosition>();
        if (erase != null) {
            erase.eraser.SetActive(false);
        }

        skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;

        var animations = skeletonAnimation.Skeleton.Data.Animations;
        foreach (Spine.Animation item in animations) {
            if (item.Name == "win") {
                skeletonAnimation.AnimationName = "win";
                yield return new WaitForSeconds(item.Duration);
                break;
            }

            if (item.Name == "win1") {
                skeletonAnimation.AnimationState.SetAnimation(0, "win1", false);
                //skeletonAnimation.AnimationName = "win1";

                yield return new WaitForSeconds(item.Duration);

                Spine.Animation win2 = skeletonAnimation.Skeleton.Data.FindAnimation("win2");

                if (win2 != null) {
                    skeletonAnimation.AnimationName = "win2";
                    yield return new WaitForSeconds(win2.Duration);
                }
            }
        }

        Gameplay.Instance.Win();
    }
}
