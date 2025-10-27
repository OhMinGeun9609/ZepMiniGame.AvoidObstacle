using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScoreRecorder : MonoBehaviour
{
    private static readonly string BEST_SCORE = "best";
    private static readonly string REACHED_STAGE = "ReachedStage";

    int bestScore = 0;
    int reachedMax = 0;

    static ScoreRecorder scoreRecorder;
    public static ScoreRecorder Instance { get { return scoreRecorder; } }

    public void Start()
    {
        scoreRecorder = this;

        LoadBest();
    }

    public void LoadBest()
    {
        bestScore = PlayerPrefs.GetInt(BEST_SCORE, 0);
        reachedMax = PlayerPrefs.GetInt(REACHED_STAGE, 0);
    }

    public int JudgeAndSet(int score, int reached)
    {
        if (bestScore < score)
        {
            bestScore = score;
            reachedMax = reached;
            PlayerPrefs.SetInt(BEST_SCORE, bestScore);
            PlayerPrefs.SetInt(REACHED_STAGE, reachedMax);
        }

        return bestScore;
    }
}
