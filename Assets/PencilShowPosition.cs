using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;
public class PencilShowPosition : MonoBehaviour
{
    [HideInInspector]
    public GameObject pencil;
    PaintToSpriteController paint;
    Camera mainCam;
    public bool drawCorrect;




    private void Start()
    {
        Debug.Log("create eraser");
        mainCam = Camera.main;
        paint = GetComponent<PaintToSpriteController>();
        pencil = ObjectPool.Instance.GetGameObjectFromPool("pencil", new Vector3(999f, 999f));
        pencil.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (paint.isDrawing)
        {
            pencil.gameObject.SetActive(true);
            Vector2 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            pencil.transform.position = paint.lastWorldPos;
            Debug.Log(pos);
        }
        else
        {
            Vector2 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            pencil.transform.position = pos;
        }

    }
    // Start is called before the first frame update
   
}
