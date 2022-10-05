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
        yield return new WaitUntil(() => {
            return draw.IsDrawFinished();
        });

        Debug.Log("Draw finished!!");
        drawFinishEvent?.Invoke();

        yield return new WaitForSeconds(2f);
    }
}
