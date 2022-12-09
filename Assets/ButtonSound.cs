using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{
    public AudioClip buttonSound;

    bool addedSound = false;

    private void Start()
    {
        AddButtonSounds();
    }

    private void OnEnable()
    {
        AddButtonSounds();
    }

    public void AddButtonSounds()
    {
        if (addedSound) return;
        addedSound = true;

        Button[] buttons = gameObject.GetComponentsInChildren<Button>(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(PlayButtonSound);
        }
    }

    public void PlayButtonSound()
    {
        AudioSystem.Instance.PlaySound(buttonSound);
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (SceneManager.GetActiveScene().name == "Home")
            {
                EventController.MAIN_CLICK("click_button_" + EventSystem.current.currentSelectedGameObject.name + "_Scene_" + SceneManager.GetActiveScene().name);
            }
            else
            {
                EventController.GAME_PLAY("click_button_" + EventSystem.current.currentSelectedGameObject.name + "_Scene_" + SceneManager.GetActiveScene().name);
            }

            EventController.FLOW_FIRST_OPEN("click_button_" + EventSystem.current.currentSelectedGameObject.name + "_Scene_" + SceneManager.GetActiveScene().name);
        }
        Debug.LogError("========== play sound btn");
    } 
}
