using UnityEngine;

public class ColorCycle : MonoBehaviour
{
    public float speed = 0.2f;          // how fast color changes
    public float saturation = 1f;       // 0-1
    public float brightness = 1f;       // 0-1
    public bool useEmission = false;    // optional glow
    public float emissionStrength = 2f;

    private Renderer rend;
    private MaterialPropertyBlock mpb;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
    }

    void Update()
    {
        if (rend == null) return;

        // Smooth hue cycle
        float hue = Mathf.Repeat(Time.time * speed, 1f);
        Color color = Color.HSVToRGB(hue, saturation, brightness);

        rend.GetPropertyBlock(mpb);

        // Try URP first
        if (rend.sharedMaterial.HasProperty("_BaseColor"))
            mpb.SetColor("_BaseColor", color);
        else
            mpb.SetColor("_Color", color);

        // Optional glow
        if (useEmission && rend.sharedMaterial.HasProperty("_EmissionColor"))
        {
            rend.sharedMaterial.EnableKeyword("_EMISSION");
            mpb.SetColor("_EmissionColor", color * emissionStrength);
        }

        rend.SetPropertyBlock(mpb);
    }
}