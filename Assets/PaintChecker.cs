using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PaintChecker : MonoBehaviour
{
    public UnityEvent drawFinishEvent;
    public PaintToSpriteMaskController draw;

    private void Start() {
        StartCoroutine(IECheckFinish());
    }

    public IEnumerator IECheckFinish() {
        //skeletonAnimation.AnimationName = "normal";

        yield return new WaitUntil(() => {
            return draw.IsDrawFinished();
        });

        drawFinishEvent?.Invoke();

        yield return new WaitForSeconds(2f);
    }
}
