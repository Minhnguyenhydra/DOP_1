using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnScene : LevelManager
{
    public bool hairShowered;
    public bool hairDryed;
    public bool win;
    public SpriteRenderer hairPos;
    public SpriteRenderer bodyPos;

    public GameObject _dress;
    public GameObject _shower;
    public GameObject _hairDryer;

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkWin();

        if (win)
        {
            Gameplay.Instance.Win(this);
        }
    }
    
    private void checkWin()
    {
        if (Vector2.Distance(_shower.transform.position, hairPos.transform.position) < Constants.FIND_ITEM_RANGE)
        {
            _shower.gameObject.SetActive(false);
            hairShowered = true;
        }

        if (hairShowered)
        {
            if (Vector2.Distance(_hairDryer.transform.position, hairPos.transform.position) < Constants.FIND_ITEM_RANGE)
            {
                _hairDryer.gameObject.SetActive(false);
                hairDryed = true;
            }
        }

        if (hairDryed)
        {
            if (Vector2.Distance(_dress.transform.position, bodyPos.transform.position) < Constants.FIND_ITEM_RANGE)
            {
                _dress.gameObject.SetActive(false);
                win = true;
            }
        }
    }

    public override Vector3 GetGuidePosition()
    {
        List<Vector3> pos = new List<Vector3>();
        pos.Add(bodyPos.transform.position);
        pos.Add(hairPos.transform.position);

        foreach(Vector3 position in pos)
        {
            return position;
        }
        return pos[0];
    }
}
