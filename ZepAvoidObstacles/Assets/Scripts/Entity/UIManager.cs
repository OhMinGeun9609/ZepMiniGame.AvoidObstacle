using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI canTalkText;
    [SerializeField] private GameObject panel;
    public Button yesButton;
    public Button noButton;

    // Start is called before the first frame update
    void Start()
    {
        if(canTalkText == null)
        {
            Debug.LogError("TalkText is null");
        }

        if(panel == null)
        {
            Debug.LogError("Panel is Null");
        }

        canTalkText.gameObject.SetActive(false);
        panel.SetActive(false);
        
    }

    public void ShowText(bool isActive)
    {
        if (isActive)
            canTalkText.gameObject.SetActive(true);
        else
            canTalkText.gameObject.SetActive(false);
    }

    public void ShowPanel(bool isActive)
    {
        if(isActive)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }    
}
