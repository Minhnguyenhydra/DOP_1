using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGold : MonoBehaviour
{
    TextMeshProUGUI txtGold;
    float currentGold;

    private void Start() {
        txtGold = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (currentGold != GameSystem.userdata.gold) {
            txtGold.text = GameSystem.userdata.gold.ToString();
            currentGold = GameSystem.userdata.gold;
        }
    }

    public void AddGold(int goldAdd)
    {
        GameSystem.userdata.gold += goldAdd;
    }
}