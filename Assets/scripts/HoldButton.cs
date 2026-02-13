//Eyad Al Raeeini - 02/10/2026
//hold button for forward movement
using UnityEngine;
using UnityEngine.EventSystems;
public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public MobileFirstPerson player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player != null)
            player.ForwardDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (player != null)
            player.ForwardUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (player != null)
            player.ForwardUp();
    }

    void OnDisable()
    {
        if (player != null)
            player.ForwardUp();
    }
}