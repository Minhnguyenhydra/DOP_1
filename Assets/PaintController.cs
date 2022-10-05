using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintController : MonoBehaviour
{
    public Color paintColor = Color.clear;
    public Vector2Int lastPos;
    public Vector2 lastWorldPos;
    public List<Vector2> drawPoints;
    public bool isDrawing = false;
    public int erSize = 10;

    protected Texture2D m_Texture;
    protected RaycastHit2D hit;

    protected Color[] originalColors;
    protected Color[] m_Colors;

    bool canDraw = false;

    public virtual Texture2D GetSourceTexture() {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) {
            return renderer.sprite.texture;
        }
        return null;
    }

    public virtual void ApplyTexture(Texture2D texture2D) {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) {
            renderer.sprite = Sprite.Create(m_Texture, renderer.sprite.rect, new Vector2(0.5f, 0.5f));
        }
    }

    public virtual float GetTargetPercent() {
        return Constants.ERASE_PERCENT_REQUIRE;
    }

    public virtual void Start() {
        Init();
    }

    public void Init() {
        var tex = GetSourceTexture();
        m_Texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        m_Texture.filterMode = FilterMode.Bilinear;
        m_Texture.wrapMode = TextureWrapMode.Clamp;
        m_Colors = tex.GetPixels();
        originalColors = tex.GetPixels();
        m_Texture.SetPixels(m_Colors);
        m_Texture.Apply();
        drawPoints = new List<Vector2>();
        canDraw = false;

        ApplyTexture(m_Texture);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            canDraw = true;
        }

        if (Input.GetMouseButton(0) && canDraw) {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Debug.Log("hit = " + hit);

            if (hit.collider != null && hit.collider.gameObject == gameObject) {
                Debug.Log("this is collider: " + hit.collider);

                UpdateTexture();
                isDrawing = true;
            }
        } else
            isDrawing = false;
    }

    public void UpdateTexture() {
        int w = m_Texture.width;
        int h = m_Texture.height;
        lastWorldPos = hit.point;
        drawPoints.Add(hit.point);

        var mousePos = hit.point - (Vector2)hit.collider.bounds.min;
        mousePos.x *= w / hit.collider.bounds.size.x;
        mousePos.y *= h / hit.collider.bounds.size.y;
        Vector2Int p = new Vector2Int((int)mousePos.x, (int)mousePos.y);
        Vector2Int start = new Vector2Int();
        Vector2Int end = new Vector2Int();
        if (!isDrawing)
            lastPos = p;
        start.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) - erSize, 0, w);
        start.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) - erSize, 0, h);
        end.x = Mathf.Clamp(Mathf.Max(p.x, lastPos.x) + erSize, 0, w);
        end.y = Mathf.Clamp(Mathf.Max(p.y, lastPos.y) + erSize, 0, h);
        Vector2 dir = p - lastPos;
        for (int x = start.x; x < end.x; x++) {
            for (int y = start.y; y < end.y; y++) {
                Vector2 pixel = new Vector2(x, y);
                Vector2 linePos = p;
                if (isDrawing) {
                    float d = Vector2.Dot(pixel - lastPos, dir) / dir.sqrMagnitude;
                    d = Mathf.Clamp01(d);
                    linePos = Vector2.Lerp(lastPos, p, d);
                }

                if ((pixel - linePos).sqrMagnitude <= erSize * erSize) {
                    m_Colors[x + y * w] = paintColor;
                }
            }
        }
        lastPos = p;
        m_Texture.SetPixels(m_Colors);
        m_Texture.Apply();

        ApplyTexture(m_Texture);
    }

    public bool IsDrawFinished() {
        if (m_Colors == null) return false;

        float count = 0;

        for (int i = 0; i < m_Colors.Length; i++) {
            if (m_Colors[i] == paintColor) {
                count++;
            }
        }

        float percent = count / m_Colors.Length;
        Debug.Log("percent = " + percent);

        return percent > Constants.ERASE_PERCENT_REQUIRE;
    }

    public void ClearDraw() {
        m_Texture.SetPixels(originalColors);
        m_Texture.Apply();
        ApplyTexture(m_Texture);

        Init();
    }
}
