using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using UnityEngine.SceneManagement;
using Spine.Unity;
using TMPro;
using UnityEngine.Events;

public enum GameplayType { Erase, Draw, Find }

public class Gameplay : MonoBehaviour
{
    public GameObject twoBtnBot;
    public static Gameplay Instance;
    [SerializeField] GameObject btnCheat, btnSpecialLevel;
    [SerializeField] GameObject[] animSpecialBtn,animSpecialPanel;
    public GameplayType GameplayType { get; private set; }

    public List<ParticleSystem> effects;
    public List<RectTransform> findBoxs;
    public GameObject iconTick, panelAfterWin,sepcialWarningPanel;
    public GameObject guideObject;
    public RewardFirstTime rewardFirstTime;
    public UIEffect winPopup;
    public Image findItemDemo;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtQuestion;
    public GameObject closeSpecialLevelButton;
    public GameObject popUpRating;
    public AudioClip winSound;
    public Image scanImg;
    public Canvas canvasGameplay;
    public Button homeButton;
    public Button settingButton;
    public DrawManager drawManager;
    public Text playerLevel;
    public Sprite eraseObject;
    public Sprite findObject;
    public Sprite drawObject;
    public bool isBranchLevel;
    public Sprite cucgom;
    [SerializeField] private GameObject levelObject;
    public bool won = false;
    bool drawLevel, eraseLevel, eraseManyPositionInOneLevel;

    public bool isPlayingSpecial;
    GameObject obj;

    private void Awake()
    {
        // currentSpecialLevel = -1;
        btnCheat.SetActive(Datacontroller.instance.testLevel);

        Instance = this;

        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].Stop();
        }
        if (findItemDemo)
            findItemDemo.gameObject.SetActive(false);

        homeButton.onClick.AddListener(() => { SceneManager.LoadScene("Home"); });
        drawManager.gameObject.SetActive(false);

        AudioSystem.Instance.SetBGM(GameSystem.userdata.playBGM);
        AudioSystem.Instance.SetFXSound(GameSystem.userdata.playSound);

        iconTick.SetActive(false);
        GameSystem.LoadUserData();
        int level = GameSystem.userdata.level;
        //  int count = 0;
        won = false;
        isPlayingSpecial = false;
        if (isBranchLevel)
        {
            obj = Resources.Load<GameObject>("LevelBranch/Level" + (GameSystem.userdata.branchLevel+1));
            Debug.Log(GameSystem.userdata.branchLevel);
            txtLevel.text = "Story - Chapter " + (GameSystem.userdata.branchLevel+1);
            txtQuestion.text = "Make a Choice";
            if (obj != null)
            {
                levelObject = Instantiate(obj);
                scanImg.gameObject.SetActive(false);
                var findItem = FindObjectOfType<FindItemLevel>();

                for (int i = 0; i < findBoxs.Count; i++)
                {
                    findBoxs[i].gameObject.SetActive(false);
                }

                EventController.PLAY_LEVEL_STORY(GameSystem.userdata.branchLevel + 1);

                return;
            }
        }

        //while (count < 10)
        //{
        obj = Resources.Load<GameObject>("Levels/Level" + (level /*+ count*/ + 1));
        closeSpecialLevelButton.SetActive(false);
        txtLevel.text = "Level " + (level /*+ count*/ + 1);

        Debug.LogError("======= level:" + level /*+ ":" + count*/);
        // lv 3 -> spe 1
        // lv 8 -> sp 2
        // lv 13 -> sp 3
        // lv 18 -> sp 4
        // lv 23 -> sp 5

        if (level == 2 && Datacontroller.instance.saveData.passSpecial[0] == 0)
        {
            Datacontroller.instance.saveData.passSpecial[0] = 1;

        }
        else if (level == 7 && Datacontroller.instance.saveData.passSpecial[1] == 0)
        {
            Datacontroller.instance.saveData.passSpecial[1] = 1;
        }
        else if (level == 12 && Datacontroller.instance.saveData.passSpecial[2] == 0)
        {
            Datacontroller.instance.saveData.passSpecial[2] = 1;
        }
        else if (level == 17 && Datacontroller.instance.saveData.passSpecial[3] == 0)
        {
            Datacontroller.instance.saveData.passSpecial[3] = 1;
        }
        else if (level == 22 && Datacontroller.instance.saveData.passSpecial[4] == 0)
        {
            Datacontroller.instance.saveData.passSpecial[4] = 1;
        }

        for (int i = 0; i < Datacontroller.instance.saveData.passSpecial.Count; i++)
        {
            if (Datacontroller.instance.saveData.passSpecial[i] == 1)
            {
                Datacontroller.instance.saveData.newSpecialActive = true;
                break;
            }
        }
        Debug.LogError("============" + Datacontroller.instance.saveData.newSpecialActive);
        for (int i = 0; i < animSpecialBtn.Length; i++)
        {
            animSpecialBtn[i].SetActive(false);
            animSpecialPanel[i].SetActive(false);
        }
        if (Datacontroller.instance.saveData.levelSpecial < Datacontroller.instance.maxSpecialLevel)
        {
            if (Datacontroller.instance.saveData.levelSpecial == 0)
            {
                sepcialWarningPanel.SetActive(Datacontroller.instance.saveData.newSpecialActive);
                btnSpecialLevel.transform.GetChild(2).gameObject.SetActive(true);
            }
            animSpecialBtn[Datacontroller.instance.saveData.levelSpecial].SetActive(true);
            animSpecialPanel[Datacontroller.instance.saveData.levelSpecial].SetActive(true);

            btnSpecialLevel.SetActive(Datacontroller.instance.saveData.newSpecialActive);

        }
        else
        {
            btnSpecialLevel.SetActive(false);
        }

        txtQuestion.text = "";
        if (DataManager.Instance.levelInfos.Count > level /*+ count*/)
        {
            LevelInfo info = DataManager.Instance.levelInfos[level /*+ count*/];

            if (GameSystem.userdata.level == 24)
            {
                txtQuestion.text = null;
            }
            else
            {
                txtQuestion.text = info.levelTitle;
            }
        }

        if (obj != null)
        {
            levelObject = Instantiate(obj);
            GameSystem.userdata.level = level /*+ count*/;
            GameSystem.SaveUserDataToLocal();

            EventController.PLAY_LEVEL_NORMAL(level + 1);
            //break;
        }
        //else
        //{
        //    count++;
        //}
        // }

        if (levelObject == null)
        {
            GameSystem.userdata.level = 0;
            GameSystem.SaveUserDataToLocal();

            GameObject obj = Resources.Load<GameObject>("Levels/Level1");
            levelObject = Instantiate(obj);
            txtLevel.text = "LEVEL 1";
        }
        if (!GameSystem.userdata.showRating && GameSystem.userdata.level == 5)
        {
            popUpRating.SetActive(true);
            GameSystem.userdata.showRating = true;
            GameSystem.SaveUserDataToLocal();
        }

        var findItemLevel = FindObjectOfType<FindItemLevel>();
        for (int i = 0; i < findBoxs.Count; i++)
        {
            findBoxs[i].gameObject.SetActive(findItemLevel != null);
        }

        if (findItemLevel != null || FindObjectOfType<FindAndWinLevel>() != null)
        {
            GameplayType = GameplayType.Find;
            if (PlayerPrefs.GetInt("tutorial_find", 0) == 0)
            {
                PlayerPrefs.SetInt("tutorial_find", 1);
                LeanTween.delayedCall(1f, () =>
                {
                    Hint();
                });
            }
            return;
        }

        drawLevel = FindObjectOfType<DrawLevel>();
        if (drawLevel)
        {
            scanImg.sprite = drawObject;
            drawManager.gameObject.SetActive(true);
            GameplayType = GameplayType.Draw;
            if (PlayerPrefs.GetInt("tutorial_draw", 0) == 0)
            {
                PlayerPrefs.SetInt("tutorial_draw", 1);
                LeanTween.delayedCall(1f, () =>
                {
                    Hint();
                });
            }
        }
        else
        {
            eraseLevel = FindObjectOfType<EraseLevel>();
            eraseManyPositionInOneLevel = FindObjectOfType<EraseManyPositionInOneLevel>();
            eraseSpecialLevel = FindObjectOfType<EraseManyTimes>();
            drawManager.gameObject.SetActive(false);
            scanImg.sprite = cucgom;

            GameplayType = GameplayType.Erase;

            if (PlayerPrefs.GetInt("tutorial_erase", 0) == 0)
            {
                PlayerPrefs.SetInt("tutorial_erase", 1);
                LeanTween.delayedCall(1f, () =>
                {
                    Hint();
                });
            }
        }
        panelAfterWin.SetActive(false);
    }

    public void LoadBranchLevel()
    {
        SceneManager.LoadScene("BranchLevel");
    }

    public void Win(LevelManager level, bool showWinPopupImediately = true, bool loopAnimation = true)
    {
        panelAfterWin.SetActive(true);
        drawManager.gameObject.SetActive(false);
        if (won) return;
        won = true;

        if (level.animBefore != null)
        {
            level.animBefore.gameObject.SetActive(false);
        }
        StartCoroutine(IEWin(level.animAfter, level.winAnims, showWinPopupImediately, level.loopAnimation));
    }

    public IEnumerator IEWin(SkeletonAnimation skeletonAnimation, List<string> anims = null, bool showWinPopupImediately = true, bool loopAnimation = true)
    {
        var erasers = GameObject.FindGameObjectsWithTag("Eraser");
        for (int i = 0; i < erasers.Length; i++)
        {
            erasers[i].gameObject.SetActive(false);
        }

        skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].Play();
        }
        EasyEffect.Appear(iconTick, 0f, 1f, 0.15f);
        AudioSystem.Instance.PlaySound(winSound, 1);
        skeletonAnimation.AnimationState.Data.DefaultMix = 0;

        if (anims != null && anims.Count > 0)
        {
            for (int i = 0; i < anims.Count; i++)
            {
                Spine.Animation win = skeletonAnimation.Skeleton.Data.FindAnimation(anims[i]);
                if (win != null)
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, anims[i], loopAnimation);
                    yield return new WaitForSeconds(win.Duration);
                }
            }
        }
        else
        {
            Spine.Animation win = skeletonAnimation.Skeleton.Data.FindAnimation("win");
            if (win != null)
            {
                if (loopAnimation)
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win", true);
                }
                else
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win", false);
                }

                yield return new WaitForSeconds(win.Duration);
            }

            Spine.Animation win1 = skeletonAnimation.Skeleton.Data.FindAnimation("win1");
            if (win1 != null)
            {
                if (loopAnimation)
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win1", true);
                }
                else
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win1", false);
                }

                yield return new WaitForSeconds(win1.Duration);
            }

            Spine.Animation win2 = skeletonAnimation.Skeleton.Data.FindAnimation("win2");
            if (win2 != null)
            {
                if (loopAnimation)
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win2", true);
                }
                else
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win2", false);
                }
                yield return new WaitForSeconds(win2.Duration);
            }
        }
        yield return new WaitForSeconds(1f);

        if (showWinPopupImediately)
        {
            ShowWinPopup();
        }
        else
        {
            LeanTween.delayedCall(1.5f, () =>
            {
                ShowWinPopup();
            });
        }
        //panelAfterWin.SetActive(false);
    }

    public void ShowWinPopup()
    {
        if (winPopup != null)
            winPopup.DoEffect();

        if (isBranchLevel)
        {
            if (!Datacontroller.instance.saveData.firstTimeLevelBranch[GameSystem.userdata.branchLevel])
            {
                rewardFirstTime.gameObject.SetActive(true);
                Debug.LogError("========== nhan thuong branch");
                Datacontroller.instance.saveData.firstTimeLevelBranch[GameSystem.userdata.branchLevel] = true;
            }
            else
            {
                rewardFirstTime.gameObject.SetActive(false);
                Debug.LogError("========== ko nhan thuong branch");
            }
            EventController.WIN_LEVEL_SPECIAL(GameSystem.userdata.branchLevel + 1);
        }
        else
        {
            if (isPlayingSpecial)
            {
                if (!Datacontroller.instance.saveData.firstTimeLevelSpecial[Datacontroller.instance.saveData.levelSpecial - 1])
                {
                    rewardFirstTime.gameObject.SetActive(true);
                    Debug.LogError("========== nhan thuong special");
                    Datacontroller.instance.saveData.firstTimeLevelSpecial[Datacontroller.instance.saveData.levelSpecial - 1] = true;
                }
                else
                {
                    rewardFirstTime.gameObject.SetActive(false);
                    Debug.LogError("========== ko nhan thuong special");
                }

                EventController.WIN_LEVEL_SPECIAL(Datacontroller.instance.saveData.levelSpecial - 1);
            }
            else
            {
                if (!Datacontroller.instance.saveData.firstTimeLevelNormal[GameSystem.userdata.level])
                {
                    rewardFirstTime.gameObject.SetActive(true);
                    Debug.LogError("========== nhan thuong normal");
                    Datacontroller.instance.saveData.firstTimeLevelNormal[GameSystem.userdata.level] = true;
                }
                else
                {
                    rewardFirstTime.gameObject.SetActive(false);
                    Debug.LogError("========== ko nhan thuong normal");
                }

                EventController.WIN_LEVEL_SPECIAL(GameSystem.userdata.level + 1);
            }
        }
    }

    public void Hint()
    {
        var draw = FindObjectOfType<DrawLevel>();
        if (draw != null)
        {
            draw.Hint();
            Debug.LogError("======== draw level");
            return;
        }

        //  var manyTimes = FindObjectOfType<EraseManyTimes>();
        if (/*manyTimes*/eraseSpecialLevel != null/* && isPlayingSpecial*/)
        {
            //   GuidePosition(manyTimes.guidePosition.position);
            animHint.transform.position = /*manyTimes*/eraseSpecialLevel.GetGuidePosition();
            animHint.gameObject.SetActive(true);
            Debug.LogError("======== earse level level");
            return;
        }

        var level = FindObjectOfType<LevelManager>();
        Debug.LogError("type gameplay:" + GameplayType);
        if (level != null)
        {

            if (GameplayType == GameplayType.Erase)
            {
                //  GuidePosition(level.GetGuidePosition());
                if (eraseLevel || eraseManyPositionInOneLevel)
                {
                    animHint.transform.position = level.GetGuidePosition();
                    animHint.gameObject.SetActive(true);
                }
                else
                {


                    levelDragAndDrop = level.GetComponent<DragAndDropLevel>();
                    if (levelDragAndDrop != null)
                        GuidePosition(level.GetGuidePosition(), true);
                    else
                        GuidePosition(level.GetGuidePosition());
                }
            }
            else
            {
                GuidePosition(level.GetGuidePosition());
                Debug.LogError("======== hint pos:" + level.GetGuidePosition());
            }
            Debug.LogError("======== level ??????");
        }
    }

    public EraseManyTimes eraseSpecialLevel;
    DragAndDropLevel levelDragAndDrop;
    public Animator animHint;
    public void GuidePosition(Vector2 pos, bool branchPos = false)
    {
        guideObject.SetActive(true);
        if (!branchPos)
        {
            guideObject.transform.position = new Vector3(0, -3f);
        }
        else
        {
            guideObject.transform.position = levelDragAndDrop.GetDragPos();
        }
        LeanTween.move(guideObject, pos, 1f).setEaseOutCubic().setOnComplete(() =>
        {
            guideObject.SetActive(false);
        });
    }

    //public int GetSpecialLevel() {
    //    var userLevel = GameSystem.userdata.level;
    // //   Debug.LogError("======= user level:" + userLevel);
    //    for (int i = 0; i < DataManager.specialLevels.Count; i++) {
    //        if (userLevel == DataManager.specialLevels[i]) {
    //            return i + 1;
    //        }
    //    }
    //    return -1;
    //}

    public void Next()
    {
        if (isBranchLevel)
        {
            SceneManager.LoadScene("Home");
            isBranchLevel = false;
            Datacontroller.instance.ShowInter();
            return;
        }
        //if (GetSpecialLevel() > 0 && !isPlayingSpecial) {

        //    isPlayingSpecial = true;
        //    SpawnSpecialLevel();
        //    return;
        //}
        if (!isPlayingSpecial)
        {
            GameSystem.userdata.level++;
            Debug.LogError("======= user level:" + GameSystem.userdata.level);
            if (GameSystem.userdata.level > GameSystem.userdata.maxLevel)
            {
                GameSystem.userdata.maxLevel = GameSystem.userdata.level;
            }
            GameSystem.SaveUserDataToLocal();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Datacontroller.instance.ShowInter();
    }
    public void BtnCloseWarningSpecialPanel()
    {
        sepcialWarningPanel.SetActive(false);
    }
    public void BtnShowSpecialLevel()
    {
        SpawnSpecialLevel();
    }
    // int currentSpecialLevel = 0;
    public void SpawnSpecialLevel()
    {
        sepcialWarningPanel.SetActive(false);
        eraseSpecialLevel = null;
        isPlayingSpecial = true;
        Destroy(levelObject);
        // closeSpecialLevelButton.SetActive(true);
        var obj = Resources.Load<GameObject>("LevelSpecials/Special" + /*GetSpecialLevel()*//*currentSpecialLevel*/(Datacontroller.instance.saveData.levelSpecial + 1));
        drawManager.gameObject.SetActive(false);
        //txtLevel.gameObject.SetActive(isPlayingSpecial);
        //txtQuestion.gameObject.SetActive(isPlayingSpecial);
        txtLevel.text = "Special " + (Datacontroller.instance.saveData.levelSpecial + 1);
        txtQuestion.text = "Erase Clothes";
        levelObject = Instantiate(obj);
        eraseSpecialLevel = levelObject.GetComponent<EraseManyTimes>();
        EasyEffect.Disappear(winPopup.gameObject, 1, 0);
        // canvasGameplay.gameObject.SetActive(false);
        won = false;
        GameplayType = GameplayType.Erase;
        var iconManager = FindObjectOfType<IconManager>();
        if (iconManager)
        {
            iconManager.Init();
        }

        EventController.PLAY_LEVEL_SPECIAL(Datacontroller.instance.saveData.levelSpecial + 1);

        Datacontroller.instance.saveData.passSpecial[Datacontroller.instance.saveData.levelSpecial] = 2;

        btnSpecialLevel.SetActive(false);

        if (Datacontroller.instance.saveData.levelSpecial == 0)
        {
            LeanTween.delayedCall(1f, () =>
            {
                Hint();
            });
        }
        Datacontroller.instance.saveData.levelSpecial++;
        Datacontroller.instance.saveData.newSpecialActive = false;
    }

    public void LevelUp()
    {
        closeSpecialLevelButton.SetActive(false);
        GameSystem.userdata.level++;
        GameSystem.SaveUserDataToLocal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Destroy(levelObject);
        //var obj = Resources.Load<GameObject>("Levels/Level" + GameSystem.userdata.level);
        //txtLevel.text = "LEVEL " + GameSystem.userdata.level;
        //levelObject = Instantiate(obj);
        //EasyEffect.Disappear(winPopup.gameObject, 1, 0);
    }

    public void FoundItem(SpriteRenderer renderer)
    {
        Transform emptyBox = null;

        for (int i = 0; i < findBoxs.Count; i++)
        {
            Transform box = findBoxs[i].transform;
            if (box.childCount == 0)
            {
                emptyBox = box;
                break;
            }
        }
        if (emptyBox == null) return;
        StartCoroutine(IEFoundItem(renderer, emptyBox));
    }

    IEnumerator IEFoundItem(SpriteRenderer renderer, Transform emptyBox)
    {
        var demo = Instantiate(findItemDemo);

        demo.transform.position = Camera.main.WorldToScreenPoint(renderer.transform.position);
        demo.sprite = renderer.sprite;
        demo.transform.SetParent(emptyBox);
        demo.gameObject.SetActive(true);

        EasyEffect.Appear(demo.gameObject, 0f, 1f);

        yield return new WaitForSeconds(1f);

        LeanTween.move(demo.gameObject, emptyBox.transform.position, 1f).setDelay(.5f).setEaseOutCubic();
    }

    public void AddGold(int amount)
    {
        GameSystem.userdata.gold += amount;
        GameSystem.SaveUserDataToLocal();
    }
    public void Virate()
    {
        if (GameSystem.userdata.virate)
            Handheld.Vibrate();
    }
}
