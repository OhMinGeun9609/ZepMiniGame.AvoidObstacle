using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    private static readonly int DEFAULT_LEVEL = 1;
    private static readonly float UI_SHOW_TIME = 1f;

    static MiniGameManager miniGameManager;
    public static MiniGameManager _instance { get { return miniGameManager; } }

    private int currentScore = 0;

    MiniGameUIManager miniGameUiManager;
    public MiniGameUIManager MiniUIManager { get { return miniGameUiManager; } }

    Player player;
    public Player PlayerInfo { get { return player; } }

    StageManager stageManager;

    public StageManager Stage { get { return stageManager; } }

    LightManager lightManager;
    LightManager LightManager { get { return lightManager; } }

    public float playTimeDelta = 0;

    private int gameLevel = 25;
    public int stageLevel = 1;

    private void Awake()
    {
        miniGameManager = this;
        miniGameUiManager = FindObjectOfType<MiniGameUIManager>();
        lightManager = FindObjectOfType<LightManager>();
        player = FindObjectOfType<Player>();
        stageManager = StageManager._instance;

        DontDestroyOnLoad(lightManager);
    }

    private void Start()
    {
        miniGameUiManager.UpdateLevel(gameLevel);
        miniGameUiManager.UpdateScore(0);
    }

    private void Update()
    {
        playTimeDelta += Time.deltaTime;    
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        miniGameUiManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            lightManager.TurnUp();
            miniGameUiManager.UpdateLevel(gameLevel);
        }

        if (gameLevel == 26)
        {
            stageLevel++;

            miniGameUiManager.ShowNextStage(UI_SHOW_TIME);

            gameLevel = DEFAULT_LEVEL;
            player.ClearAndSpeedReset();
        }
    }
}
