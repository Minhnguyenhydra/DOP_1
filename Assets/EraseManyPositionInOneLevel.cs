using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class EraseManyPositionInOneLevel : LevelManager
{
    public PaintChecker[] checkers;
    public Transform guidePosition;

    public SkeletonAnimation maskAnim;

    public bool isMaskable;

    private void Start() {
        checkers = GetComponentsInChildren<PaintChecker>();

        if (isMaskable)
        {
            animAfter.gameObject.SetActive(false);
            maskAnim.gameObject.SetActive(true);
        }


        for (int i = 0; i < checkers.Length; i++) {
            EraserShowPosition eraserShowPosition = checkers[i].GetComponent<EraserShowPosition>();
            if (eraserShowPosition == null) {
                eraserShowPosition = checkers[i].gameObject.AddComponent<EraserShowPosition>();

            }
        }
    }

    public override void Win() {
        Debug.Log("Win");
        for (int i = 0; i < checkers.Length; i++) {
            checkers[i].draw.isDrawing = false;
            checkers[i].gameObject.SetActive(false);
        }
        if (isMaskable)
        {
            animAfter.gameObject.SetActive(true);
            maskAnim.gameObject.SetActive(false);
        }

        Gameplay.Instance.Win(this);
    }

    public void Reset() {
        for (int i = 0; i < checkers.Length; i++)
        {
            checkers[i].draw.ClearDraw();
            checkers[i].StartChecking();
        }
        Gameplay.Instance.Virate();
    }

    public override Vector3 GetGuidePosition() {
        return guidePosition.transform.position;
    }
}