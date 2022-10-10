using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class EraseManyTimes : MonoBehaviour
{
    public List<EraseManyPositionInOneLevel> eraseLevels;

    public GameObject buttonErase;
    public GameObject buttonWatchAds;

    public int currentLevel = 0;

    private void Start()
    {
        eraseLevels.UpdateSelected(0, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));

        buttonErase.gameObject.SetActive(true);
        buttonWatchAds.gameObject.SetActive(false);
    }

    public void FinishDraw()
    {
        Debug.Log("This is finish draw!");

        if (currentLevel < 2) {
            EasyEffect.Appear(buttonErase,0f,1f);
            buttonWatchAds.gameObject.SetActive(false);
        } else {
            EasyEffect.Appear(buttonWatchAds, 0f, 1f);
            buttonErase.gameObject.SetActive(false);
        }

        if (currentLevel >= eraseLevels.Count - 1)
        {
            buttonErase.gameObject.SetActive(false);
            buttonWatchAds.gameObject.SetActive(false);

            LeanTween.delayedCall(2f, () => {
                Gameplay.Instance.Win(eraseLevels[eraseLevels.Count - 1]);
            });
        } else
        {
            currentLevel++;
            eraseLevels.UpdateSelected(currentLevel, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));

            for (int i = 0; i < eraseLevels[currentLevel].checkers.Length; i++) {
                eraseLevels[currentLevel].checkers[i].StartChecking();
            }
        }
    }
}