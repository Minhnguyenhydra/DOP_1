using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class MyDebugger : MonoBehaviour
{
    public SpriteRenderer find;
    public int currentLevel;
    int count = 0;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
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
        currentLevel = GameSystem.userdata.level;
    }


    //public void UpdateSpriteMask()
    //{
    //    spriteMask.sprite = source.sprite;
    //}
}
