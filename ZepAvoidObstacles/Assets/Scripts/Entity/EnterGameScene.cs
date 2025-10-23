using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGameScene : MonoBehaviour
{
    public void EnterGame()
    {
        SceneManager.LoadScene("AvoidObstacle");
    }
}
