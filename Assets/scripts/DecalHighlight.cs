//Eyad Al Raeeini - 02/17/2026
//decal highlight on player proximity

using UnityEngine;

public class DecalHighlight : MonoBehaviour
{
    public Renderer decalRenderer;

    public float showDistance = 4f;
    public float fadeSpeed = 5f;

    private Transform player;
    private Color originalColor;
    private float targetAlpha = 0f;

    void Start()
    {
        if (decalRenderer == null)
            decalRenderer = GetComponent<Renderer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        originalColor = decalRenderer.material.color;

        // start invisible
        SetAlpha(0f);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= showDistance)
            targetAlpha = 1f;
        else
            targetAlpha = 0f;

        float currentAlpha = decalRenderer.material.color.a;
        float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);

        SetAlpha(newAlpha);
    }

    void SetAlpha(float a)
    {
        Color c = originalColor;
        c.a = a;
        decalRenderer.material.color = c;
    }
}