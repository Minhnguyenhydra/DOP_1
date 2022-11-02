using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBranchLevelInfo : MonoBehaviour
{
    public List<GameObject> branchLevels = new List<GameObject>();
    int value;

    void Start()
    {
        InvokeRepeating(nameof(SaveData), 0f, 1f);
    }

    void SaveData() {
        for (int i = 0; i < branchLevels.Count; i++) {
            if (branchLevels[i].activeSelf) {
                GameSystem.userdata.branchLevel = branchLevels[i].GetComponent<BranchLevelsInfo>().Levels;
                GameSystem.SaveUserDataToLocal();


            }
        }
    }
}
