using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace DarkcupGames {
    public class CheatManager : MonoBehaviour {
        public GameObject cheatPanel;
        public TMP_InputField inputLevel;

        public void ShowCheat() {
            cheatPanel.SetActive(true);
        }

        public void HideCheat() {
            cheatPanel.SetActive(false);
        }

        public void ResetAll() {
            GameSystem.userdata = new UserData();
            GameSystem.SaveUserDataToLocal();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void AddCoins(int number) {
            GameSystem.userdata.gold += number;
            GameSystem.SaveUserDataToLocal();
        }

        public void PlayLevel() {
            int level = 0;
            int.TryParse(inputLevel.text, out level);
            GameSystem.userdata.level = level;
            GameSystem.SaveUserDataToLocal();
            Utils.ReloadScene();
        }

        public void ReplayCurrentLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void PlayScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}

