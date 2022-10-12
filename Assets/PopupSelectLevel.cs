using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class PopupSelectLevel : MonoBehaviour
{
    public UIUpdater levelUpdater;

    private void Start() {
        levelUpdater.UpdateChildUI(DataManager.Instance.levelInfos);
    }
}
