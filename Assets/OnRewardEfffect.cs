using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRewardEfffect : MonoBehaviour
{
    public GameObject bongDen;
    public GameObject bongDen2;
    public GameObject bongDen3;

    public RectTransform firstLocation;
    public RectTransform finishPlace;
    public RectTransform middlePlace;
    public RectTransform middlePlace2;

    public void OnRewards()
    {
        Debug.Log("Calling");
        LeanTween.move(bongDen, finishPlace, 1.5f).setOnComplete(() =>
        {

            LeanTween.scale(bongDen, new Vector3(0, 0, 0), .5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {
                LeanTween.move(bongDen, firstLocation, 0f).setEase(LeanTweenType.easeInBack);
                LeanTween.scale(bongDen, new Vector3(0.65528f, 0.65528f, 0.65528f), 0f).setEase(LeanTweenType.easeInBack);
                GameSystem.userdata.gold += 5f;

            });
        });

        LeanTween.move(bongDen2, middlePlace, .5f).setOnComplete(() =>
        {
            LeanTween.move(bongDen2, finishPlace, 1f).setOnComplete(() =>
            {

                LeanTween.scale(bongDen2, new Vector3(0, 0, 0), .5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
                {
                    LeanTween.scale(bongDen2, new Vector3(0.65528f, 0.65528f, 0.65528f), 0f).setEase(LeanTweenType.easeInBack);

                    LeanTween.move(bongDen2, firstLocation, 0f).setEase(LeanTweenType.easeInBack);
                });
                
            });
        });

        LeanTween.move(bongDen3, middlePlace2, .5f).setOnComplete(() =>
        {
            LeanTween.move(bongDen3, finishPlace, 1f).setOnComplete(() =>
            {
                LeanTween.scale(bongDen3, new Vector3(0, 0, 0), .5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
                {

                    LeanTween.scale(bongDen3, new Vector3(0.65528f, 0.65528f, 0.65528f), 0f).setEase(LeanTweenType.easeInBack);

                    LeanTween.move(bongDen3, firstLocation, 0f).setEase(LeanTweenType.easeInBack);
                });
                
            });
        });
    }
}
