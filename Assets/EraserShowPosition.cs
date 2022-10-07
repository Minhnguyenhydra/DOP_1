using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class EraserShowPosition : MonoBehaviour
{
    [HideInInspector]
    public GameObject eraser;
    PaintToSpriteMaskController paint;
    Camera mainCam;

    private void Start() {
        mainCam = Camera.main;
        paint = GetComponent<PaintToSpriteMaskController>();
        eraser = ObjectPool.Instance.GetGameObjectFromPool("eraser", new Vector3(999f,999f));
    }

    private void Update() {
        if (paint.isDrawing) {
            Vector2 pos = mainCam.ScreenToWorldPoint((Vector2)paint.lastWorldPos);
            eraser.transform.position = paint.lastWorldPos;
            eraser.gameObject.SetActive(true);
            Debug.Log(pos);
        } else {
            eraser.gameObject.SetActive(false);
        }
    }
}
