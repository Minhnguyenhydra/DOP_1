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

    private void Awake() {
        GameSystem.LoadUserData();
        txtLevel.text = (GameSystem.userdata.level + 1) + "/" + Constants.MAX_LEVEL;
    }

    private void Start() {
        Init();
        ShowStory(0);
    }

    public void Init() {
        storyDatas = new List<StoryData>();
        storyDatas.Add(new StoryData() {
            imgDemo = "",
            unlockText = "Unlock at level 22",
            unlockPrice = 200,
            unlocked = true
        });
        storyDatas.Add(new StoryData() {
            imgDemo = "",
            unlockText = "Unlock at level 53",
            unlockPrice = 500
        });
        storyDatas.Add(new StoryData() {
            imgDemo = "",
            unlockText = "Unlock at level 63",
            unlockPrice = 600
        });
    }

    public void NextStory() {
        int nextIndex = storyDatas.GetNextIndex(GameSystem.userdata.currentStory);
        GameSystem.userdata.currentStory = nextIndex;
        GameSystem.SaveUserDataToLocal();

        ShowStory(nextIndex);
    }

    public void PreviousStory() {
        int previousIndex = storyDatas.GetPreviousIndex(GameSystem.userdata.currentStory);
        GameSystem.userdata.currentStory = previousIndex;
        GameSystem.SaveUserDataToLocal();

        ShowStory(previousIndex);
    }

    public void ShowStory(int index) {
        StoryData data = storyDatas[index];
        storyUpdater.UpdateUI(data, storyUpdater.gameObject);
        storySprites.SetEnableChild(index);
    }
}
