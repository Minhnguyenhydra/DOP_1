using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAdsRewards : MonoBehaviour
{
    EraseManyTimes specialLevel;

    // Start is called before the first frame update
    void Start()
    {
        specialLevel = FindObjectOfType<EraseManyTimes>();
        Debug.Log(specialLevel.name);
    }




    public void OnAdsWatch()
    {
        specialLevel.GetComponent<EraseManyTimes>().OnWatchAdsClick();
    }
  
}
