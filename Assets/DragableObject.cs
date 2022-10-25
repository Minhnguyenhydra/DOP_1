using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragableObject : MonoBehaviour, IDragHandler, IEndDragHandler{
    Camera mainCam;


    public bool isReturn;

     bool hasDragged;
    public Transform returnToPos;
    private void Start() {

        mainCam = Camera.main;
    }

    public void OnMouseEnter()
    {

    }



    public void OnDrag(PointerEventData eventData) {

        Vector2 pos = mainCam.ScreenToWorldPoint(eventData.position);
        transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    
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
