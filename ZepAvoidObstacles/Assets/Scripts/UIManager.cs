using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MiniGameUIManager : MonoBehaviour
{
    private readonly string CURREN_LEVEL_DISPLAY = "Level : ";
    private readonly string POINT_UNIT = " Point";
    private readonly string STAGE_UNIT = " Stage";

    public MiniGameManager gm;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI newStageText;

    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI reachedStageBest;
    public GameObject panel;

    public Button restartButton;
    public Button ExitButton;
    public Button TitleButton;

    // Start is called before the first frame update
    void Start()
    {
        gm = MiniGameManager._instance;
        if (restartText == null)
            Debug.LogError("restart text is null");
        if (scoreText == null)
            Debug.LogError("score text is null");
        if (levelText == null)
            Debug.LogError("level text is null");
        if (newStageText == null)
            Debug.LogError("new stage text is null");

        restartText.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);

        restartButton.onClick.AddListener(Retry);
        ExitButton.onClick.AddListener(Exit);
        TitleButton.onClick.AddListener(BackTitle);
    }

    public void Retry()
    {
        gm.RestartGame();
    }

    public void Exit()
    {
        gm.BackToMain();
    }
    public void BackTitle()
    {
        gm.BackToTitle();
    }

    public void SetRestart(int best, int current, int stage)
    {
        panel.gameObject.SetActive(true);
        bestScore.text = best.ToString() + POINT_UNIT;
        currentScore.text = current.ToString() + POINT_UNIT;
        reachedStageBest.text = stage.ToString() + STAGE_UNIT;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateLevel(int level)
    {
        levelText.text = CURREN_LEVEL_DISPLAY + level.ToString();
    }

    public void ShowNextStage(float duration)
    {
        newStageText.gameObject.SetActive(true);
        StartCoroutine(HideAfterDelay(duration));
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        newStageText.gameObject.SetActive(false);
    }
}
