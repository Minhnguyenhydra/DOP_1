using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using UnityEngine.SceneManagement;
using Spine.Unity;
using TMPro;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;

    public List<ParticleSystem> effects;
    public List<RectTransform> findBoxs;

    public GameObject dautich;
    public GameObject guideObject;
    public UIEffect winPopup;
    public Image findItemDemo;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtQuestion;
    public GameObject closeSpecialLevelButton;
    public GameObject popUpRating;
    public AudioClip winSound;
    public Image scanImg;
    public Canvas canvasGameplay;

    public Sprite eraseObject;
    public Sprite findObject;
    public Sprite drawObject;


    public Sprite cucgom;
    [SerializeField]private GameObject levelObject;
    bool won = false;
    bool isPlayingSpecial;

    private void Awake() {
        Instance = this;

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Stop();
        }
        if (findItemDemo)
            findItemDemo.gameObject.SetActive(false);
    }

    private void Start() {
        dautich.SetActive(false);
        GameSystem.LoadUserData();
        int level = GameSystem.userdata.level;

        int count = 0;
        won = false;
        isPlayingSpecial = false;

        while (count < 10) {
            GameObject obj = Resources.Load<GameObject>("Levels/Level" + (level + count + 1));
            closeSpecialLevelButton.SetActive(false);
            txtLevel.text = "Level " + (level + count + 1);

            txtQuestion.text = "";
            if (DataManager.Instance.levelInfos.Count > level + count) {
                LevelInfo info = DataManager.Instance.levelInfos[level + count];
                txtQuestion.text = info.levelTitle;
            }

            if (obj != null) {
                levelObject = Instantiate(obj);
                GameSystem.userdata.level = level + count;
                GameSystem.SaveUserDataToLocal();
                break;
            } else {
                count++;
            }
        }

        if (levelObject == null) {
            GameSystem.userdata.level = 0;
            GameSystem.SaveUserDataToLocal();

            levelObject = Resources.Load<GameObject>("Levels/Level1");

            txtLevel.text = "LEVEL 1";
        }

        AudioSystem.Instance.SetBGM(GameSystem.userdata.playBGM);
        AudioSystem.Instance.SetFXSound(GameSystem.userdata.playSound);

        if (!GameSystem.userdata.showRating && GameSystem.userdata.level == 6) {
            popUpRating.SetActive(true);
            GameSystem.userdata.showRating = true;
            GameSystem.SaveUserDataToLocal();
        }

        var findItemLevel = FindObjectOfType<FindItemLevel>();

        for (int i = 0; i < findBoxs.Count; i++) {
            findBoxs[i].gameObject.SetActive(findItemLevel != null);
        }

        if (findItemLevel != null) {
            return;
        }

        var findLevel = FindObjectOfType<FindAndWinLevel>();

        if (findLevel != null)
        {
            return;

        }
        else
        {
            var drawLevel = FindObjectOfType<DrawLevel>();

            if(drawLevel)
            {
                scanImg.sprite = drawObject; 
            }
            else
            {
                scanImg.sprite = cucgom;
            }
        }


        
    }

    public void Win(LevelManager level, bool showWinPopupImediately     = true, bool loopAnimation = true) {
        if (won) return;
        won = true;

        if (level.animBefore != null) {
            level.animBefore.gameObject.SetActive(false);
        }
        StartCoroutine(IEWin(level.animAfter, level.winAnims, showWinPopupImediately));
    }



    

    IEnumerator IEWin(SkeletonAnimation skeletonAnimation, List<string> anims = null, bool showWinPopupImediately = true, bool loopAnimation = true) {
        skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
        for (int i = 0; i < effects.Count; i++) {
            effects[i].Play();
        }
        EasyEffect.Appear(dautich, 0f, 1f, 0.15f);
        AudioSystem.Instance.PlaySound(winSound, 1);
        skeletonAnimation.AnimationState.Data.DefaultMix = 0;

        if (anims != null && anims.Count > 0)
        {
            for (int i = 0; i < anims.Count; i++)
            {
                Spine.Animation win = skeletonAnimation.Skeleton.Data.FindAnimation(anims[i]);
                if (win != null)
                {
                    skeletonAnimation.AnimationName = anims[i];

                    yield return new WaitForSeconds(win.Duration);
                }
            }
        } else
        {
            Spine.Animation win = skeletonAnimation.Skeleton.Data.FindAnimation("win");
            if (win != null)
            {
                if (loopAnimation) {
                    skeletonAnimation.AnimationName = "win";
                } else {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win", false);
                }

                yield return new WaitForSeconds(win.Duration);
            }

            Spine.Animation win1 = skeletonAnimation.Skeleton.Data.FindAnimation("win1");
            if (win1 != null)
            {
                if (loopAnimation) {
                    skeletonAnimation.AnimationName = "win1";
                } else {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win1", false);
                }

                yield return new WaitForSeconds(win1.Duration);
            }

            Spine.Animation win2 = skeletonAnimation.Skeleton.Data.FindAnimation("win2");
            if (win2 != null)
            {
                if (loopAnimation) {
                    skeletonAnimation.AnimationName = "win2";
                } else {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win2", false);
                }
                yield return new WaitForSeconds(win2.Duration);
            }
        }
        yield return new WaitForSeconds(1f);

        if (showWinPopupImediately) {
            ShowWinPopup();
        }
    }

    public void ShowWinPopup() {
        winPopup.DoEffect();
    }

    public void Hint() {
        var manyTimes = FindObjectOfType<EraseManyTimes>();
        if (manyTimes != null) {
            GuidePosition(manyTimes.guidePosition.position);
            return;
        }

        var level = FindObjectOfType<LevelManager>();
        if (level != null) {
            GuidePosition(level.GetGuidePosition());
        }
    }

    public void GuidePosition(Vector2 pos) {
        guideObject.SetActive(true);
        guideObject.transform.position = new Vector2(0, 0);
        LeanTween.move(guideObject, pos, 1f).setEaseOutCubic().setOnComplete(() => {
            guideObject.SetActive(false);
        });
    }

    public int GetSpecialLevel()
    {
        var userLevel = GameSystem.userdata.level;
        for (int i = 0; i < DataManager.specialLevels.Count; i++)
        {
            if (userLevel == DataManager.specialLevels[i])
            {
                return i+1;
            }
        }
        return -1;
    }

    public void Next()
    {
        if (GetSpecialLevel() > 0 && !isPlayingSpecial)
        {
            isPlayingSpecial = true;
            SpawnSpecialLevel();
            return;
        }

        GameSystem.userdata.level++;
        if (GameSystem.userdata.level > GameSystem.userdata.maxLevel)
        {
            GameSystem.userdata.maxLevel = GameSystem.userdata.level;
        }
        GameSystem.SaveUserDataToLocal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }




    public void SpawnSpecialLevel() {
        Destroy(levelObject);
        closeSpecialLevelButton.SetActive(true);
        var obj = Resources.Load<GameObject>("LevelSpecials/Special" + GetSpecialLevel());
        txtLevel.gameObject.SetActive(isPlayingSpecial);
        txtQuestion.gameObject.SetActive(isPlayingSpecial);
        levelObject = Instantiate(obj);
        EasyEffect.Disappear(winPopup.gameObject, 1, 0);
        canvasGameplay.gameObject.SetActive(false);
        won = false;
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

    public void FoundItem(SpriteRenderer renderer) {
        Transform emptyBox = null;

        for (int i = 0; i < findBoxs.Count; i++) {
            Transform box = findBoxs[i].transform;
            if (box.childCount == 0) {
                emptyBox = box;
                break;
            }
        }
        if (emptyBox == null) return;
        StartCoroutine(IEFoundItem(renderer, emptyBox));
    }

    IEnumerator IEFoundItem(SpriteRenderer renderer, Transform emptyBox) {
        var demo = Instantiate(findItemDemo);

        demo.transform.position = Camera.main.WorldToScreenPoint(renderer.transform.position);
        demo.sprite = renderer.sprite;
        demo.transform.SetParent(emptyBox);
        demo.gameObject.SetActive(true);

        EasyEffect.Appear(demo.gameObject, 0f, 1f);

        yield return new WaitForSeconds(1f);

        LeanTween.move(demo.gameObject, emptyBox.transform.position, 1f).setEaseOutCubic();
    }

    public void AddGold(int amount) {
        GameSystem.userdata.gold += amount;
        GameSystem.SaveUserDataToLocal();
    }

    public void Virate()
    {
        if (GameSystem.userdata.virate)
            Handheld.Vibrate();
    }
}
