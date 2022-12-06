using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;
using UnityEngine.UI;

public class LuckySpin : MonoBehaviour
{
    [SerializeField]private Rotate rotate;
    public int spinTime = 1;
    [SerializeField] GameObject watchAds;
    public void BtnClose()
    {
        if (spinTime < 1)
            return;
        EasyEffect.Disappear(gameObject, 1f, 0f);

    }
    public void BtnShowWatchAds()
    {
        if (spinTime < 1)
            return;
        EasyEffect.Appear(watchAds, 0f, 1f);
    }
    private void OnEnable()
    {
        spinTime = 1;
    }
    public void Spin()
    {
        if (spinTime < 1) return;
        spinTime--;
        rotate.spinSpeed = 500f;
        rotate.enabled = true;
    }

}
