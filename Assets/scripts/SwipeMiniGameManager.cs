//Eyad Al Raeeini - 02/17/2026
//swipe minigame manager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SwipeMiniGameManager : MonoBehaviour
{
    public static SwipeMiniGameManager Instance;

    public GameObject miniGamePanel;
    public TMP_Text comboText;
    public TMP_Text timerText;
    public TMP_Text resultText;
    public UIDrawTrail uiTrail;

    public float minSwipeDistance = 80f;
    public float showComboSeconds = 5f;

    private GameObject currentEnemy;
    private PlayerHealth playerHealth;

    private List<Direction> combo;
    private List<Direction> playerInput;

    private bool inputEnabled;
    private Vector2 swipeStart;
    private int damageOnFail;

    void Awake()
    {
        Instance = this;

        if (miniGamePanel != null)
            miniGamePanel.SetActive(false);

        if (resultText != null)
            resultText.text = "";
    }

    public void StartMiniGame(GameObject enemy, GameObject player, int comboLength, int damage)
    {
        currentEnemy = enemy;
        damageOnFail = damage;

        playerHealth = player.GetComponent<PlayerHealth>();

        combo = GenerateCombo(comboLength);
        playerInput = new List<Direction>();

        miniGamePanel.SetActive(true);

        if (resultText != null)
            resultText.text = "";

        StartCoroutine(ShowComboThenPlay());
    }

    IEnumerator ShowComboThenPlay()
    {
        inputEnabled = false;
        uiTrail.Clear();

        comboText.text = "";
        if (timerText != null)
            timerText.text = "Memorize";

        float delayBetweenArrows = showComboSeconds / combo.Count;

        foreach (Direction d in combo)
        {
            comboText.text = DirectionToArrow(d);
            yield return new WaitForSeconds(delayBetweenArrows);
        }

        comboText.text = "";
        if (timerText != null)
            timerText.text = "Swipe Now!";

        inputEnabled = true;
    }

    void Update()
    {
        if (!miniGamePanel.activeSelf || !inputEnabled)
            return;

        if (Pointer.current == null)
            return;

        Vector2 pos = Pointer.current.position.ReadValue();

        if (Pointer.current.press.wasPressedThisFrame)
        {
            swipeStart = pos;
            uiTrail.Clear();
            uiTrail.Begin();
        }

        if (Pointer.current.press.wasReleasedThisFrame)
        {
            uiTrail.End();

            Vector2 delta = pos - swipeStart;

            if (delta.magnitude < minSwipeDistance)
                return;

            Direction dir = GetDirection(delta);
            playerInput.Add(dir);

            if (playerInput.Count >= combo.Count)
            {
                inputEnabled = false;
                Evaluate();
            }
        }
    }

    void Evaluate()
    {
        bool success = true;

        for (int i = 0; i < combo.Count; i++)
        {
            if (playerInput[i] != combo[i])
            {
                success = false;
                break;
            }
        }

        if (success)
        {
            if (resultText != null)
                resultText.text = "SUCCESS";

            if (currentEnemy != null)
                Destroy(currentEnemy);
        }
        else
        {
            if (resultText != null)
                resultText.text = "FAILED";

            if (playerHealth != null)
                playerHealth.TakeDamage(damageOnFail);
        }

        StartCoroutine(CloseAfterDelay(1f));
    }

    IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        miniGamePanel.SetActive(false);
        uiTrail.Clear();
    }

    List<Direction> GenerateCombo(int length)
    {
        List<Direction> list = new List<Direction>();
        for (int i = 0; i < length; i++)
        {
            list.Add((Direction)Random.Range(0, 4));
        }
        return list;
    }

    Direction GetDirection(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
                return Direction.Right;
            else
                return Direction.Left;
        }
        else
        {
            if (delta.y > 0)
                return Direction.Up;
            else
                return Direction.Down;
        }
    }

    string DirectionToArrow(Direction d)
    {
        if (d == Direction.Up)
            return "UP";
        if (d == Direction.Down)
            return "DOWN";
        if (d == Direction.Left)
            return "LEFT";
        if (d == Direction.Right)
            return "RIGHT";
        return "?";
    }
}