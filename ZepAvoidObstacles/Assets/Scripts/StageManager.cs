using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    MiniGameManager gm;
    Obstacle obstacle;
    SpriteRenderer top;
    SpriteRenderer bottom;
    public Transform topObject;
    public Transform bottomObject;

    static StageManager stageManager;

    public static StageManager _instance { get { return stageManager; } }

    private int currentStageNum = 1;
    private int maxStageNum = 3;

    // Start is called before the first frame update
    void Start()
    {
        stageManager = this;
        gm = MiniGameManager._instance;
    }
}
