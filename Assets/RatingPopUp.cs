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

    public void SetSelect(int id)
    {
        for (int i = 0; i <= id; i++)
        {
            starButtons[i].starImg.sprite = starButtons[i].yellowStar;
        }
    }

    public void Dissapear()
    {
        gameObject.SetActive(false);
    }
}
