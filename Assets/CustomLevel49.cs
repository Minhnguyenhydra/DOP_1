using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using TMPro;

public class CustomLevel49 : FindAndWinLevel
{
    GameObject findObj;
    Vector2 pos;
    Color color;
    private void Start()
    {
        color = Gameplay.Instance.settingButton.image.color;
        color.a = 0;
        Gameplay.Instance.settingButton.image.color = color;
        Gameplay.Instance.txtQuestion.gameObject.SetActive(false);
        pos.x = Camera.main.ScreenToWorldPoint(Gameplay.Instance.settingButton.transform.position).x + 0.4f;
        pos.y = Camera.main.ScreenToWorldPoint(Gameplay.Instance.settingButton.transform.position).y - 0.4f;
        //findObj = new GameObject(); /*Instantiate(new GameObject(), pos, Quaternion.identity);*/
        ////   objectFinds = new List<GameObject>() { findObj };
        //findObj.transform.position = pos;
        //objectFinds.Clear();
        //objectFinds.Add(findObj);
        objectFinds[0].transform.position = pos;
        isMultiple = false;
    }
    public override Vector3 GetGuidePosition()
    {
        /*Camera.main.ScreenToWorldPoint(Gameplay.Instance.settingButton.transform.position)*/
        return objectFinds[0].transform.position;
    }
}
