using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingPopUp : MonoBehaviour
{
    public StarButton[] starButtons;
    public int id;
    public void SetUnselectAll()
    {
        for (int i = 0; i < starButtons.Length; i++)
        {
            starButtons[i].starImg.sprite = starButtons[i].blackStar;
        }
    }

    public void SetSelect(int _id)
    {
        for (int i = 0; i <= _id; i++)
        {
            starButtons[i].starImg.sprite = starButtons[i].yellowStar;
        }
        id = _id;
    }

    public void Dissapear()
    {
        gameObject.SetActive(false);
    }
    public void BtnRateUs()
    {
        if (id < 3)
        {

        }
        else
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
        }
      //  Dissapear();
    }
}
