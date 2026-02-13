using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    public static SwipeInput Instance;

    public float minSwipeDistance = 80f;

    private Vector2 startPos;
    private bool swiping;
    private bool active;

    private CombatManager combat;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Begin(CombatManager manager)
    {
        combat = manager;
        active = true;
    }

    public void End()
    {
        active = false;
        swiping = false;
        combat = null;
    }

    void Update()
    {
        if (!active) return;

        // Touch (mobile)
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                startPos = t.position;
                swiping = true;
            }
            else if (t.phase == TouchPhase.Ended && swiping)
            {
                HandleSwipe(startPos, t.position);
                swiping = false;
            }
        }
        // Mouse (Editor testing)
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
                swiping = true;
            }
            else if (Input.GetMouseButtonUp(0) && swiping)
            {
                HandleSwipe(startPos, (Vector2)Input.mousePosition);
                swiping = false;
            }
        }
    }

    void HandleSwipe(Vector2 start, Vector2 end)
    {
        Vector2 delta = end - start;

        if (delta.magnitude < minSwipeDistance)
            return;

        Direction dir;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            dir = (delta.x > 0) ? Direction.Right : Direction.Left;
        else
            dir = (delta.y > 0) ? Direction.Up : Direction.Down;

        combat.ReceiveSwipe(dir);
    }
}
