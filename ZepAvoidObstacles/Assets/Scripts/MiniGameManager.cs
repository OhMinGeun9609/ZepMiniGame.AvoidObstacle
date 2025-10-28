using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    private static readonly float UI_SHOW_TIME = 1f;
    private static readonly int DEFAULT_LEVEL = 1;


    static MiniGameManager miniGameManager;
    public static MiniGameManager _instance { get { return miniGameManager; } }

    private int currentScore = 0;

    MiniGameUIManager miniGameUiManager;

    Player player;
    public Player PlayerInfo { get { return player; } }

    StageManager stageManager;

    ScoreRecorder scoreRecord;

    public float playTimeDelta = 0;

    private int stage = 1;
    private bool check = false;
    private int gameLevel = 1;
    private int best = 0;
    

    private void Awake()
    {
        Time.timeScale = 1f;
        miniGameManager = this;
        miniGameUiManager = FindObjectOfType<MiniGameUIManager>();
        player = FindObjectOfType<Player>();
        stageManager = GetComponentInChildren<StageManager>();
        scoreRecord = GetComponentInChildren<ScoreRecorder>();
    }

    private void Start()
    {
        miniGameUiManager.UpdateLevel(gameLevel);
        miniGameUiManager.UpdateScore(0);
    }

    private void Update()
    {
        if (stageManager == null) Debug.Log("stageManager is null");

        playTimeDelta += Time.deltaTime;
        stageManager.AddInterrupt(stage);
    }

    public void GameOver()
    {
        best = scoreRecord.JudgeAndSet(currentScore, stage);
        miniGameUiManager.SetRestart(currentScore, best, stage);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("ZepScene");
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("MiniTitle");
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        miniGameUiManager.UpdateScore(currentScore);

        if (currentScore % 5 == 0)
        {
            gameLevel++;
            player.PlayerLevelUp(gameLevel);
            miniGameUiManager.UpdateLevel(gameLevel);

            check = stageManager.IsStageClear(gameLevel, playTimeDelta);

            if(check)
            {
                miniGameUiManager.ShowNextStage(UI_SHOW_TIME);
                player.ClearAndSpeedReset();

                gameLevel = DEFAULT_LEVEL;
                stage++;
            }
        }
    }
}
