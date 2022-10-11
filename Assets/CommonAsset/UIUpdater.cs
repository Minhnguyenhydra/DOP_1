using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using System.ComponentModel;
using System;

namespace DarkcupGames {
    public class TestClassData : System.Object {
        public string demoSprite;
        public int amount;
        public string itemName;
    }

    public enum UIInfoType {
        Button, Image, Text
    }

    [System.Serializable]
    public class ObjectComponentInfo {
        public List<UIInfo> texts = new List<UIInfo>();
        public List<UIInfo> imgs = new List<UIInfo>();
        public List<UIInfo> buttons = new List<UIInfo>();
    }

    [System.Serializable]
    public class UIInfo {
        public string uiName;
        public UIInfoType type;
        public MonoBehaviour component;
        public string originalText = "";
    }

    public class UIUpdater : MonoBehaviour {
        public bool appendText = false;
        bool inited = false;

        ObjectComponentInfo componentInfo;
        Dictionary<GameObject, ObjectComponentInfo> dicComponent = new Dictionary<GameObject, ObjectComponentInfo>();

        //List<UIInfo> texts = new List<UIInfo>();
        //List<UIInfo> imgs = new List<UIInfo>();
        //List<UIInfo> buttons = new List<UIInfo>();

        private void Start() {
            if (componentInfo == null) {
                componentInfo = GetComponentInfo(gameObject);
            }
        }

        ObjectComponentInfo GetComponentInfo(GameObject obj) {
            //if (inited) return;

            //inited = true;
            ObjectComponentInfo info = new ObjectComponentInfo();

            var allTexts = obj.GetComponentsInChildren<TextMeshProUGUI>(true);
            if (allTexts != null && allTexts.Length > 0) {
                for (int i = 0; i < allTexts.Length; i++) {
                    info.texts.Add(new UIInfo() {
                        uiName = allTexts[i].gameObject.name,
                        type = UIInfoType.Text,
                        component = allTexts[i],
                        originalText = allTexts[i].text
                    });
                }
            }

            var allImages = obj.GetComponentsInChildren<Image>(true);
            if (allImages != null && allImages.Length > 0) {
                for (int i = 0; i < allImages.Length; i++) {
                    info.imgs.Add(new UIInfo() {
                        uiName = allImages[i].gameObject.name,
                        type = UIInfoType.Image,
                        component = allImages[i]
                    });
                }
            }

            var allButtons = obj.GetComponentsInChildren<Button>(true);
            if (allButtons != null && allButtons.Length > 0) {
                for (int i = 0; i < allButtons.Length; i++) {
                    info.buttons.Add(new UIInfo() {
                        uiName = allButtons[i].gameObject.name,
                        type = UIInfoType.Button,
                        component = allButtons[i]
                    });
                }
            }

            return info;
        }

        /// <summary>
        /// Update data to gameObject attached by this script, child Object with same name with the field name will receive data
        /// </summary>
        /// <param name="sourceData"></param>
        public void UpdateUI(object sourceData) {
            UpdateUI(sourceData, gameObject, componentInfo);
        }

        /// <summary>
        /// Update data from source data to targetObject, child Object with same name with the field name will receive data
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetObject"></param>
        public void UpdateUI(object sourceData, GameObject targetObject) {
            UIUpdaterData dataContainer = targetObject.GetComponent<UIUpdaterData>();

            if (dataContainer != null) {
                dataContainer.data = sourceData;
            } else {
                targetObject.AddComponent(typeof(UIUpdaterData));
                dataContainer = targetObject.GetComponent<UIUpdaterData>();
                dataContainer.data = sourceData;
            }

            ObjectComponentInfo info = null;
            if (dicComponent.ContainsKey(targetObject)) {
                info = dicComponent[targetObject];
            } else {
                info = GetComponentInfo(targetObject);
                dicComponent.Add(targetObject, info);
            }

            //List<UIInfo> texts = new List<UIInfo>();
            //List<UIInfo> imgs = new List<UIInfo>();
            //List<UIInfo> buttons = new List<UIInfo>();

            //var allTexts = targetObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            //if (allTexts != null && allTexts.Length > 0) {
            //    for (int i = 0; i < allTexts.Length; i++) {
            //        texts.Add(new UIInfo() {
            //            uiName = allTexts[i].gameObject.name,
            //            type = UIInfoType.Text,
            //            component = allTexts[i]
            //        });
            //    }
            //}

            //var allImages = targetObject.GetComponentsInChildren<Image>(true);
            //if (allImages != null && allImages.Length > 0) {
            //    for (int i = 0; i < allImages.Length; i++) {
            //        imgs.Add(new UIInfo() {
            //            uiName = allImages[i].gameObject.name,
            //            type = UIInfoType.Image,
            //            component = allImages[i]
            //        });
            //    }
            //}

            //var allButtons = targetObject.GetComponentsInChildren<Button>(true);
            //if (allButtons != null && allButtons.Length > 0) {
            //    for (int i = 0; i < allButtons.Length; i++) {
            //        buttons.Add(new UIInfo() {
            //            uiName = allButtons[i].gameObject.name,
            //            type = UIInfoType.Button,
            //            component = allButtons[i]
            //        });
            //    }
            //}

            UpdateUI(sourceData, targetObject, info);
        }

        void UpdateUI(object sourceData, GameObject targetObject, ObjectComponentInfo info) {
            MemberInfo[] members = sourceData.GetType().GetMembers();

            List<UIInfo> texts = info.texts;
            List<UIInfo> imgs = info.imgs;
            List<UIInfo> buttons = info.buttons;

            foreach (MemberInfo memberInfo in members) {
                string memberName = memberInfo.Name;

                for (int i = 0; i < texts.Count; i++) {
                    if (texts[i].uiName == memberName) {
                        object dataValue = GetMemberValue(memberInfo, sourceData);

                        if (appendText && texts[i].originalText != "") {
                            texts[i].component.GetComponent<TextMeshProUGUI>().text = texts[i].originalText + " " + dataValue.ToString();
                        } else {
                            texts[i].component.GetComponent<TextMeshProUGUI>().text = dataValue.ToString();
                        }
                    }
                }

                for (int i = 0; i < imgs.Count; i++) {
                    if (imgs[i].uiName == memberName) {
                        object dataValue = GetMemberValue(memberInfo, sourceData);

                        if (dataValue == null) continue;

                        if (dataValue.GetType() == typeof(Sprite)) {
                            imgs[i].component.GetComponent<Image>().sprite = (Sprite)dataValue;
                        } else if (dataValue.GetType() == typeof(System.Action)) {

                        } else {
                            imgs[i].component.GetComponent<Image>().sprite = Resources.Load<Sprite>(dataValue.ToString());
                        }
                    }
                }

                for (int i = 0; i < buttons.Count; i++) {
                    if (buttons[i].uiName == memberName) {
                        object dataValue = GetMemberValue(memberInfo, sourceData);
                        if (dataValue == null) continue;

                        if (dataValue.GetType() == typeof(System.Action)) {
                            UnityAction unityAction = new UnityAction((System.Action)dataValue);

                            buttons[i].component.GetComponent<Button>().onClick.RemoveAllListeners();
                            buttons[i].component.GetComponent<Button>().onClick.AddListener(unityAction);
                        }
                    }
                }

                if (memberName == "unlocked") {
                    object dataValue = GetMemberValue(memberInfo, sourceData);

                    if (dataValue == null) continue;

                    GameObject obj = targetObject.GetChildComponent<GameObject>("btnBuy");
                    if (obj != null) {
                        obj.SetActive((bool)dataValue == false);
                    }

                    GameObject objUse = targetObject.GetChildComponent<GameObject>("btnUse");
                    if (objUse != null) {
                        objUse.SetActive((bool)dataValue == true);
                    }
                }
            }
        }

        public void UpdateChildUI<T>(List<T> list) {
            for (int i = 0; i < transform.childCount; i++) {
                if (i < list.Count) {
                    transform.GetChild(i).gameObject.SetActive(true);
                    UpdateUI(list[i], transform.GetChild(i).gameObject);
                } else {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        public void UpdateListShop(List<bool> listUnlocked) {
            for (int i = 0; i < listUnlocked.Count; i++) {
                GameObject obj = gameObject.GetChildComponent<GameObject>("btnBuy");
                if (obj != null) {
                    obj.SetActive(listUnlocked[i] == false);
                }

                GameObject objUse = gameObject.GetChildComponent<GameObject>("btnUse");
                if (objUse != null) {
                    objUse.SetActive(listUnlocked[i] == true);
                }
            }
        }

        public static object GetMemberValue(MemberInfo member, object source) {
            switch (member.MemberType) {
                case MemberTypes.Field:
                    return ((FieldInfo)member).GetValue(source);
                case MemberTypes.Property:
                    try {
                        return ((PropertyInfo)member).GetValue(source, null);
                    } catch (TargetParameterCountException e) {
                        //throw new ArgumentException("MemberInfo has index parameters", "member", e);
                        return null;
                    }
                default:
                    //throw new ArgumentException("MemberInfo is not of type FieldInfo or PropertyInfo", "member");
                    return null;
            }
        }
    }
}