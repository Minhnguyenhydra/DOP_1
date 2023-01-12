using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public enum LevelType {
    Draw, Erase,Drag 
}
[System.Serializable]
public class LevelInfo {
    public int levelId;
    public int levelIdDisplay;
    public string levelTitle;
    public bool unlocked;
    public LevelType levelType;
    public System.Action btnUse;
    public GameObject rewardButton;
    public Sprite normalImage;
    public Sprite disableImage;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public long Ticks;
    public long nextRewards;
    public int Day = 0;
    public static List<int> specialLevels = new List<int>() { 2, 7, 12, 17, 22 };
    public List<LevelInfo> levelInfos;
    public Button claimGiftButton;
    public Button soundButton;
    public Button bgmButton;
    public Button vibrateButton;

    public Sprite[] sounds, musics, vibrates;

    public Sprite normalImage;
    public Sprite disableImage;
    private void Awake() {
        GameSystem.LoadUserData();
        Instance = this;
        Init();
    }

    public void Init() {
        levelInfos = new List<LevelInfo>();

        List<string> titles = new List<string>();

        //1
        titles.Add("What is the girl looking at?");//1
        titles.Add("Make the farmer happy");//2
        titles.Add("Unlock My Phone");//3
        titles.Add("Erase the excess.");//4
        titles.Add("Help her get contact with friends.");//5
        titles.Add("Save the Cat.");//6
        titles.Add("Find the good girl.");//7
        titles.Add("Feed the lizard.");//8
        titles.Add("Someone's Looking");//9
        titles.Add("Catch the thief.");//10
        titles.Add("Help the witch transform");//11
        titles.Add("Help him lift the weight");//12
        titles.Add("Time out");//13
        titles.Add("What is she affraid of?");//14
        titles.Add("Where is a female toilet?");//15
        titles.Add("Find a ninja");//16
        titles.Add("Taking Off The Mask");//17
        titles.Add("Help the girl to fly");//18
        titles.Add("Help her warm up");//19
        titles.Add("Beautiful girl and photographer.");//20
        titles.Add("Is this the real Santa?");//21
        titles.Add("How did the prisoner escape?");//22
        titles.Add("Find the frog.");//23
        titles.Add("Find the suprising girl");//24
        titles.Add("What are they doing?");//25
        titles.Add("Why is he worried?");//26
        titles.Add("Get the rid of vampire.");//27
        titles.Add("Help the girl");//28
        titles.Add("Make him sexy");//29
        titles.Add("Use your imagination");//30
        titles.Add("Find the mysterious person.");//31
        titles.Add("Wake up!");//32
        titles.Add("Summon the genie.");//33
        titles.Add("Find the second girl");//34
        titles.Add("Help the boy stay awake.");//35
        titles.Add("Who is real?.");//36
        titles.Add("Help him please her.");//37
        titles.Add("Help them relax...");//38
        titles.Add("Find the first full tank.");//39
        titles.Add("What is she doing?");//40
        titles.Add("You will be surprised.");//41
        titles.Add("Help him a singer");//42
        titles.Add("Kill the monster");//43
        titles.Add("Help her lose weight.");//44
        titles.Add("Kill The Vampires.");//45
        titles.Add("Catch the robber");//46
        titles.Add("Make her happy");//47
        titles.Add("Make it cool!");//48
        titles.Add("Find the letter O.");//49
        titles.Add("Find the sheep.");//50
        titles.Add("Find an omelet");//51
        titles.Add("Find the thief");//52
        titles.Add("Help the bus to drive through.");//53
        titles.Add("Surprise gift.");//54
        titles.Add("");//55
        titles.Add("Finish the costume");//56
        titles.Add("Save the girl.");//57
        titles.Add("Help him.");//58
        titles.Add("He wants a ghost.");//59
        titles.Add("Help Her fix the airpod");//60





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
    private void Start()
    {
        //if (vibrateButton)
        //    vibrateButton.gameObject.SetActive(GameSystem.userdata.virate);
        //if (soundButton)
        //    soundButton.gameObject.SetActive(GameSystem.userdata.playSound);
        //if (bgmButton)
        //    bgmButton.gameObject.SetActive(GameSystem.userdata.playBGM);
        DisplaySetting();
    }
    public void DisplaySetting()
    {
        vibrateButton.image.sprite = GameSystem.userdata.virate ? vibrates[1] : vibrates[0];
        soundButton.image.sprite = GameSystem.userdata.playSound ? sounds[1] : sounds[0];
        bgmButton.image.sprite = GameSystem.userdata.playBGM ? musics[1] : musics[0];

        Debug.LogError("====== start:" + bgmButton.image.sprite);
    }
    public void PlayLevel(int level) {
        GameSystem.userdata.level = level;
        GameSystem.SaveUserDataToLocal();

        DarkcupGames.Utils.ChangeScene(Constants.SCENE_GAMEPLAY);
    }

    public void ClaimGiftExtend(int day)
    {
        Day += day;
        GameSystem.userdata.nextDay = DateTime.Now.AddDays(Day).Ticks;
        GameSystem.SaveUserDataToLocal();
    }
}
