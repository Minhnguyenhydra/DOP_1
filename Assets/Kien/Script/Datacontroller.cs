using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Networking;
using static GetMoreGame;
using System.Threading;
using System.Globalization;
using System;
using static SaveData;


//[System.Serializable]
public class SaveData
{
    public bool offmusic, offsound, offvibra, removeAds, rated, showTut;
    public int currentLevel, session, levelSpecial, countVideoForRemoveAds;
    public int currentDailyGift;
    public bool canTakeDailyGift, newSpecialActive;

    public DateTime oldDay = System.DateTime.Now;

    public List<int> passSpecial = new List<int>();
    public List<bool> firstTimeLevelNormal = new List<bool>();
    public List<bool> firstTimeLevelSpecial = new List<bool>();
    public List<bool> firstTimeLevelBranch = new List<bool>();

}
[System.Serializable]
public class SaveMoreGame
{
    public List<int> indexClicked = new List<int>();
}
[System.Serializable]
public class GetMoreGame
{
    public List<InfoMoreGame> infoMoreGame = new List<InfoMoreGame>();
    [System.Serializable]
    public class InfoMoreGame
    {
        public int index;
        public string nameGame;
        public string link;
        public string linkIcon;
        public string packageName;
        public Texture2D myTexture;
    }
}
public class Datacontroller : MonoBehaviour
{
    public int maxSpecialLevel = 5;
    public int maxNormalLevel = 49;
    public int maxBranchLevel = 6;
    public bool testLevel;
    public bool debug;
    public bool anUI;
    public bool anAds;
    public bool testIAP;
    public SaveData saveData;
    public static Datacontroller instance;
    public bool isHack;

    string urlMoreGame;
    string urlLevel;

    public string devAndroid, devkeyIos;
    public string urlLevelAndroid, urlLevelIOS;
    public string appIDIos;

    public Sprite[] spReward;


    public void RemoveAdsFunc()
    {
        saveData.removeAds = true;
        AdsController.instance.HideBanner();
    }

    private void Awake()
    {
        if (instance == null)
        {
            Application.targetFrameRate = 300;
            Debug.unityLogger.logEnabled = debug;
            //    Input.multiTouchEnabled = false;
            CultureInfo ci = new CultureInfo("en-us");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllData();
        }
        else
        {
            DestroyImmediate(gameObject);
        }

#if UNITY_IOS
       // urlMoreGame = urlIos;
        urlLevel = urlLevelIOS;
#else
        //  urlMoreGame = urlAdroid;
        urlLevel = urlLevelAndroid;
#endif

        wwwLevel = UnityWebRequest.Get(urlLevel);


    }
    UnityWebRequest wwwLevel;



    void ReadPlayer(string value)
    {

        strDataLoadPref = null;
        strDataLoadPref = value;
        if (!string.IsNullOrEmpty(strDataLoadPref) && strDataLoadPref != "" && strDataLoadPref != "[]")
        {
            saveData = JsonMapper.ToObject<SaveData>(strDataLoadPref);
        }

    }

    void LoadData(string value)
    {
        saveData = new SaveData();
        ReadPlayer(value);
    }
    void LoadAllData()
    {
        LoadData(PlayerPrefs.GetString(DataParam.SAVEDATA));
        //  LoadSaveMoreGame(PlayerPrefs.GetString(DataParam.SAVEMOREGAME));
        DataParam.beginShowInter = DataParam.lastShowInter = System.DateTime.Now;

        if (saveData.session == 0)
        {
            saveData.oldDay = System.DateTime.Now;
            saveData.canTakeDailyGift = true;
        }

        saveData.session++;
        Debug.LogError(saveData.oldDay.Date + ":" + System.DateTime.Now.Date);
        if (saveData.oldDay.Date != System.DateTime.Now.Date)
        {
            saveData.canTakeDailyGift = true;
            if (saveData.currentLevel == 7)
            {
                saveData.currentLevel = 0;
            }
        }
    }

    void Start()
    {

        ////Debug.LogError("=======" + urlMoreGame);
        ////WWW www = new WWW(urlMoreGame);

        ////StartCoroutine(WaitForRequest(www));
        //StartCoroutine(WaitForRequestLevel(wwwLevel));
        ////  LoadSaveMoreGame(PlayerPrefs.GetString(DataParam.SAVEMOREGAME));

        //for (int i = 2; i < 2; i++)
        //{
        //    Debug.LogError("zooooooooooo tessstttttt lissttttttt");
        //}
        CreateSpecialLevelInfo();
    }
    void CreateSpecialLevelInfo()
    {
        for (int i = saveData.passSpecial.Count; i < maxSpecialLevel; i++)
        {
            int _pass = new int();
            saveData.passSpecial.Add(_pass);
        }
        for (int i = saveData.firstTimeLevelSpecial.Count; i < maxSpecialLevel; i++)
        {
            bool _fistTime = new bool();
            saveData.firstTimeLevelSpecial.Add(_fistTime);
        }
        for (int i = saveData.firstTimeLevelNormal.Count; i < maxNormalLevel; i++)
        {
            bool _fistTime = new bool();
            saveData.firstTimeLevelNormal.Add(_fistTime);
        }
        for (int i = saveData.firstTimeLevelBranch.Count; i < maxBranchLevel; i++)
        {
            bool _fistTime = new bool();
            saveData.firstTimeLevelBranch.Add(_fistTime);
        }

    }
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors

        if (www.error == null)
        {
            //  Debug.LogError("=====WWW MoreGame!: " + www.text);
            //jData = JsonMapper.ToObject(www.text);
            //if (jData != null)
            //{
            getMoreGame = JsonMapper.ToObject<GetMoreGame>(www.text/*jData.ToJson()*/);
            //}
        }
        else
        {
            //   Debug.Log("======WWW Error MoreGame: " + www.error);
        }

        for (int i = 0; i < getMoreGame.infoMoreGame.Count; i++)
        {
            SetImage(getMoreGame.infoMoreGame[i].linkIcon, i);
        }
    }

    IEnumerator WaitForRequestLevel(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("======WWW Error Level: " + www.error);
        }
        else
        {
            // DataParam.wwwLevel = www.downloadHandler.text;
            Debug.LogError("=====WWW Level!: " + www.downloadHandler.text);
        }


    }

    static bool calculate;
    GetMoreGame getMoreGame = new GetMoreGame();
    public void CalculateListMoreGameBegin()
    {
        if (calculate)
            return;
        for (int i = 0; i < getMoreGame.infoMoreGame.Count; i++)
        {
            if (!saveMoreGame.indexClicked.Contains(getMoreGame.infoMoreGame[i].index) && getMoreGame.infoMoreGame[i].packageName != Application.identifier && getMoreGame.infoMoreGame[i].myTexture != null)
            {
                moreGameLst.infoMoreGame.Add(getMoreGame.infoMoreGame[i]);
            }
            if (getMoreGame.infoMoreGame[i].packageName != Application.identifier && getMoreGame.infoMoreGame[i].myTexture != null)
            {
                moreGameLstToComingSoon.infoMoreGame.Add(getMoreGame.infoMoreGame[i]);
            }
        }
        calculate = true;
    }
    public void SetImage(string url, int i)
    {
        StartCoroutine(DownloadImage(url, i));
    }
    Texture2D texture;
    IEnumerator DownloadImage(string url, int i)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.LogError("======" + request.error);
        else
        {
            texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            getMoreGame.infoMoreGame[i].myTexture = texture;
            Debug.LogError("======" + "texture ok");
        }
    }
    void CreateTestMoreGame()
    {
        getMoreGame.infoMoreGame.Clear();
        for (int i = 0; i < 3; i++)
        {
            InfoMoreGame _getMoreGame = new InfoMoreGame();
            _getMoreGame.index = i;
            _getMoreGame.nameGame = "Game" + (i + 1);
            getMoreGame.infoMoreGame.Add(_getMoreGame);
        }
        string moregame = JsonMapper.ToJson(getMoreGame);
        Debug.LogError(moregame);
    }
    void LoadSaveMoreGame(string value)
    {
        ReadSaveMoreGame(value);
    }
    string strDataLoadPref;
    void ReadSaveMoreGame(string value)
    {
        strDataLoadPref = null;
        strDataLoadPref = value;

        if (!string.IsNullOrEmpty(strDataLoadPref) && strDataLoadPref != "" && strDataLoadPref != "[]")
        {
            // Debug.LogError(strDataLoadPref);
            //  jData = JsonMapper.ToObject(strDataLoadPref);
            //   if (jData != null)
            saveMoreGame = JsonMapper.ToObject<SaveMoreGame>(strDataLoadPref/*jData.ToJson()*/);
        }
    }


    public GetMoreGame moreGameLst = new GetMoreGame();
    public GetMoreGame moreGameLstToComingSoon = new GetMoreGame();
    public SaveMoreGame saveMoreGame = new SaveMoreGame();
    public string urlAdroid, urlIos;

    JsonData jData;
    string tempsaveData, tempsaveMoreGame;
    void SetSaveTemp()
    {
        tempsaveData = JsonMapper.ToJson(saveData);
        tempsaveMoreGame = JsonMapper.ToJson(saveMoreGame);
    }
    void SaveData()
    {
        SetSaveTemp();
        PlayerPrefs.SetString(DataParam.SAVEDATA, tempsaveData);
        //   PlayerPrefs.SetString(DataParam.SAVEMOREGAME, tempsaveMoreGame);
        PlayerPrefs.Save();
        // Debug.LogError("===save data===");
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
            //  Debug.LogError("======!focus");
        }
        //else
        //{
        //    Debug.LogError("======focus");
        //    if (DataParam.afterShowAds)
        //    {
        //        DataParam.afterShowAds = false;
        //        Debug.LogError("======after show ads");
        //    }
        //    else
        //    {
        //      //  Debug.LogError("======can show ad here");
        //        if (AdsController.instance != null)
        //            AdsController.instance.ShowOpenAdsAfterChangeApp();
        //        // ShowInter();
        //    }
        //}
    }

    public void ShowInter()
    {

        //#if !UNITY_EDITOR
        if (saveData.removeAds)
            return;

        DataParam.lastShowInter = System.DateTime.Now;
        if ((DataParam.lastShowInter - DataParam.beginShowInter).TotalSeconds > DataParam.timeDelayShowAds)
        {
            if (AdsController.instance != null)
                AdsController.instance.ShowInter();
            EventController.AF_INTERS_AD_ELIGIBLE();
        }
        //#endif
    }
}
