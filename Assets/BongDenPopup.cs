using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BongDenPopup : MonoBehaviour
{
    public GameObject bongDen;
    public RectTransform finishedPlace;

    // Start is called before the first frame update
    void Start()
    {
        bongDen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWatchAdsCliked()
    {
        bongDen.SetActive(true);
        LeanTween.move(bongDen, finishedPlace, 1.5f).setEase(LeanTweenType.easeInQuad);

        StartCoroutine(GoldsAdd());

    }

    public IEnumerator GoldsAdd()
    {
        yield return new WaitForSeconds(1.5f);
        LeanTween.scale(bongDen, new Vector3(0f, 0f, 0f), .5f).setEase(LeanTweenType.easeInBack);
        GameSystem.userdata.gold += 5f;
        yield return new WaitForSeconds(1.6f);
        Gameplay.Instance.Next();
    }
}
