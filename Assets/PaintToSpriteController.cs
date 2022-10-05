using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DarkcupGames;
using UnityEngine.SceneManagement;

namespace DarkcupGames {
    public class PaintToSpriteController : PaintController {
        SpriteRenderer spriteRenderer;

        public override void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            base.Start();
        }

        public override void ApplyTexture(Texture2D texture2D) {
            spriteRenderer.sprite = Sprite.Create(m_Texture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));
        }

        public override Texture2D GetSourceTexture() {
            return spriteRenderer.sprite.texture;
        }

        public override float GetTargetPercent() {
            return Constants.ERASE_PERCENT_REQUIRE;
        }
    }
}