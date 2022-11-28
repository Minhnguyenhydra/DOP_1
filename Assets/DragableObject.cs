using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Spine.Unity;

[RequireComponent(typeof(AudioSource))]
public class DragableObject : MonoBehaviour, IDragHandler, IEndDragHandler{
    //public PaintToSpriteMaskController draw;
    public Sprite mouseCursor;

    public string wrongAnim;
    public bool isReturn;
    //public bool isErase;
    public bool isDraw;
    public bool isCorrect;

    Vector3 startPos;
    Camera mainCam;

    bool hasDragged;

    private void Start() {

        //if (isErase)
        //{
        //    EraserShowPosition eraserShowPosition = draw.GetComponent<EraserShowPosition>();
        //    if (eraserShowPosition == null)
        //    {
        //        eraserShowPosition = draw.gameObject.AddComponent<EraserShowPosition>();
        //    }
        //}
       
        mainCam = Camera.main;
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData) {

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = pos;
        //if (isErase)
        //{
        //    draw.GetComponent<EraserShowPosition>().eraser.gameObject.SetActive(true);
        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (isErase)
        //{
        //    draw.GetComponent<EraserShowPosition>().eraser.gameObject.SetActive(false);
        //}
        hasDragged = true;
    }

    public void Update()
    {
        if (isReturn && hasDragged)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 25f * Time.deltaTime);
            if (Vector2.Distance(transform.position, startPos) < 0.5f) {
                transform.position = startPos;
                hasDragged = false;
            }
            //if(Vector2.Distance(transform.position, startPos) > 0.5f)
            //{

            //}
        }
    }
}
