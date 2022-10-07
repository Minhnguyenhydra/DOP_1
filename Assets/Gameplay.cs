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
    public UIEffect winPopup;
    public Image findItemDemo;
    public List<RectTransform> findBoxs;
    public TextMeshProUGUI txtLevel;

    private void Awake() {
        Instance = this;

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Stop();
        }
        findItemDemo.gameObject.SetActive(false);
    }

    private void Start() {
        GameSystem.LoadUserData();
        int level = GameSystem.userdata.level;

        int count = 0;

        GameObject levelObject = null;

        while (count < 10) {
            GameObject obj = Resources.Load<GameObject>("Levels/Level" + (level + count + 1));

            if (obj != null) {
                levelObject = Instantiate(obj);
                txtLevel.text = "LEVEL " + (level + count + 1);
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
    }

    public void Win(LevelManager level) {
        if (level.animBefore != null) {
            level.animBefore.gameObject.SetActive(false);
        }
        StartCoroutine(IEWin(level.animAfter));
    }

    IEnumerator IEWin(SkeletonAnimation skeletonAnimation) {
        skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Play();
        }

        var animations = skeletonAnimation.Skeleton.Data.Animations;
        foreach (Spine.Animation item in animations) {
            if (item.Name == "win") {
                skeletonAnimation.AnimationName = "win";
                yield return new WaitForSeconds(item.Duration);
                break;
            }

            if (item.Name == "win1") {
                skeletonAnimation.AnimationState.SetAnimation(0, "win1", false);
                //skeletonAnimation.AnimationName = "win1";

                yield return new WaitForSeconds(item.Duration);

                Spine.Animation win2 = skeletonAnimation.Skeleton.Data.FindAnimation("win2");

                if (win2 != null) {
                    skeletonAnimation.AnimationName = "win2";
                    yield return new WaitForSeconds(win2.Duration);
                }
            }
        }

        //StartCoroutine(IEPlayCongratulation());
        yield return new WaitForSeconds(1f);

        winPopup.DoEffect();
    }

    //public IEnumerator IEPlayCongratulation() {
       

    //    yield return new WaitForSeconds(2f);

    //    winPopup.DoEffect();
    //}

    public void Hint() {
        FindObjectOfType<LevelManager>()?.Hint();
    }

    public void Next() {
        GameSystem.userdata.level++;
        GameSystem.SaveUserDataToLocal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}
