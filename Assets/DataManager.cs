using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType {
    Draw, Erase
}

public class LevelInfo {
    public int levelId;
    public int levelIdDisplay;
    public string levelTitle;
    public bool unlocked;
    public LevelType levelType;
    public System.Action btnUse;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public static List<int> specialLevels = new List<int>() { 2, 7, 12, 17, 22 };
    public List<LevelInfo> levelInfos;

    private void Awake() {
        GameSystem.LoadUserData();
        Instance = this;
        Init();
    }

    public void Init() {
        levelInfos = new List<LevelInfo>();

        List<string> titles = new List<string>();

        //1
        titles.Add("Taking Off The Mask");
        titles.Add("Help her warm up");
        titles.Add("What is the girl looking at?");
        titles.Add("Find the second girl");
        titles.Add("Help him lift the weight");
        
        //6
        titles.Add("Catch the robber");
        titles.Add("Make her happy");
        titles.Add("Help them relax...");
        titles.Add("Make the farmer happy");
        titles.Add("Where is a female toilet?");

        //11
        titles.Add("Help the witch transform");
        titles.Add("Make it cool!");
        titles.Add("Time out");
        titles.Add("What is she affraid of?");
        titles.Add("Find the suprising girl");

        //16
        titles.Add("Find a ninja");
        titles.Add("What are they doing?");
        titles.Add("Help the girl to fly");
        titles.Add("Make him sexy");
        titles.Add("Beautiful girl and photographer.");

        //21
        titles.Add("Is this the real Santa?");
        titles.Add("How did the prisoner escape?");
        titles.Add("Find the frog.");
        titles.Add("Surprise gift.");
        titles.Add("Where is the gold?");

        //26
        titles.Add("Why is he worried?");
        titles.Add("Get the rid of vampire.");
        titles.Add("Help the girl");
        titles.Add("Find the costume.");
        titles.Add("Use your imagination");

        //31
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");

        //36
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");

        //41
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");

        //46
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");
        titles.Add("Some text");

        for (int i = 0; i < titles.Count; i++) {
            int index = i;

            levelInfos.Add(new LevelInfo() {
                levelId = i,
                levelIdDisplay = i + 1,
                levelTitle = titles[i],
                unlocked = i < GameSystem.userdata.maxLevel || i == 0,
                btnUse = () => {
                    PlayLevel(index);
                }
            });
        }
    }

    public void PlayLevel(int level) {
        GameSystem.userdata.level = level;
        GameSystem.SaveUserDataToLocal();

        DarkcupGames.Utils.ChangeScene(Constants.SCENE_GAMEPLAY);
    }
}
