using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class MiniGameUIManager : MonoBehaviour
{
    private readonly string CURREN_LEVEL_DISPLAY = "Level : ";
    private readonly int SHOW_COUNT = 3;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI newStageText;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (restartText == null)
            Debug.LogError("restart text is null");
        if (scoreText == null)
            Debug.LogError("score text is null");
        if (levelText == null)
            Debug.LogError("level text is null");
        if (newStageText == null)
            Debug.LogError("new stage text is null");

        restartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        restartText.gameObject.SetActive(true);
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
