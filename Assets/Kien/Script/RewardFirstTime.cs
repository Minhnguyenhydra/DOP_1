using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardFirstTime : MonoBehaviour
{
    public GameObject bongDen;

    public RectTransform finishedPlace;

    // Start is called before the first frame update
    void Start()
    {
        bongDen.SetActive(true);
        LeanTween.move(bongDen, finishedPlace, 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            bongDen.SetActive(false);
            GameSystem.userdata.gold += 5;
            GameSystem.SaveUserDataToLocal();
        }).setDelay(0.5f);
    }

}
