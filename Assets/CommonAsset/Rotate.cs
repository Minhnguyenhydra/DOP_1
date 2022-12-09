using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkcupGames
{
    public class Rotate : MonoBehaviour
    {
        public GameObject[] flyingBulb;
        public GameObject locationCheck;
        public RectTransform finishLocation;
        public float spinSpeed = 500f;
        public float angle = 1f;
        [SerializeField]
        LuckySpin myLuckySpin;
        public List<Vector2> posOriginal = new List<Vector2>();
        int indexBubl;
        GameObject nearest;
        private void Start()
        {
            enabled = false;

            Debug.LogError("======= start");
            for (int i = 0; i < flyingBulb.Length; i++)
            {
                posOriginal.Add(flyingBulb[i].transform.localPosition);
            }
            indexBubl = 0;
            nearest = flyingBulb[0];

        }

        private void Update()
        {
            if (spinSpeed <= 0f)
            {
                indexBubl = 0;
                for (int i = 0; i < flyingBulb.Length; i++)
                {
                    Debug.LogError("============= check legth:" + Vector2.Distance(flyingBulb[i].transform.position, locationCheck.transform.position) + ":" + Vector2.Distance(nearest.transform.position, locationCheck.transform.position));
                    if (Vector2.Distance(flyingBulb[i].transform.position, locationCheck.transform.position) <= Vector2.Distance(nearest.transform.position, locationCheck.transform.position))
                    {
                        indexBubl = i;

                        Debug.LogError("============= check :" + i);
                    }
                }
                nearest = flyingBulb[indexBubl];
                //   Debug.LogError("============= check :" + nearest);
                nearest.LeanMove(finishLocation.transform.position, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                {
                    if (indexBubl == 0)
                    {
                        GameSystem.userdata.gold += 10;
                    }
                    else if (indexBubl == 1)
                    {
                        GameSystem.userdata.gold += 15;
                    }
                    else if (indexBubl == 2)
                    {
                        GameSystem.userdata.gold += 10;
                    }
                    else if (indexBubl == 3)
                    {
                        GameSystem.userdata.gold += 10;
                    }
                    else if (indexBubl == 4)
                    {
                        GameSystem.userdata.gold += 15;
                    }
                    else if (indexBubl == 5)
                    {
                        GameSystem.userdata.gold += 50;
                    }


                    GameSystem.SaveUserDataToLocal();
                    nearest.transform.localPosition = posOriginal[indexBubl];

                });
                myLuckySpin.spinTime = 1;
                enabled = false;
                return;
            }
            spinSpeed -= Time.deltaTime * 50f;
            transform.Rotate(new Vector3(0, 0, angle).normalized * spinSpeed * Time.deltaTime);
        }
    }
}
