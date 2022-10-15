using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string userid;
    public string username;
    public float gold;
    public float diamond;
    public int level;
    public int maxLevel;
    public float currentXp;
    public float baseXp;
    //public int monsterInTeam =1;
    public List<string> monstersAvailable;
    public int monsterSlot = 4;
    public List<string> boughtItems;
    public int currentStory;
    public bool playBGM;
    public bool playSound;
    public bool virate;
    public int currentSpecialLevel;

    public UserData()
    {
        boughtItems = new List<string>();
        monstersAvailable = new List<string>();
        monstersAvailable.Add("Monster");
    }
}