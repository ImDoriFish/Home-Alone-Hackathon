using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Task State")]
    public bool door1Done = false;
    public bool door2Done = false;
    public bool lightDone = false;

    [Header("Timer")]
    public float startTime = 240f; // 4 minutes
    private float timeRemaining;
    public TextMeshProUGUI timerText;

    [Header("Scene Names")]
    public string winSceneName = "End";
    public string loseSceneName = "EndFail";

    private bool gameEnded = false;

    void Start()
    {
        timeRemaining = startTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (gameEnded)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0f)
            timeRemaining = 0f;

        UpdateTimerUI();

        if (AllTasksDone())
        {
            WinGame();
            return;
        }

        if (timeRemaining <= 0f)
        {
            LoseGame();
        }
    }

    public bool AllTasksDone()
    {
        return door1Done && door2Done && lightDone;
    }

    public void SetDoorDone(int doorID, bool done)
    {
        if (doorID == 1)
        {
            door1Done = done;
        }
        else if (doorID == 2)
        {
            door2Done = done;
        }
    }

    public void SetLightDone(bool done)
    {
        lightDone = done;
    }

    void WinGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;
        SceneManager.LoadScene(winSceneName);
    }

    void LoseGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;
        SceneManager.LoadScene(loseSceneName);
    }

    void UpdateTimerUI()
    {
        if (timerText == null)
            return;

        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}