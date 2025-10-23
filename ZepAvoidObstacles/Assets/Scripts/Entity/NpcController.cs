using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public bool canTalk { get; set; }
    public bool isTalk { get; set; }

    public UIManager Ui;
    
    // Start is called before the first frame update
    void Start()
    {
        if(Ui == null)
        {
            Debug.LogError("UIManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        SendIsTalkableStatus(canTalk);
        SendIsTalk(isTalk);
    }

    void SendIsTalkableStatus(bool isTalkable)
    {
        Ui.ShowText(isTalkable);
    }
    
    void SendIsTalk(bool isTalking)
    {
        Ui.ShowPanel(isTalking);
    }
}
