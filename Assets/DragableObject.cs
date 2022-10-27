using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class DragableObject : MonoBehaviour, IDragHandler, IEndDragHandler{
    Camera mainCam;

    public PaintToSpriteMaskController draw;

    public bool isReturn;

    public bool isErase;

    public bool isDraw;

     bool hasDragged;

    public Sprite mouseCursor;

    public Transform returnToPos;


    private void Start() {

        if (isErase)
        {
            EraserShowPosition eraserShowPosition = draw.GetComponent<EraserShowPosition>();
            if (eraserShowPosition == null)
            {
                eraserShowPosition = draw.gameObject.AddComponent<EraserShowPosition>();
            }
           
        }
       
        mainCam = Camera.main;
    }

    public void OnMouseEnter()
    {

    }



    public void OnDrag(PointerEventData eventData) {

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
        if (isErase)
        {
            draw.GetComponent<EraserShowPosition>().eraser.gameObject.SetActive(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isErase)
        {
            Debug.Log("End");
            draw.GetComponent<EraserShowPosition>().eraser.gameObject.SetActive(false);
        }
        hasDragged = true;

    }

    public void Update()
    {
        if (isReturn && hasDragged)
        {
            
            if(Vector2.Distance(transform.position, returnToPos.position) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, returnToPos.position, 25f * Time.deltaTime);
                if(Vector2.Distance(transform.position, returnToPos.position) < 0.1f)
                {
                    transform.position = returnToPos.position;
                    hasDragged = false;
                }
            }

        }

    }
}
