using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;

    public List<ParticleSystem> effects;
    public UIEffect winPopup;
    public Image findItemDemo;
    public List<RectTransform> findBoxs;

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
        if (level > Constants.MAX_LEVEL) level = 0;

        GameObject obj = Resources.Load<GameObject>("Levels/Level" + (level + 1));
        Instantiate(obj);
    }

    public void Win() {
        StartCoroutine(IEPlayCongratulation());
    }

    public IEnumerator IEPlayCongratulation() {
        for (int i = 0; i < effects.Count; i++) {
            effects[i].Play();
        }

        yield return new WaitForSeconds(1f);

        winPopup.DoEffect();
    }

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
