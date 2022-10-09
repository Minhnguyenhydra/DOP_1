using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class EraseManyTimes : MonoBehaviour
{
    public List<EraseManyPositionInOneLevel> eraseLevels;

    public int currentLevel = 0;

    private void Start()
    {
        //eraseLevels = new List<EraseManyPositionInOneLevel>();
        //eraseLevels.AddRange(gameObject.GetComponentsInChildren<EraseManyPositionInOneLevel>());

        eraseLevels.UpdateSelected(0, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));


    }

    public void FinishDraw()
    {
        if (currentLevel >= eraseLevels.Count - 1)
        {
            Gameplay.Instance.Win(eraseLevels[eraseLevels.Count - 1]);
        } else
        {
            currentLevel++;
            eraseLevels.UpdateSelected(currentLevel, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));
        }
    }
}
