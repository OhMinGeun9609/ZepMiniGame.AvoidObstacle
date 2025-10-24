using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGameScene : MonoBehaviour
{
    public static EnterGameScene _instance;
    
    public static EnterGameScene Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new EnterGameScene();
            }

            return _instance;
        }
    }

    public void EnterGame()
    {
        SceneManager.LoadScene("AvoidObstacle");
    }
}
