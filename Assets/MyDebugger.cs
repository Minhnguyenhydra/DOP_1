using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class MyDebugger : MonoBehaviour
{
    public SpriteRenderer find;
    //public SpriteRenderer source;
    //public SpriteMask spriteMask;
    int count = 0;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //UpdateSpriteMask();
            //Gameplay.Instance.FoundItem(find);
            GameSystem.userdata.level += 5;
            GameSystem.SaveUserDataToLocal();
            Utils.ReloadScene();
        }
    }


    //public void UpdateSpriteMask()
    //{
    //    spriteMask.sprite = source.sprite;
    //}
}
