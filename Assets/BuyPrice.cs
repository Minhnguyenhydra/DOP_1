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
    public GameObject flyingPulb;

    public RectTransform branchLevelLocation;
    public RectTransform firstLocation;
    // Start is called before the first frame update
    void Start()
    {
        flyingPulb.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buyItem()
    {
        Debug.Log("Clicked");
        Debug.Log(GameSystem.userdata.branchLevel);
        if (!GameSystem.userdata.boughtItems.Contains(GameSystem.userdata.branchLevel.ToString()))
        {

            Debug.Log("Buy itme");
            flyingPulb.gameObject.SetActive(true);
            if(GameSystem.userdata.gold >= 500)
            {
                GameSystem.userdata.gold -= 500;

                LeanTween.move(flyingPulb, branchLevelLocation, 1f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
                  {

                      LeanTween.scale(flyingPulb, new Vector3(0, 0, 0), .5f).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
                      {
                          LeanTween.move(flyingPulb, firstLocation, 0f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
                          {
                              LeanTween.scale(flyingPulb, new Vector3(1.2298f, 1.2298f, 1.2298f), 0f).setEase(LeanTweenType.easeOutBack);
                              flyingPulb.gameObject.SetActive(false);
                          });
                          GameSystem.userdata.boughtItems.Add(GameSystem.userdata.branchLevel.ToString());
                          GameSystem.SaveUserDataToLocal();
                          switchButton();
                      });
                     
                  });
                
            }
            else
            {
                Debug.Log("Bought");
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
