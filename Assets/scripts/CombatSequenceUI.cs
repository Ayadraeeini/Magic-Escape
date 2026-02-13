//Eyad Al Raeeini - 11/14/2025
//combat sequence ui display/


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatSequenceUI : MonoBehaviour
{
    public Transform container;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float completedAlpha = 0.25f;

    public void ShowSequence(List<Direction> seq, int currentIndex)
    {
        Clear();

        for (int i = 0; i < seq.Count; i++)
        {
            GameObject go = new GameObject("Step_" + i);
            go.transform.SetParent(container, false);

            Image img = go.AddComponent<Image>();
            img.sprite = GetSprite(seq[i]);
            img.raycastTarget = false;

            Color c = img.color;
            if (i < currentIndex)
                c.a = completedAlpha;
            else
                c.a = 1f;
            img.color = c;

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(70, 70);
        }
    }

    void Clear()
    {
        for (int i = container.childCount - 1; i >= 0; i--)
            Destroy(container.GetChild(i).gameObject);
    }

    Sprite GetSprite(Direction d)
    {
         if (d == Direction.Up)
            return upSprite;
        else if (d == Direction.Down)
            return downSprite;
        else if (d == Direction.Left)
            return leftSprite;
        else if (d == Direction.Right)
             return rightSprite;
        else
            return upSprite;
    }
}