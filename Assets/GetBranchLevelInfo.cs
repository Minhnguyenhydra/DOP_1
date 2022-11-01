using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBranchLevelInfo : MonoBehaviour
{
    public List<GameObject> branchLevels = new List<GameObject>();
    // Start is called before the first frame update
    int value;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < branchLevels.Count; i++)
        {
            if (branchLevels[i].activeSelf)
            {
                GameSystem.userdata.branchLevel = branchLevels[i].GetComponent<BranchLevelsInfo>().Levels;
                GameSystem.SaveUserDataToLocal();


            }   
        }
    }
}
