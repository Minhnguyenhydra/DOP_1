using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
public class BuyPrice : MonoBehaviour
{
    public Button btnBuy;
    public Button btnUse;

    public GameObject notEnoughtMoneyPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buyItem()
    {
        Debug.Log("Clicked");
        if (!GameSystem.userdata.boughtItems.Contains(GameSystem.userdata.branchLevel.ToString()))
        {
            if(GameSystem.userdata.gold >= 500)
            {
                GameSystem.userdata.gold -= 500;
                GameSystem.userdata.boughtItems.Add(GameSystem.userdata.branchLevel.ToString());
                GameSystem.SaveUserDataToLocal();
                switchButton();
            }
            else
            {
                EasyEffect.Appear(notEnoughtMoneyPanel, 0f, 1.4f);
                StartCoroutine(popUpDissapear());
                btnBuy.enabled = false;
            }

        }

    }
    public IEnumerator popUpDissapear()
    {
        yield return new WaitForSeconds(.75f);
        EasyEffect.Appear(notEnoughtMoneyPanel, 1.4f, 0f);
        btnBuy.enabled = true;
    }

    public void switchButton()
    {
        btnBuy.gameObject.SetActive(false);
        btnUse.gameObject.SetActive(true);
    }
}
