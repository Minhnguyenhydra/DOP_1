using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyPrice : MonoBehaviour
{
    public Button btnBuy;
    public Button btnUse;
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
        if (!GameSystem.userdata.boughtItems.Contains(GameSystem.userdata.branchLevel.ToString()))
        {
            if(GameSystem.userdata.gold > 500)
            {
                GameSystem.userdata.gold -= 500;
                GameSystem.SaveUserDataToLocal();
                GameSystem.userdata.boughtItems.Add(GameSystem.userdata.branchLevel.ToString());
                switchButton();
            }

            GameSystem.SaveUserDataToLocal();
        }

    }
    

    public void switchButton()
    {
        btnBuy.gameObject.SetActive(false);
        btnUse.gameObject.SetActive(true);
    }
}
