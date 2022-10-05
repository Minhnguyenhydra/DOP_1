using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintToSpriteMaskController : PaintController
{
    public SpriteMask spriteMask;

    public override void Start() {
        spriteMask = GetComponent<SpriteMask>();
        base.Start();
    }

    public override void ApplyTexture(Texture2D texture2D) {
        spriteMask.sprite = Sprite.Create(m_Texture, spriteMask.sprite.rect, new Vector2(0.5f, 0.5f));
    }

    public override Texture2D GetSourceTexture() {
        return spriteMask.sprite.texture;
    }

    public override float GetTargetPercent() {
        return Constants.ERASE_PERCENT_REQUIRE;
    }
}
