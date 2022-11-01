using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWatchAdController : MonoBehaviour
{
    public GameObject watchAdsToSpinPanel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSpinClick()
    {
        watchAdsToSpinPanel.gameObject.SetActive(true);

    }
}
