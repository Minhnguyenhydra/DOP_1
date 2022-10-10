using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomLevel39 : DrawLevel
{
    public GameObject completeButton;

    public override void Start() {
        base.Start();
        completeButton.gameObject.SetActive(false);
    }

    public override void Win() {
        draw.isDrawing = false;
        draw.gameObject.SetActive(false);

        EraserShowPosition erase = draw.GetComponent<EraserShowPosition>();
        if (erase != null) {
            erase.eraser.SetActive(false);
        }
        Gameplay.Instance.Win(this, false);

        completeButton.SetActive(true);
    }

    public void OnCompleteClick() {
        Gameplay.Instance.ShowWinPopup();
    }
}
