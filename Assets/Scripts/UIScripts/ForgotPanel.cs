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
    public Button loginBtn;
    public Button backBtn;

    private string emailId;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        sentMsg.enabled = false;

        emailIF.onValueChanged.RemoveAllListeners();
        emailIF.onValueChanged.AddListener(EmailValue);

        sendLink.onClick.RemoveAllListeners();
        sendLink.onClick.AddListener(OnSendLink);

        loginBtn.onClick.RemoveAllListeners();
        loginBtn.onClick.AddListener(OnLoginClicked);

        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(OnBack);

        loginBtn.gameObject.SetActive(false);
        sendLink.gameObject.SetActive(true);
        emailIF.gameObject.SetActive(true);
    }

    void EmailValue(string eml)
    {
        emailId = eml;
    }

    void OnSendLink()
    {
        AuthController.authController.SendPasswordResetMail(emailId);
        sentMsg.enabled = true;
        sendLink.gameObject.SetActive(false);
        emailIF.gameObject.SetActive(false);
        loginBtn.gameObject.SetActive(true);
    }

    void OnLoginClicked()
    {
        Base_UIPanel nextPanel = UIManager.instance.loginPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnBack()
    {
        Base_UIPanel nextPanel = UIManager.instance.splashPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }
}
