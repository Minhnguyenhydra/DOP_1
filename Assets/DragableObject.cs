using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragableObject : MonoBehaviour, IDragHandler {
    Camera mainCam;
    private void Start() {
        mainCam = Camera.main;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 pos = mainCam.ScreenToWorldPoint(eventData.position);
        transform.position = pos;
    }
}
