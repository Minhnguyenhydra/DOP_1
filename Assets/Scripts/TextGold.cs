using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGold : MonoBehaviour
{
    TextMeshProUGUI txtGold;

    private void Start() {
        txtGold = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        txtGold.text = GameSystem.userdata.gold.ToString();
    }
}