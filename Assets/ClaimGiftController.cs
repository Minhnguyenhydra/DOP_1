using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class ClaimGiftController : MonoBehaviour
{
    public List <GameObject> daysReward = new List<GameObject>();
    int Index = 0;
    public Button claimButton;
    public RectTransform destination;
    public Sprite normalImage;
    public Sprite disableImage;
    // Update is called once per frame

    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameSystem.userdata.nextDay = DateTime.Now.Ticks;
        }
    }

    public void CheckExtended()
    {

        if(DateTime.Now.Ticks > GameSystem.userdata.nextDay)
        {

            claimButton.enabled = true;
            claimButton.GetComponent<Image>().sprite = normalImage;


        }
        else
        {
            claimButton.enabled = false;
            claimButton.GetComponent<Image>().sprite = disableImage;
        }
    }

    public void OnRewards()
    {
        GameSystem.userdata.nextDay = DateTime.Now.AddDays(1).Ticks;
        claimButton.enabled = false;
        claimButton.GetComponent<Image>().sprite = disableImage;
        daysReward[Index].transform.Find("btnBuy").gameObject.SetActive(false);
        GameObject lightBulb = daysReward[Index].transform.Find("reward").gameObject;
        LeanTween.scale(lightBulb, new Vector3(1.35f, 1.35f, 1.35f), .5f).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
        {
            LeanTween.move(lightBulb, destination, 1f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {

                LeanTween.scale(lightBulb, new Vector3(0, 0, 0), 1f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
                {
                    if (Index == daysReward.Count - 1)
                    {
                        GameSystem.userdata.gold += 100;
                        return;
                    }
                    GameSystem.userdata.gold += 10;
                    Index++;
                    
                });
            });
        });
        Debug.Log("Rewarded");
    }
}
