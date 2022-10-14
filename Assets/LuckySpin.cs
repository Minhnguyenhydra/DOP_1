using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;
using UnityEngine.UI;

public class LuckySpin : MonoBehaviour
{
    [SerializeField]private Rotate rotate;
    private int spinTime = 1;

    private void OnEnable()
    {
        spinTime = 1;
    }
    public void Spin()
    {
        if (spinTime == 0) return;
        spinTime--;
        rotate.spinSpeed = 500f;
        rotate.enabled = true;
    }
}
