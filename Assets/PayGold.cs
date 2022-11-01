using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;
using UnityEngine.UI;
public class PayGold : MonoBehaviour
{
    public GameObject panelNotEnoughMoney;
    public Button payGoldBTN;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPayGoldClick()
    {
        EasyEffect.Appear(panelNotEnoughMoney, 0.1f, 1f);
        payGoldBTN.enabled = false;
        StartCoroutine(popUpDisAppear());
    }

    IEnumerator popUpDisAppear()
    {
        yield return new WaitForSeconds(1f);
        EasyEffect.Appear(panelNotEnoughMoney, 1f, 0f);
        payGoldBTN.enabled = true;

    }
}
