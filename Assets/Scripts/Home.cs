using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Spine.Unity;
using DarkcupGames;

public class StoryData
{
    public string imgDemo;
    public int unlockPrice;
    public bool unlocked;
    public string unlockText;
    public string storyName;
}

public class Home : MonoBehaviour
{
    public TextMeshProUGUI txtLevel;

    [SerializeField] List<SkeletonGraphic> skeletonGraphics;
    [SerializeField] UIUpdater storyUpdater;
    [SerializeField] Transform storySprites;

    List<StoryData> storyDatas;
    public List<StoryData> StoryDatas { get => storyDatas; }

    [SerializeField] GameObject btnCheat;

    public GameObject storyWarning;



    private void Awake()
    {
        btnCheat.SetActive(Datacontroller.instance.testLevel);

        GameSystem.LoadUserData();
        txtLevel.text = (GameSystem.userdata.level + 1) + "/" + Constants.MAX_LEVEL;
    }

    private void Start()
    {
        index = GameSystem.userdata.branchLevel;

        if(!Datacontroller.instance.saveData.showWaringStoryUnlock)
        {
            if (GameSystem.userdata.gold >= 500)
            {
                Datacontroller.instance.saveData.showWaringStoryUnlock = true;
                if (!GameSystem.userdata.boughtItems.Contains("0"))
                {
                    storyWarning.SetActive(true);
                    index = 0;
                }
            }
        }    

        Init();
        ShowStory(index);
    }

    public void Init()
    {
        storyDatas = new List<StoryData>();

        storyDatas.Add(new StoryData()
        {
            storyName = "1",
            imgDemo = "",
            unlockText = "Unlock at level 22",
            unlockPrice = 200,
            unlocked = GameSystem.userdata.boughtItems.Contains(0.ToString()) || GameSystem.userdata.level >= 23
        }); ;
        storyDatas.Add(new StoryData()
        {
            storyName = "2",
            imgDemo = "",
            unlockText = "Unlock at level 22",
            unlockPrice = 200,
            unlocked = GameSystem.userdata.boughtItems.Contains(1.ToString()) || GameSystem.userdata.level >= 23
        }); storyDatas.Add(new StoryData()
        {
            storyName = "3",
            imgDemo = "",
            unlockText = "Unlock at level 22",
            unlockPrice = 200,
            unlocked = GameSystem.userdata.boughtItems.Contains(2.ToString()) || GameSystem.userdata.level >= 23
        }); storyDatas.Add(new StoryData()
        {
            storyName = "4",
            imgDemo = "",
            unlockText = "Unlock at level 22",
            unlockPrice = 200,
            unlocked = GameSystem.userdata.boughtItems.Contains(3.ToString()) || GameSystem.userdata.level >= 23
        });
        storyDatas.Add(new StoryData()
        {
            storyName = "5",
            imgDemo = "",
            unlockText = "Unlock at level 53",
            unlockPrice = 500,
            unlocked = GameSystem.userdata.level >= 52 || GameSystem.userdata.boughtItems.Contains(4.ToString())
        });
        storyDatas.Add(new StoryData()
        {
            storyName = "6",
            imgDemo = "",
            unlockText = "Unlock at level 63",
            unlockPrice = 600,
            unlocked = GameSystem.userdata.level >= 62 || GameSystem.userdata.boughtItems.Contains(5.ToString())
        });

        GameSystem.userdata.storyList = storyDatas;
    }
    int index;
    public void NextStory()
    {
        index++;
        if (index >= Datacontroller.instance.maxBranchLevel) {
            index = 0;
        }

       // int nextIndex = storyDatas.GetNextIndex(GameSystem.userdata.branchLevel);
       // GameSystem.userdata.currentStory = nextIndex;
       // GameSystem.SaveUserDataToLocal();
        Init();
        ShowStory(index);

    }

    public void PreviousStory()
    {
        index--;
        if (index < 0)
        {
            index = Datacontroller.instance.maxBranchLevel - 1;
        }
        // int previousIndex = storyDatas.GetPreviousIndex(GameSystem.userdata.branchLevel);
        // GameSystem.userdata.currentStory = previousIndex;
        // GameSystem.SaveUserDataToLocal();
        Init();
        ShowStory(index);
    }

    public void ShowStory(int index)
    {

        StoryData data = storyDatas[index];
        GameSystem.userdata.branchLevel = index /*+ 1*/;
        GameSystem.SaveUserDataToLocal();
        storyUpdater.UpdateUI(data, storyUpdater.gameObject);
        storySprites.SetEnableChild(index);
        Debug.LogError(GameSystem.userdata.branchLevel);
    }

    public void Show(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }
}

