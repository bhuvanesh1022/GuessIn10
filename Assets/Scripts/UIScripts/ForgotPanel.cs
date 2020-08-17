using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForgotPanel : Base_UIPanel
{
    public TMP_InputField emailIF;
    public TextMeshProUGUI sentMsg;
    public Button sendLink;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        emailIF.onValueChanged.RemoveAllListeners();
        emailIF.onValueChanged.AddListener(EmailValue);

        sendLink.onClick.RemoveAllListeners();
        sendLink.onClick.AddListener(OnSendLink);
    }

    void EmailValue(string eml)
    {
        //AuthController.authController.emailId = eml;
        //PlayerPrefs.SetString("EMAIL", AuthController.authController.emailId);
    }

    void OnSendLink()
    {
        Base_UIPanel nextPanel = UIManager.instance.resetPasswordPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }
}
