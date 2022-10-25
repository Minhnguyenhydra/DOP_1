using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragInAndWin : LevelManager
{
    public bool win;

    public SpriteRenderer firePlace;

    public GameObject smallWood;
    public GameObject fire;
    public GameObject bigWood;

    public bool woodIn;
    public bool isFired;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckWin();

        if (win)
        {
            Gameplay.Instance.Win(this);
        }
    }

    private void CheckWin()
    {
        if (Vector2.Distance(firePlace.transform.position, smallWood.transform.position) < Constants.FIND_ITEM_RANGE)
        {
            smallWood.gameObject.SetActive(false);
            woodIn = true;
        }

        if (woodIn)
        {
            if (Vector2.Distance(firePlace.transform.position, fire.transform.position) < Constants.FIND_ITEM_RANGE)
            {
                fire.gameObject.SetActive(false);
                isFired = true;
            }
        }

        if (isFired)
        {
            if (Vector2.Distance(firePlace.transform.position, bigWood.transform.position) < Constants.FIND_ITEM_RANGE)
            {
                bigWood.gameObject.SetActive(false);
                win = true;
            }
        }
    }
}
