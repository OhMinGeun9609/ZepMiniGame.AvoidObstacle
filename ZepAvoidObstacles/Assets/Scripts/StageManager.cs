using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static readonly int MAX_LEVEL_IN_STAGE = 25;

    MiniGameManager gm;

    StageManager stageManager;

    public GameObject ammo;
    public GameObject bomb;

    private int prevStageNum = 0;
    private int currentStageNum = 1;
    private int maxStageNum = 3;

    // Start is called before the first frame update
    void Start()
    {
        gm = MiniGameManager._instance;
    }

    public bool IsStageClear(int gameLevel, float time)
    {
        if (gameLevel > MAX_LEVEL_IN_STAGE)
        {
            StageMemory();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StageMemory()
    {
        prevStageNum = currentStageNum;
        currentStageNum++;
    }

    public void AddInterrupt(int stage)
    {
        if (stage == 1)
        {
            Debug.Log("stage 1");
            return;
        }

        if (currentStageNum > prevStageNum  && !IsInvoking("OnAttack"))
        {
            InvokeRepeating("OnAttack", 0.5f, 1f);

            if(currentStageNum == maxStageNum && !IsInvoking("OnExplosive"))
            {
                InvokeRepeating("OnExplosive", 1f, 2f);
            }
        }
    }
    private void OnAttack()
    {
        Instantiate(ammo);
    }
    private void OnExplosive()
    {
        Instantiate(bomb);
    }
}
