//Eyad Al Raeeini - 02/17/2026
//swipe minigame manager with retries, damage, pushback, and fail sound

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

    public int maxAttempts = 3;
    public int damageOnFail = 20;

    public MonoBehaviour playerMovementScript;

    public Transform cameraToShake;
    public float shakeDuration = 0.18f;
    public float shakeStrength = 0.12f;

    public float pushBackForce = 3f;

    public AudioSource audioSource;
    public AudioClip failSound;

    private GameObject currentEnemy;
    private GameObject currentPlayer;

    private PlayerHealth playerHealth;
    private HUDManager playerHUD;

    private List<Direction> combo;
    private List<Direction> playerInput;

    private bool inputEnabled;
    private bool miniGameRunning;

    private Vector2 swipeStart;

    private int attemptsLeft;

    private MonoBehaviour cachedMovement;

    private Vector3 camStartLocalPos;

    void Awake()
    {
        Instance = this;

        if (miniGamePanel != null)
            miniGamePanel.SetActive(false);

        if (resultText != null)
            resultText.text = "";

        if (cameraToShake != null)
            camStartLocalPos = cameraToShake.localPosition;
    }

    public void StartMiniGame(GameObject enemy, GameObject player, int comboLength, int damage)
    {
        if (miniGameRunning)
            return;

        miniGameRunning = true;

        currentEnemy = enemy;
        currentPlayer = player;

        playerHealth = player.GetComponent<PlayerHealth>();

        playerHUD = FindObjectOfType<HUDManager>();

        if (playerHUD != null)
            playerHUD.ShowHP();

        if (comboLength < 1)
            comboLength = 1;

        combo = GenerateCombo(comboLength);

        playerInput = new List<Direction>();

        attemptsLeft = maxAttempts;

        if (attemptsLeft < 1)
            attemptsLeft = 1;

        FreezePlayer(player);

        if (miniGamePanel != null)
            miniGamePanel.SetActive(true);

        if (resultText != null)
            resultText.text = "";

        if (timerText != null)
            timerText.text = "Memorize";

        if (comboText != null)
            comboText.text = "";

        if (uiTrail != null)
            uiTrail.Clear();

        StartCoroutine(ShowComboThenPlay());
    }

    IEnumerator ShowComboThenPlay()
    {
        inputEnabled = false;

        if (uiTrail != null)
            uiTrail.Clear();

        if (timerText != null)
            timerText.text = "Memorize";

        int count = combo.Count;

        if (count < 1)
            count = 1;

        float delayBetweenArrows = showComboSeconds / count;

        for (int i = 0; i < combo.Count; i++)
        {
            comboText.text = "";

            yield return new WaitForSecondsRealtime(0.05f);

            comboText.text = DirectionToArrow(combo[i]);

            float remaining = delayBetweenArrows - 0.05f;

            if (remaining < 0.05f)
                remaining = 0.05f;

            yield return new WaitForSecondsRealtime(remaining);
        }

        yield return new WaitForSecondsRealtime(0.2f);

        comboText.text = "";

        if (timerText != null)
            timerText.text = "Swipe Now!";

        inputEnabled = true;
    }

    void Update()
    {
        if (!miniGameRunning)
            return;

        if (miniGamePanel == null || !miniGamePanel.activeSelf)
            return;

        if (!inputEnabled)
            return;

        if (Pointer.current == null)
            return;

        Vector2 pos = Pointer.current.position.ReadValue();

        if (Pointer.current.press.wasPressedThisFrame)
        {
            swipeStart = pos;

            if (uiTrail != null)
            {
                uiTrail.Clear();
                uiTrail.Begin();
            }
        }

        if (Pointer.current.press.wasReleasedThisFrame)
        {
            if (uiTrail != null)
                uiTrail.End();

            Vector2 delta = pos - swipeStart;

            if (delta.magnitude < minSwipeDistance)
                return;

            Direction dir = GetDirection(delta);

            playerInput.Add(dir);

            if (playerInput.Count >= combo.Count)
            {
                inputEnabled = false;

                EvaluateAttempt();
            }
        }
    }

    void EvaluateAttempt()
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

            StartCoroutine(EndMiniGameAfter(0.6f));

            return;
        }

        attemptsLeft--;

        ShakeScreen();

        if (playerHealth != null)
            playerHealth.TakeDamage(damageOnFail);

        if (attemptsLeft > 0)
        {
            if (resultText != null)
                resultText.text = "WRONG!";

            StartCoroutine(FailSequence());
        }
        else
        {
            if (resultText != null)
                resultText.text = "FAILED";

            if (audioSource != null && failSound != null)
                audioSource.PlayOneShot(failSound);

            PushPlayerBack();

            StartCoroutine(ResetEnemyTrigger());

            StartCoroutine(EndMiniGameAfter(0.8f));
        }
    }

    IEnumerator FailSequence()
    {
        inputEnabled = false;

        yield return new WaitForSecondsRealtime(shakeDuration);

        combo = GenerateCombo(combo.Count);

        playerInput.Clear();

        if (uiTrail != null)
            uiTrail.Clear();

        if (resultText != null)
            resultText.text = "";

        if (timerText != null)
            timerText.text = "Memorize";

        if (comboText != null)
            comboText.text = "";

        StartCoroutine(ShowComboThenPlay());
    }

    IEnumerator ResetEnemyTrigger()
    {
        if (currentEnemy == null)
            yield break;

        EnemyMiniGameTrigger trigger =
            currentEnemy.GetComponent<EnemyMiniGameTrigger>();

        if (trigger == null)
            yield break;

        yield return new WaitForSeconds(1f);

        trigger.ResetTrigger();
    }

    IEnumerator EndMiniGameAfter(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (miniGamePanel != null)
            miniGamePanel.SetActive(false);

        if (uiTrail != null)
            uiTrail.Clear();

        UnfreezePlayer();

        if (playerHUD != null)
            playerHUD.HideHP();

        inputEnabled = false;

        miniGameRunning = false;
    }

    void FreezePlayer(GameObject player)
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
            return;
        }

        cachedMovement = player.GetComponent<MobileFirstPerson>();

        if (cachedMovement != null)
            cachedMovement.enabled = false;
    }

    void UnfreezePlayer()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
            return;
        }

        if (cachedMovement != null)
            cachedMovement.enabled = true;

        cachedMovement = null;
    }

    void PushPlayerBack()
    {
        if (currentPlayer == null)
            return;

        CharacterController cc =
            currentPlayer.GetComponent<CharacterController>();

        if (cc == null)
            return;

        Vector3 pushDir = -currentPlayer.transform.forward;

        cc.Move(pushDir * pushBackForce);
    }

    void ShakeScreen()
    {
        if (cameraToShake == null)
            return;

        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        if (cameraToShake == null)
            yield break;

        camStartLocalPos = cameraToShake.localPosition;

        float t = shakeDuration;

        while (t > 0f)
        {
            Vector2 r =
                Random.insideUnitCircle * shakeStrength;

            cameraToShake.localPosition =
                camStartLocalPos + new Vector3(r.x, r.y, 0f);

            t -= Time.unscaledDeltaTime;

            yield return null;
        }

        cameraToShake.localPosition = camStartLocalPos;
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
            return "↑";

        if (d == Direction.Down)
            return "↓";

        if (d == Direction.Left)
            return "←";

        if (d == Direction.Right)
            return "→";

        return "?";
    }
}