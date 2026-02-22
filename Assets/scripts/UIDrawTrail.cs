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

    private Texture2D tex;
    private Color32[] clearPixels;
    private bool drawing;

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
        Clear();
    }

    public void End()
    {
        drawing = false;
    }

    public void Clear()
    {
        tex.SetPixels32(clearPixels);
        tex.Apply(false);
    }

    void Update()
    {
        if (!drawing) return;
        if (Pointer.current == null) return;
         if (!Pointer.current.press.isPressed) return;

         Vector2 screenPos = Pointer.current.position.ReadValue();

        Vector2 local;
         if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, null, out local))
            return;
        Vector2 size = rect.rect.size;
        float u = (local.x / size.x) + 0.5f;
        float v = (local.y / size.y) + 0.5f;

        if (u < 0 || u > 1 || v < 0 || v > 1) return;
        int x = Mathf.RoundToInt(u * (textureSize - 1));
        int y = Mathf.RoundToInt(v * (textureSize - 1));

        DrawCircle(x, y, brushSize, new Color32(0, 255, 255, 255));
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