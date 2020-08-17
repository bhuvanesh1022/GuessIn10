using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResetPanel : Base_UIPanel
{
    public TMP_InputField passwordIF;
    public Button revealText1;
    public TMP_InputField confirmPasswordIF;
    public Button revealText2;
    public TextMeshProUGUI errMsg;
    public Button resetPassword;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        passwordIF.onValueChanged.RemoveAllListeners();
        passwordIF.onValueChanged.AddListener(PasswordValue);

        revealText1.onClick.RemoveAllListeners();
        revealText1.onClick.AddListener(OnRevealText1);

        confirmPasswordIF.onValueChanged.RemoveAllListeners();
        confirmPasswordIF.onValueChanged.AddListener(ConfirmPasswordValue);

        revealText2.onClick.RemoveAllListeners();
        revealText2.onClick.AddListener(OnRevealText2);

        resetPassword.onClick.RemoveAllListeners();
        resetPassword.onClick.AddListener(OnSendLink);
    }

    void PasswordValue(string pwd1)
    {
        //AuthController.authController.emailId = pwd1;
        //PlayerPrefs.SetString("EMAIL", AuthController.authController.emailId);
    }

    void ConfirmPasswordValue(string pwd2)
    {
        //AuthController.authController.emailId = pwd2;
        //PlayerPrefs.SetString("EMAIL", AuthController.authController.emailId);
    }

    void OnRevealText1()
    {
        switch (passwordIF.contentType)
        {
            case TMP_InputField.ContentType.Password:
                passwordIF.contentType = TMP_InputField.ContentType.Standard;
                break;

            case TMP_InputField.ContentType.Standard:
                passwordIF.contentType = TMP_InputField.ContentType.Password;
                break;

            default:
                break;
        }
    }

    void OnRevealText2()
    {
        switch (confirmPasswordIF.contentType)
        {
            case TMP_InputField.ContentType.Password:
                confirmPasswordIF.contentType = TMP_InputField.ContentType.Standard;
                break;

            case TMP_InputField.ContentType.Standard:
                confirmPasswordIF.contentType = TMP_InputField.ContentType.Password;
                break;

            default:
                break;
        }
    }

    void OnSendLink()
    {
        Base_UIPanel nextPanel = UIManager.instance.loginPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }
}
