using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI canTalkText;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

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

        yesButton.onClick.AddListener(YesButtonOnClick);
        noButton.onClick.AddListener(NoButtonOnClick);
    }

    void YesButtonOnClick()
    {
        EnterGameScene.Instance.EnterGame();
    }
    void NoButtonOnClick()
    {
        ClosePanel();
    }

    public void ShowText()
    {
        canTalkText.gameObject.SetActive(true);
    }
    public void HideText()
    {
        canTalkText.gameObject.SetActive(false);
    }

    public void ShowPanel()
    {
        panel.gameObject.SetActive(true);
    }
    void ClosePanel()
    {
        panel.gameObject.SetActive(false);
    }
}
