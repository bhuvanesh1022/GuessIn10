using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginPanel : Base_UIPanel
{
    public TMP_InputField emailIF;
    public TMP_InputField passwordIF;

    public Button revealText;
    public Button forgotPassword;
    public Button loginBtn;
    public Button registerBtn;
    public Button facebookBtn;
    public Button googleBtn;

    private string emailId;
    private string password;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        emailIF.onValueChanged.RemoveAllListeners();
        emailIF.onValueChanged.AddListener(EmailValue);

        passwordIF.onValueChanged.RemoveAllListeners();
        passwordIF.onValueChanged.AddListener(PasswordValue);

        revealText.onClick.RemoveAllListeners();
        revealText.onClick.AddListener(OnRevealText);

        forgotPassword.onClick.RemoveAllListeners();
        forgotPassword.onClick.AddListener(OnForgotPassword);

        loginBtn.onClick.RemoveAllListeners();
        loginBtn.onClick.AddListener(() => { OnLoginPressed(); });

        registerBtn.onClick.RemoveAllListeners();
        registerBtn.onClick.AddListener(OnRegisterPressed);

        facebookBtn.onClick.RemoveAllListeners();
        facebookBtn.onClick.AddListener(OnFacebookBtn);

        googleBtn.onClick.RemoveAllListeners();
        googleBtn.onClick.AddListener(OnGoogleBtn);
    }

    void EmailValue(string eml)
    {
        emailId = eml;
        PlayerPrefs.SetString("EMAIL", emailId);
    }

    void PasswordValue(string pwrd)
    {
        password = pwrd;
        PlayerPrefs.SetString("PASSWORD", password);
    }

    void OnRevealText()
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

    void OnForgotPassword()
    {
        Base_UIPanel nextPanel = UIManager.instance.forgotPasswordPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnLoginPressed()
    {
        AuthController.authController.LoginWithEmail(emailId, password);
    }

    void OnRegisterPressed()
    {
        Base_UIPanel nextPanel = UIManager.instance.registerPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnFacebookBtn()
    {
        AuthController.authController.SignInFacebook();
    }

    void OnGoogleBtn()
    {
        AuthController.authController.GoogleSignInDelegate();
    }
}
