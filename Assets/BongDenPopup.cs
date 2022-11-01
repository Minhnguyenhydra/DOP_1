using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BongDenPopup : MonoBehaviour
{
    public GameObject bongDen;
    public GameObject bongDen2;
    public GameObject bongDen3;
    public RectTransform finishedPlace;
    public RectTransform middlePlace;
    public RectTransform middlePlace2;
    // Start is called before the first frame update
    void Start()
    {
        bongDen.SetActive(false);
        bongDen2.SetActive(false);
        bongDen3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWatchAdsCliked()
    {
        bongDen.SetActive(true);
        bongDen2.SetActive(true);
        bongDen3.SetActive(true);


        LeanTween.move(bongDen2, middlePlace, .5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.move(bongDen2, finishedPlace, 1f).setEase(LeanTweenType.easeInQuad);
        });

        LeanTween.move(bongDen3, middlePlace2, .5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.move(bongDen3, finishedPlace, 1f).setEase(LeanTweenType.easeInQuad);
        });

        LeanTween.move(bongDen, finishedPlace, 1.5f).setEase(LeanTweenType.easeInQuad);

        StartCoroutine(GoldsAdd());

    }

    public IEnumerator GoldsAdd()
    {
        yield return new WaitForSeconds(1.5f);
        LeanTween.scale(bongDen, new Vector3(0f, 0f, 0f), .5f).setEase(LeanTweenType.easeInBack).setOnComplete(() => 
        {

            GameSystem.userdata.gold += 5f;

        });
        LeanTween.scale(bongDen2, new Vector3(0f, 0f, 0f), .5f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(bongDen3, new Vector3(0f, 0f, 0f), .5f).setEase(LeanTweenType.easeInBack);

        yield return new WaitForSeconds(1.6f);
        Gameplay.Instance.Next();
    }
}
