using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CloseToNPC()
    {
        uiManager.ShowText();
    }

    public void ExitToNPC()
    {
        uiManager.HideText();
    }

    public void TalkToNPC()
    {
        uiManager.ShowPanel();
    }
}
