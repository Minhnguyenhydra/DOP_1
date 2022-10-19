using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DarkcupGames;

public class Loading : MonoBehaviour
{
    public Image fillImage;

    private void Start() {
        LeanTween.value(0f, 1f, 1f).setOnUpdate((float f) => {
            fillImage.fillAmount = f;
        }).setOnComplete(() => {
            Utils.ChangeScene("Home");
        });
    }

}
