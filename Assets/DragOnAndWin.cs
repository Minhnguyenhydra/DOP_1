using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnAndWin : LevelManager
{

    public SpriteRenderer girlPos;
    public SpriteRenderer menPos;
    public SpriteRenderer girlHandPos;

    public GameObject girlDress;
    public GameObject menDress;
    public GameObject flower;

     bool girlDressed;
     bool boyDressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckWin();
    }

    private void CheckWin()
    {
        if (Vector2.Distance(girlDress.transform.position, girlPos.transform.position) < Constants.FIND_ITEM_RANGE)
        {
            girlDressed = true;
            girlDress.gameObject.SetActive(false);
        }

        if (girlDressed)
        {
            if (Vector2.Distance(menDress.transform.position, menPos.transform.position) < Constants.FIND_ITEM_RANGE)
            {
                menDress.gameObject.SetActive(false);

                boyDressed = true;
            }
        }

        if (boyDressed)
        {
            if (Vector2.Distance(flower.transform.position, girlHandPos.transform.position) < Constants.FIND_ITEM_RANGE)
            {
                flower.gameObject.SetActive(false);
                Gameplay.Instance.Win(this);
            }
        }
    }

    public override Vector3 GetGuidePosition()
    {
        List<Vector3> position = new List<Vector3>();
        position.Add(girlPos.transform.position);
        position.Add(menPos.transform.position);
        position.Add(girlHandPos.transform.position);

        foreach(Vector3 pos in position)
        {
            return pos;
        }

        return position[0];
    }
}
