using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHintButton : MonoBehaviour
{
    public GameObject hintButton;
    public GameObject unHintButton;
    public GameObject goldHintButton;
    public GameObject adsHintButton;
    
    void Start()
    {
        unHintButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHintButtonClicked()
    {

        LeanTween.scale(goldHintButton, new Vector3(1, 1, 1), 0.25f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(adsHintButton, new Vector3(1, 1, 1), 0.25f).setEase(LeanTweenType.easeOutBack);
        unHintButton.gameObject.SetActive(true);
        hintButton.gameObject.SetActive(false);
    }

    public void OnUnHintButtonClicked()
    {
        LeanTween.scale(goldHintButton, new Vector3(0, 0, 0), 0.5f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(adsHintButton, new Vector3(0, 0, 0), 0.5f).setEase(LeanTweenType.easeInBack);
        hintButton.gameObject.SetActive(true);
        unHintButton.gameObject.SetActive(false) ;

    }
}
