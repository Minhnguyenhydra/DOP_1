using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyGiftBouder : MonoBehaviour
{
    public int index;
    public GameObject lockObj, unlockObj;
    public GameObject reward;
    public void Display()
    {
        if(index < Datacontroller.instance.saveData.currentDailyGift)
        {
            lockObj.SetActive(true);
            unlockObj.SetActive(false);
            reward.SetActive(false);
        }
        else if(index == Datacontroller.instance.saveData.currentDailyGift)
        {
            lockObj.SetActive(false);
            unlockObj.SetActive(true);
            reward.SetActive(true);
            //if (Datacontroller.instance.saveData.canTakeDailyGift)
            //{
            //    lockObj.SetActive(false);
            //    unlockObj.SetActive(true);
            //    reward.SetActive(true);
            //}
            //else
            //{
            //    lockObj.SetActive(true);
            //    unlockObj.SetActive(false);
            //    reward.SetActive(false);
            //}
        }
        else
        {
            lockObj.SetActive(true);
            unlockObj.SetActive(false);
            reward.SetActive(false);
        }
    }
}
