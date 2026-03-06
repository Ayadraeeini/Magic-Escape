//Eyad Al Raeeini - 02/17/2026
//ui draw trail 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class UIDrawTrail : MonoBehaviour
{
    public RawImage raw;
    public RectTransform rect;
    public int textureSize = 512;
    public int brushSize = 10;
    public float minMovePixels = 0f;
    public Color32 brushColor = new Color32(0, 255, 255, 255);
    private Texture2D tex;
    private Color32[] clearPixels;
    private bool drawing;
    private Vector2 lastScreenPos;
    private bool hasLast;

    void Awake()
    {
        if (raw == null)
            raw = GetComponent<RawImage>();
        if (rect == null)
            rect = GetComponent<RectTransform>();

        tex = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Bilinear;
        clearPixels = new Color32[textureSize * textureSize];
        Clear();
        raw.texture = tex;
    }

    public void Begin()
    {
        drawing = true;
        hasLast = false;
        Clear();
    }

    public void End()
    {
        drawing = false;
        hasLast = false;
    }

    public void Clear()
    {
        if (tex == null) return;

        tex.SetPixels32(clearPixels);
        tex.Apply(false);
    }

    void Update()
    {
        if (!drawing) return;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
            TryDraw(screenPos);
            return;
        }

        if (Pointer.current != null && Pointer.current.press.isPressed)
        {
            Vector2 screenPos = Pointer.current.position.ReadValue();
            TryDraw(screenPos);
        }
    }

    void TryDraw(Vector2 screenPos)
    {
        if (hasLast && minMovePixels > 0f)
        {
            if (Vector2.Distance(lastScreenPos, screenPos) < minMovePixels)
                return;
        }

        lastScreenPos = screenPos;
        hasLast = true;

        Vector2 local;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, null, out local))
            return;

        Vector2 size = rect.rect.size;
        float u = (local.x / size.x) + 0.5f;
        float v = (local.y / size.y) + 0.5f;

        if (u < 0f || u > 1f || v < 0f || v > 1f) return;

        int x = Mathf.RoundToInt(u * (textureSize - 1));
        int y = Mathf.RoundToInt(v * (textureSize - 1));

        DrawCircle(x, y, brushSize, brushColor);
        tex.Apply(false);
    }

    void DrawCircle(int cx, int cy, int r, Color32 col)
    {
        int r2 = r * r;
        for (int y = -r; y <= r; y++)
        {
            for (int x = -r; x <= r; x++)
            {
                if (x * x + y * y > r2)
                    continue;

                int px = cx + x;
                int py = cy + y;

                if (px < 0 || px >= textureSize || py < 0 || py >= textureSize)
                    continue;

                tex.SetPixel(px, py, col);
            }
        }
    }
}