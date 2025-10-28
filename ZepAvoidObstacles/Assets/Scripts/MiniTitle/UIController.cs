using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button start;
    public Button backZep;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        start.onClick.AddListener(StartGame);
        backZep.onClick.AddListener(BackZep);
    }
    
    private void StartGame()
    {
        SceneManager.LoadScene("AvoidObstacles");
    }

    private void BackZep()
    {
        SceneManager.LoadScene("ZepScene");
    }
}
