using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using TMPro;

public class CustomLevel49 : FindAndWinLevel
{
    GameObject findObj;

    private void Start() {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Gameplay.Instance.settingButton.transform.position);
        findObj = Instantiate(new GameObject(), pos, Quaternion.identity);
        objectFinds = new List<GameObject>() { findObj };
        isMultiple = false;
    }
    public override Vector3 GetGuidePosition() {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Gameplay.Instance.settingButton.transform.position);
        return pos;
    }
}
