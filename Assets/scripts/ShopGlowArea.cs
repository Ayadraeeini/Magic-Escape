//Eyad Al Raeeini - 03/17/2026
//shop glow area animation//
using UnityEngine;
public class ShopGlowArea : MonoBehaviour
{
    public Renderer[] outlines;
    public float colorSpeed = 0.5f;
    public float emissionStrength = 2.5f;

    void Update()
    {
        if (outlines == null || outlines.Length == 0)
            return;

        float hue = Mathf.Repeat(Time.time * colorSpeed, 1f);
        Color color = Color.HSVToRGB(hue, 1f, 1f);
        foreach (Renderer r in outlines)
        {
            if (r == null) continue;

            Material mat = r.material;

            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", color);
             else
                mat.SetColor("_Color", color);

            if (mat.HasProperty("_EmissionColor"))
            {
                mat.EnableKeyword("_EMISSION");
                 mat.SetColor("_EmissionColor", color * emissionStrength);
            }
        }
    }
}