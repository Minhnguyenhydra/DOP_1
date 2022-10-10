 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DarkcupGames {
    public enum UIEffectElementType{
        Appear, RunText
    } 

    public class UIEffectElement : MonoBehaviour {
        public UIEffectElementType effectElementType = UIEffectElementType.Appear;
        public float effectTime = 0.2f;

        public long number;

        public void PrepareEffect() {
            switch (effectElementType) {
                case UIEffectElementType.Appear:
                    gameObject.SetActive(false);
                    break;

                case UIEffectElementType.RunText:
                    var text = GetComponent<TextMeshProUGUI>();
                    if (text != null) {
                        long.TryParse(text.text, out number);
                        text.text = "0";
                    }
                    break;
            }
        }

        public IEnumerator IEDoEffect() {
            switch (effectElementType) {
                case UIEffectElementType.Appear:
                    EasyEffect.Appear(gameObject, 0f, 1f, effectTime);
                    yield return new WaitForSeconds(effectTime);
                    break;

                case UIEffectElementType.RunText:
                    var text = GetComponent<TextMeshProUGUI>();
                    if (text != null) {
                        EasyEffect.RunTextNumber(text, 0, number, effectTime);
                    }
                    yield return new WaitForSeconds(effectTime);
                    break;
            }
        }
    }
}