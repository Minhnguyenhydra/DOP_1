using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseManyPositionInOneLevel : LevelManager
{
    public PaintChecker[] checkers;
    public Transform guidePosition;

    private void Start() {
        checkers = GetComponentsInChildren<PaintChecker>();

        for (int i = 0; i < checkers.Length; i++) {
            EraserShowPosition eraserShowPosition = checkers[i].GetComponent<EraserShowPosition>();
            if (eraserShowPosition == null) {
                eraserShowPosition = checkers[i].gameObject.AddComponent<EraserShowPosition>();
            }
        }
    }

    public override void Win() {
        for (int i = 0; i < checkers.Length; i++) {
            checkers[i].draw.isDrawing = false;
            checkers[i].gameObject.SetActive(false);
        }

        Gameplay.Instance.Win(this);
    }

    public void Reset() {
        for (int i = 0; i < checkers.Length; i++)
        {
            checkers[i].draw.ClearDraw();
            checkers[i].StartChecking();
        }
    }

    public override Vector3 GetGuidePosition() {
        return guidePosition.transform.position;
    }
}