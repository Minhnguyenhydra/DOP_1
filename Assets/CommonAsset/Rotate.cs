using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkcupGames
{
    public class Rotate : MonoBehaviour
    {
        public float spinSpeed = 500f;
        public float angle = 1f;

        private void Start()
        {
            enabled = false;

        }
        private void Update()
        {
            if (spinSpeed <= 0f)
            {
                GameSystem.userdata.gold += 5;
                enabled = false;
                return;
            }
            spinSpeed -= Time.deltaTime * 50f;
            transform.Rotate(new Vector3(0, 0, angle).normalized * spinSpeed * Time.deltaTime);
        }
    }
}
