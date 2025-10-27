using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light levelUpEffect;

    public void Start()
    {
        if(levelUpEffect == null)
        {
            Debug.LogError("Effect is NULL");
            return;
        }
    }
    private void Update()
    {
        if(levelUpEffect.enabled)
        {
            Thread.Sleep(100);
            levelUpEffect.enabled = false;
        }
    }

    public void TurnUp()
    {
        levelUpEffect.enabled = true;

    }
}
