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
        private void Start()
        {
            enabled = false;

        }
        private void Update()
        {
            if (spinSpeed <= 0f)
            {
                GameObject nearest = flyingBulb[0];
                for(int i = 0; i < flyingBulb.Length; i++)
                {
                    if(Vector2.Distance(flyingBulb[i].transform.position,locationCheck.transform.position) < Vector2.Distance(nearest.transform.position, locationCheck.transform.position))
                    {
                        nearest = flyingBulb[i];
                    }
                }
                LeanTween.move(nearest, finishLocation, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                {
                    LeanTween.scale(nearest, new Vector3(0f, 0f, 0f), .5f).setEase(LeanTweenType.easeInBack);
                    GameSystem.userdata.gold += 5;
                    GameSystem.SaveUserDataToLocal();

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
