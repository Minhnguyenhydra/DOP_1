using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DarkcupGames;

public class ClaimGiftController : MonoBehaviour
{
    public List <DailyGiftBouder> daysReward = new List<DailyGiftBouder>();
   // int Index = 0;
    public Button claimButton;
    public RectTransform destination;
    public Sprite normalImage;
    public Sprite disableImage;
    [SerializeField] GameObject dailyPanel;
    // Update is called once per frame

    public void BtnShowDailyGift()
    {
        EasyEffect.Appear(dailyPanel, 0f, 1f);
        click = false;
        Display();
    }
    void Display()
    {
        for(int i = 0; i < daysReward.Count; i ++)
        {
            daysReward[i].Display();
        }
        if (Datacontroller.instance.saveData.canTakeDailyGift)
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

    //private void Start()
    //{

    //}
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        GameSystem.userdata.nextDay = DateTime.Now.Ticks;
    //    }
    //}

    //public void CheckExtended()
    //{

    //    if(DateTime.Now.Ticks > GameSystem.userdata.nextDay)
    //    {

    //        claimButton.enabled = true;
    //        claimButton.GetComponent<Image>().sprite = normalImage;


    //    }
    //    else
    //    {
    //        claimButton.enabled = false;
    //        claimButton.GetComponent<Image>().sprite = disableImage;
    //    }
    //}
    void Reward(int coinAdd)
    {
        GameSystem.userdata.gold += coinAdd;

        Datacontroller.instance.saveData.currentDailyGift++;
        Datacontroller.instance.saveData.canTakeDailyGift = false;
        Datacontroller.instance.saveData.oldDay = System.DateTime.Now;
        GameSystem.SaveUserDataToLocal();

        Debug.LogError(Datacontroller.instance.saveData.currentDailyGift + ":" + Datacontroller.instance.saveData.canTakeDailyGift);
        click = false;
    }
    public void BtnClose()
    {
        if (!click)
            EasyEffect.Disappear(dailyPanel, 1f, 0f);
    }
    bool click;
    public void OnRewards()
    {
        // GameSystem.userdata.nextDay = DateTime.Now.AddDays(1).Ticks;
        if (click)
            return;
        if (Datacontroller.instance.saveData.canTakeDailyGift == false)
            return;
        click = true;
        daysReward[Datacontroller.instance.saveData.currentDailyGift].Display();
        claimButton.enabled = false;
        claimButton.GetComponent<Image>().sprite = disableImage;
        GameObject lightBulb = daysReward[Datacontroller.instance.saveData.currentDailyGift].reward;
        LeanTween.scale(lightBulb, new Vector3(1.35f, 1.35f, 1.35f), .5f).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
        {
            LeanTween.move(lightBulb, destination, 1f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {

                LeanTween.scale(lightBulb, new Vector3(0, 0, 0), 1f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
                {
                    if (Datacontroller.instance.saveData.currentDailyGift == daysReward.Count - 1)
                    {
                        Reward(100);
                        return;
                    }
                    Reward(10);
                });
            });
        });
        Debug.Log("Rewarded");
    }
}
