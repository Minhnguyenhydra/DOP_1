using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class EraseManyTimes : MonoBehaviour
{
    public List<EraseManyPositionInOneLevel> eraseLevels;

    public GameObject buttonWatchAds;
    public int currentLevel = 0;
    public Transform guidePosition;

    public bool isClickadsable;

    private void Start()
    {
        eraseLevels.UpdateSelected(0, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));

        buttonWatchAds.gameObject.SetActive(false);
    }

    public void FinishDraw()
    {
        Debug.Log(eraseLevels.Count - 1);

        if (currentLevel < eraseLevels.Count -1 ) {
            buttonWatchAds.gameObject.SetActive(true);
        } else {
            EasyEffect.Appear(buttonWatchAds, 0f, 1f);
        }

        if (currentLevel >= eraseLevels.Count - 1)
        {
            buttonWatchAds.gameObject.SetActive(false);

         
            Gameplay.Instance.Win(eraseLevels[eraseLevels.Count - 1]);  
        }
        else
        {
            currentLevel++;
            if (isClickadsable)
            {
                var go = eraseLevels[currentLevel].transform.Find("check_correct");
                go.gameObject.SetActive(false);
                Debug.Log(go.name);

                eraseLevels.UpdateSelected(currentLevel, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));
            }
            else 
            {
                eraseLevels.UpdateSelected(currentLevel, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));
                for (int i = 0; i < eraseLevels[currentLevel].checkers.Length; i++)
                {
                    eraseLevels[currentLevel].checkers[i].StartChecking();
                }
            }
            

        }
    }

    public void OnWatchAdsClick()
    {
        var go = eraseLevels[currentLevel].transform.Find("check_correct");

        var eraser = go.GetComponent<EraserShowPosition>();
        if(eraser == null)
        {
        go.gameObject.AddComponent<EraserShowPosition>();

        }
        go.gameObject.SetActive(true);

        for (int i = 0; i < eraseLevels[currentLevel].checkers.Length; i++)
        {
            eraseLevels[currentLevel].checkers[i].StartChecking();
        }
        buttonWatchAds.gameObject.SetActive(false);
    }

    public void Hint() {

    }

    public void OnCloseSpecialLevel() {
        Gameplay.Instance.LevelUp();
    }

    public void OnNoButtonSelected()
    {
        Gameplay.Instance.Next();
    }
}