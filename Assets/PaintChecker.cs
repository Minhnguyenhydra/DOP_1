 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(PaintController))]
public class PaintChecker : MonoBehaviour
{
    public UnityEvent drawFinishEvent;
    [HideInInspector]
    public PaintController draw;
    

    private void Start() {
        draw = GetComponent<PaintController>();
        StartChecking();
    }

    public void StartChecking() {
        StopCoroutine(IECheckFinish());
        StartCoroutine(IECheckFinish());
    }

    public IEnumerator IECheckFinish() {
        while (true)
        {
            yield return new WaitUntil(() => {
                return Input.GetMouseButtonUp(0);
            });

            if (!Gameplay.Instance.PopUpShow())
            {
                if (draw.IsDrawFinished())
                {
                    draw.GetComponent<EraserShowPosition>().eraser.gameObject.SetActive(false);
                    drawFinishEvent?.Invoke();
                    break;
                }
                else
                {
                    draw.ClearDraw();
                }
            }
            else
            {
                draw.GetComponent<EraserShowPosition>().eraser.gameObject.SetActive(false);
                draw.ClearDraw();
            }    
        }

        yield return new WaitForSeconds(2f);
    }
}
