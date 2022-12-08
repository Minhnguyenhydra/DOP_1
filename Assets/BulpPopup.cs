using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulpPopup : MonoBehaviour
{

    public GameObject popUpWin;
    public GameObject bongDen;
    public GameObject bongDen2;
    public GameObject bongDen3;
    public RectTransform firstLocation;
    public RectTransform finishedPlace;
    public RectTransform middlePlace;
    public RectTransform middlePlace2;

    RectTransform bongden1stLocation;
    RectTransform bongden21stLocation;
    RectTransform bongden31stLocation;
    // Start is called before the first frame update
    void Start()
    {
        bongden1stLocation = bongDen.GetComponent<RectTransform>();
        LeanTween.scale(bongDen, new Vector3(0, 0, 0),0f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(bongDen2, new Vector3(0, 0, 0), 0f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(bongDen3, new Vector3(0, 0, 0), 0f).setEase(LeanTweenType.easeInBack);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWatchAdsCliked()
    {
        LeanTween.scale(bongDen, new Vector3(1.3535f, 1.3535f, 1.3535f), 0f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(bongDen2, new Vector3(1.3535f, 1.3535f, 1.3535f), 0f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(bongDen3, new Vector3(1.3535f, 1.3535f, 1.3535f), 0f).setEase(LeanTweenType.easeInBack);
        Debug.Log("Checking");
        if(popUpWin != null)
        {
            popUpWin.gameObject.SetActive(true);

        }
        bongDen.SetActive(true);
        bongDen2.SetActive(true);
        bongDen3.SetActive(true);


        LeanTween.move(bongDen2, middlePlace, .5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.move(bongDen2, finishedPlace, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() => {
                bongDen2.SetActive(false);
                bongDen2.transform.position = firstLocation.position;
                bongDen2.transform.localScale = Vector2.one;
            });
        });

        LeanTween.move(bongDen3, middlePlace2, .5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            LeanTween.move(bongDen3, finishedPlace, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() => {
                bongDen3.SetActive(false);
                bongDen3.transform.position = firstLocation.position;
                bongDen3.transform.localScale = Vector2.one;
            });
        });

        LeanTween.move(bongDen, finishedPlace, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() => {
            GameSystem.userdata.gold += 10;
            GameSystem.SaveUserDataToLocal();
            bongDen.SetActive(false);
            bongDen.transform.position = firstLocation.position;
            bongDen.transform.localScale = Vector2.one;
        });

      //  StartCoroutine(GoldsAdd());

    }

    public IEnumerator GoldsAdd()
    {
        yield return new WaitForSeconds(0.2f);
        LeanTween.scale(bongDen, new Vector3(0f, 0f, 0f), .5f).setEase(LeanTweenType.easeInBack).setOnComplete(() => 
        {
            GameSystem.userdata.gold += 10;
            GameSystem.SaveUserDataToLocal();
            LeanTween.move(bongDen, firstLocation, 0f).setEase(LeanTweenType.easeInQuad);

        });
        LeanTween.scale(bongDen2, new Vector3(0f, 0f, 0f), .5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            LeanTween.move(bongDen2, firstLocation, 0f).setEase(LeanTweenType.easeInQuad);
        });
        LeanTween.scale(bongDen3, new Vector3(0f, 0f, 0f), .5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            LeanTween.move(bongDen3, firstLocation, 0f).setEase(LeanTweenType.easeInQuad);

        });

    }
}
