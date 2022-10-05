using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDebugger : MonoBehaviour
{
    public SpriteRenderer find;
    //public SpriteRenderer source;
    //public SpriteMask spriteMask;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //UpdateSpriteMask();
            Gameplay.Instance.FindItem(find);
        }
    }


    //public void UpdateSpriteMask()
    //{
    //    spriteMask.sprite = source.sprite;
    //}
}
