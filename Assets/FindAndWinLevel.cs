using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAndWinLevel : LevelManager
{
    public GameObject magnify;
    public List<GameObject> objectFinds = new List<GameObject>();
     bool isWin = false;
    public bool isActive = true;
    public bool isMultiple;
    public bool isSpeical;
    public bool isImmediately;
    public void Start() {
        //Hint();
        //founds = new Dictionary<SpriteRenderer, bool>();
        //for (int i = 0; i < findObjects.Count; i++) {
        //    founds.Add(findObjects[i], false);
        //}
    }

    public override void Hint() {
        StartCoroutine(IEHint());
    }

    public IEnumerator IEHint() {
        yield return new WaitForSeconds(1f);
        Win();
    }

    public override void Win() {
        Gameplay.Instance.Win(this);
        //skeletonAnimation.AnimationName = "win";
        //skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
    }

    private void Update() {
        CheckWin();
    }

    public void CheckWin() {
        if (isWin) return;

        if (!isMultiple)
        {
            if (!isSpeical)
            {
                float distance = Vector2.Distance(objectFinds[0].transform.position, magnify.transform.GetChild(0).position);

                if (distance < Constants.FIND_ITEM_RANGE)
                {
                    isWin = true;
                    //magnify.gameObject.SetActive(false);
                }
            }
            else
            {
                float distance = Vector2.Distance(objectFinds[0].transform.position, magnify.transform.GetChild(0).GetChild(0).position);

                if (distance < .1f)
                {
                    isWin = true;
                    //magnify.gameObject.SetActive(false);
                }
            }
        }

        else
        {
            int index = 0;
            int checkFalse = objectFinds.Count;
            while(index < objectFinds.Count)
            {
                if (objectFinds[index].activeSelf)
                {
                    isActive = true;
                    checkFalse = objectFinds.Count;
                }
                else
                {
                    isActive = false;
                    checkFalse--;
                }
                index++;
            }

            foreach (GameObject objectFind in objectFinds)
            {
                float range = isSpeical ? .75f : Constants.FIND_ITEM_RANGE;

                var circle = magnify.transform.GetChild(0);
                if(Vector2.Distance(circle.transform.position,objectFind.transform.position) < range )
                {
                    objectFind.gameObject.SetActive(false);

                }
            }
            isWin = checkFalse == 0;
        }

        
        

        if (isWin) {
            if (!isImmediately)
            {
                Debug.Log("Delay win");
                LeanTween.delayedCall(1.5f, () =>
                {
                    magnify.gameObject.SetActive(false);
                    Gameplay.Instance.Win(this);
                });
            }
            else
            {
                magnify.gameObject.SetActive(false);

                Gameplay.Instance.Win(this);
            }
        }

    }

    public override Vector3 GetGuidePosition() {

        for(int i =0; i < objectFinds.Count; i++)
        {
            if (objectFinds[i].activeSelf)
            {
                return objectFinds[i].transform.position;
            }
        }

        return Vector3.zero;
    }
}
