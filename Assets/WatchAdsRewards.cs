using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAdsRewards : MonoBehaviour
{

    public static WatchAdsRewards instance;
     EraseManyTimes specialLevel;
    public EraseManyTimes SpecialLevel
    {
        set => specialLevel = value;
    }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update


    public void OnAdsWatch()
    {
        Debug.Log("Yo");

        if(Gameplay.Instance.isPlayingSpecial)
        {
            if(specialLevel == null)
            {
                specialLevel = FindObjectOfType<EraseManyTimes>();
                Debug.Log(specialLevel.name);
            }

            specialLevel.GetComponent<EraseManyTimes>().OnWatchAdsClick();

        }
    }
  
}
