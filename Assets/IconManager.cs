using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using TMPro;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    public static IconManager instance;
    RectTransform rect;
    public Image iconMove;
    public Image icon;
    public Sprite sprErase;
    public Sprite sprDraw;
    public Sprite sprFind;
    GameObject kinhlup;
    GameObject pencil;

    private void Start() {
        instance = this;
        rect = GetComponent<RectTransform>();
        GetComponent<Image>().enabled = false;
        if (SceneManager.GetActiveScene().name != Constants.SCENE_GAMEPLAY) {
            gameObject.SetActive(false);
            if (iconMove) iconMove.gameObject.SetActive(false);
            if (icon) icon.gameObject.SetActive(false);
            return;
        }
        Init();
    }

    public void Init() {
        if (pencil != null && pencil.gameObject.activeInHierarchy) {
            pencil.SetActive(false);
        }
        if (kinhlup != null && kinhlup.gameObject.activeInHierarchy) {
            kinhlup.SetActive(false);
        }

        if (Gameplay.Instance.GameplayType == GameplayType.Draw) {
            icon.sprite = sprDraw;
            iconMove.sprite = sprDraw;
            pencil = ObjectPool.Instance.GetGameObjectFromPool("pencil", Vector2.zero);
            pencil.SetActive(false);
        }
        if (Gameplay.Instance.GameplayType == GameplayType.Erase) {
            icon.sprite = sprErase;
            iconMove.sprite = sprErase;
        }
        if (Gameplay.Instance.GameplayType == GameplayType.Find) {
            icon.sprite = sprFind;
            iconMove.sprite = sprFind;

            var find1 = FindObjectOfType<FindAndWinLevel>();
            if (find1 != null) {
                kinhlup = find1.magnify;
            }
            var find2 = FindObjectOfType<FindItemLevel>();
            if (find2 != null) {
                kinhlup = find2.magnify;
            }
        }
        iconMove.gameObject.SetActive(false);
    }

    private void Update() {
        bool inside = RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition);
        if (Gameplay.Instance.GameplayType == GameplayType.Erase) {
            if (inside && Input.GetMouseButton(0)) {
                iconMove.transform.position = Input.mousePosition;
                iconMove.gameObject.SetActive(true);
                icon.gameObject.SetActive(false);
            } else {
                iconMove.gameObject.SetActive(false);
                icon.gameObject.SetActive(true);
            }
        }

        if (Gameplay.Instance.GameplayType == GameplayType.Draw) {
            if (inside && Input.GetMouseButton(0)) {
                icon.gameObject.SetActive(false);
                pencil.gameObject.SetActive(true);
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pencil.transform.position = pos;
            } else {
                icon.gameObject.SetActive(true);
                pencil.gameObject.SetActive(false);
            }
        }

        if (Gameplay.Instance.GameplayType == GameplayType.Find) {
            if (inside && Input.GetMouseButton(0)) {
                icon.gameObject.SetActive(false);
                kinhlup.transform.GetChild(0).gameObject.SetActive(true);
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                kinhlup.transform.position = pos;
            } else {
                icon.gameObject.SetActive(true);
                kinhlup.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
