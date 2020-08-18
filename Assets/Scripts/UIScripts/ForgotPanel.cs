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

    private string emailId;

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
        emailId = eml;
    }

    void OnSendLink()
    {
        StartCoroutine("OnLinkSent", emailId);
    }

    IEnumerator OnLinkSent(string email)
    {
        AuthController.authController.SendPasswordResetMail(email);
        sentMsg.enabled = true;
        sendLink.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        Base_UIPanel nextPanel = UIManager.instance.loginPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }
}
