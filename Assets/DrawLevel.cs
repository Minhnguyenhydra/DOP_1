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
    public List<GameObject> tutorialDots;

    public bool isDraw;

    public virtual void Start()
    {

        PencilShowPosition pencilShowPosition = draw.GetComponent<PencilShowPosition>();
        if (pencilShowPosition == null)
        {
            pencilShowPosition = draw.gameObject.AddComponent<PencilShowPosition>();
        }
        for (int i = 0; i < tutorialDots.Count; i++)
        {
            tutorialDots[i].gameObject.SetActive(false);
        }
        StartCoroutine(IEGameplay());
    }

    public IEnumerator IEGameplay()
    {


        while (true)
        {
            yield return new WaitUntil(() =>
            {
                return draw.isDrawing == true;
            });
            yield return new WaitUntil(() =>
            {
                return draw.isDrawing == false;
            });

            int insideCount = 0;
            Debug.LogError("popup:" + Gameplay.Instance.PopUpShow());
            if (!Gameplay.Instance.PopUpShow())
            {

                for (int i = 0; i < draw.drawPoints.Count; i++)
                {
                    if (checkArea.OverlapPoint(draw.drawPoints[i]))
                    {
                        insideCount++;
                    }
                }
                float percent = ((float)insideCount) / draw.drawPoints.Count;
                if (percent > Constants.DRAW_PERCENT_REQUIRE)
                {
                    Win();
                }
                else
                {
                    draw.ClearDraw();
                    Gameplay.Instance.Virate();
                }
            }
            else
            {
                draw.ClearDraw();
            }    
        }
    }

    public override void Win()
    {
        draw.isDrawing = false;
        draw.gameObject.SetActive(false);
        for (int i = 0; i < tutorialDots.Count; i++)
        {
            tutorialDots[i].gameObject.SetActive(false);
        }
        PencilShowPosition pencil = draw.GetComponent<PencilShowPosition>();
        if (pencil != null)
        {
            pencil.pencil.SetActive(false);
        }
        Gameplay.Instance.Win(this);
    }

    public override Vector3 GetGuidePosition()
    {
        return checkArea.transform.position;
    }

    public override void Hint()
    {
        StartCoroutine(IEDrawHint());
    }

    IEnumerator IEDrawHint()
    {
        for (int i = 0; i < tutorialDots.Count; i++)
        {
            tutorialDots[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2f);
        //for (int i = 0; i < tutorialDots.Count; i++) {
        //    tutorialDots[i].gameObject.SetActive(false);
        //}
    }
}