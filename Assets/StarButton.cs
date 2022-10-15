using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarButton : MonoBehaviour
{
    public Sprite blackStar;
    public Sprite yellowStar;
    [System.NonSerialized]public Image starImg;
    [System.NonSerialized] public RatingPopUp ratingPopUp;
    [SerializeField] private int id;
    private void Start()
    {
        starImg = GetComponent<Image>();
        ratingPopUp = transform.parent.GetComponentInParent<RatingPopUp>();

    }

    public void StarClick()
    {
        ratingPopUp.SetUnselectAll();
        ratingPopUp.SetSelect(id);
    }
}
