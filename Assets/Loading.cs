using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DarkcupGames;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Image fillImage;

    private IEnumerator Start() {
        var load = SceneManager.LoadSceneAsync("Home");
        load.allowSceneActivation = false;
        LeanTween.value(0f, 1f, 1f).setOnUpdate((float f) => {
            fillImage.fillAmount = f;
        });
        yield return new WaitForSeconds(2f);
        load.allowSceneActivation = true;
    }

}
