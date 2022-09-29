using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;

    public List<ParticleSystem> effects;
    public UIEffect winPopup;

    private void Awake() {
        Instance = this;

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Stop();
        }
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
}
