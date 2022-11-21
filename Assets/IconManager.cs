using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using TMPro;

public class IconManager : MonoBehaviour
{
    RectTransform rect;
    public Image eraser;
    public Image icon;

    private void Start() {
        rect = GetComponent<RectTransform>();
        GetComponent<Image>().enabled = false;
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            Debug.Log("Mouse button 0");
        }

        bool inside = RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition);
        if (inside && Input.GetMouseButton(0)) {
            Debug.Log("Inside!!");
            eraser.transform.position = Input.mousePosition;
            eraser.gameObject.SetActive(true);
            icon.gameObject.SetActive(false);
        } else {
            eraser.gameObject.SetActive(false);
            icon.gameObject.SetActive(true);
        }
    }
}
