using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class DrawLevel : LevelManager
{
    public PaintToSpriteController draw;
    public Collider2D checkArea;
    public string winAnimationName;

    public bool isDraw;

    public virtual void Start() {

        PencilShowPosition pencilShowPosition = draw.GetComponent<PencilShowPosition>();
        if(pencilShowPosition == null)
        {
            pencilShowPosition = draw.gameObject.AddComponent<PencilShowPosition>();
        }
        StartCoroutine(IEGameplay());
    }

    public IEnumerator IEGameplay() {
        while (true) {
            yield return new WaitUntil(() => {
              
                return draw.isDrawing == true;
            });

            yield return new WaitUntil(() => {
                return draw.isDrawing == false;
            });

            Debug.Log("Draw finished!");

            int insideCount = 0;
            for (int i = 0; i < draw.drawPoints.Count; i++) {
                if (checkArea.OverlapPoint(draw.drawPoints[i])) {
                    insideCount++;
                }
            }

            float percent = ((float)insideCount) / draw.drawPoints.Count;

            Debug.Log("Percent = " + percent);

            if (percent > Constants.DRAW_PERCENT_REQUIRE) {
                Win();
            } else {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                draw.ClearDraw();
                Gameplay.Instance.Virate();
            }
            //if (item.Value.GetComponent<PolygonCollider2D>().OverlapPoint(CurrentCar.transform.position)) {
            //    inside = true;
            //}
        }
    }

    public override void Win() {

        
        draw.isDrawing = false;
        draw.gameObject.SetActive(false);

        PencilShowPosition pencil = draw.GetComponent<PencilShowPosition>();
        if (pencil != null) {
            pencil.pencil.SetActive(false);
        }
        Gameplay.Instance.Win(this);
    }

    public override Vector3 GetGuidePosition() {
        return checkArea.transform.position;
    }
}
