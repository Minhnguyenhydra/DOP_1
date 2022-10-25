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
    public bool drawCorrect;


    private void Start() {

        mainCam = Camera.main;
        paint = GetComponent<PaintToSpriteMaskController>();
        eraser = ObjectPool.Instance.GetGameObjectFromPool("eraser", new Vector3(999f,999f));
        eraser.gameObject.SetActive(false);
    }

    private void Update() {



        if (paint.isDrawing) {
            eraser.gameObject.SetActive(true);
            Vector2 pos = mainCam.ScreenToWorldPoint((Vector2)paint.lastWorldPos);
            eraser.transform.position = paint.lastWorldPos;
            Debug.Log(pos);
        }
        else
        {


            Vector2 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            eraser.transform.position = pos;
        }
        
    }
}
