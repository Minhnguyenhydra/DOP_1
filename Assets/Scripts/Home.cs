using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Spine.Unity;
using DarkcupGames;

public class StoryData {
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

    private void Awake()
    {
        GameSystem.LoadUserData();
        txtLevel.text = (GameSystem.userdata.level + 1) + "/" + Constants.MAX_LEVEL;
    }

    private void Start()
    {
        Init();
        ShowStory(0);
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
            unlocked = GameSystem.userdata.boughtItems.Contains(1.ToString()) || GameSystem.userdata.level >= 23
        }); ;
        storyDatas.Add(new StoryData()
        {
            storyName = "2",
            imgDemo = "",
            unlockText = "Unlock at level 22",
            unlockPrice = 200,
            unlocked = GameSystem.userdata.boughtItems.Contains(2.ToString()) || GameSystem.userdata.level >= 23
        }); storyDatas.Add(new StoryData()
        {
            storyName = "3",
            imgDemo = "",
            unlockText = "Unlock at level 22",
            unlockPrice = 200,
            unlocked = GameSystem.userdata.boughtItems.Contains(3.ToString()) || GameSystem.userdata.level >= 23
        }); storyDatas.Add(new StoryData()
        {
            storyName = "4",
            imgDemo = "",
            unlockText = "Unlock at level 22",
            unlockPrice = 200,
            unlocked = GameSystem.userdata.boughtItems.Contains(4.ToString()) || GameSystem.userdata.level >= 23
        });
        storyDatas.Add(new StoryData()
        {
            storyName = "5",
            imgDemo = "",
            unlockText = "Unlock at level 53",
            unlockPrice = 500,
            unlocked = GameSystem.userdata.level >= 52 || GameSystem.userdata.boughtItems.Contains(5.ToString())
        });
        storyDatas.Add(new StoryData()
        {
            storyName = "6",
            imgDemo = "",
            unlockText = "Unlock at level 63",
            unlockPrice = 600,
            unlocked = GameSystem.userdata.level >= 62 || GameSystem.userdata.boughtItems.Contains(6.ToString())
        });

        GameSystem.userdata.storyList = storyDatas;
    }

    public void NextStory()
    {
        int nextIndex = storyDatas.GetNextIndex(GameSystem.userdata.currentStory);
        GameSystem.userdata.currentStory = nextIndex;
        GameSystem.SaveUserDataToLocal();
        Init();
        ShowStory(nextIndex);

    }

    public void PreviousStory()
    {
        int previousIndex = storyDatas.GetPreviousIndex(GameSystem.userdata.currentStory);
        GameSystem.userdata.currentStory = previousIndex;
        GameSystem.SaveUserDataToLocal();
        Init();
        ShowStory(previousIndex);
    }

    public void ShowStory(int index)
    {
        StoryData data = storyDatas[index];
        GameSystem.userdata.branchLevel = index + 1;
        GameSystem.SaveUserDataToLocal();
        storyUpdater.UpdateUI(data, storyUpdater.gameObject);
        storySprites.SetEnableChild(index);
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

