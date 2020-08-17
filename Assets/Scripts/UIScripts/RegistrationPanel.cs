using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegistrationPanel : Base_UIPanel
{
    public TMP_InputField userNameIF;
    public TMP_InputField emailIF;
    public TMP_InputField passwordIF;

    public Button revealText;
    public Button tNcHl;
    public Button ppHl;
    public Button registerBtn;
    public Button facebookBtn;
    public Button googleBtn;

    public TextMeshProUGUI emptyField;

    private string userName;
    private string emailId;
    private string password;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        userNameIF.onValueChanged.RemoveAllListeners();
        userNameIF.onValueChanged.AddListener(UserNameValue);

        emailIF.onValueChanged.RemoveAllListeners();
        emailIF.onValueChanged.AddListener(EmailValue);

        passwordIF.onValueChanged.RemoveAllListeners();
        passwordIF.onValueChanged.AddListener(PasswordValue);

        revealText.onClick.RemoveAllListeners();
        revealText.onClick.AddListener(OnRevealText);

        tNcHl.onClick.RemoveAllListeners();
        tNcHl.onClick.AddListener(OnTermsConditions);

        ppHl.onClick.RemoveAllListeners();
        ppHl.onClick.AddListener(OnPrivacyPolicy);

        registerBtn.onClick.RemoveAllListeners();
        registerBtn.onClick.AddListener(OnRegisterPressed);

        facebookBtn.onClick.RemoveAllListeners();
        facebookBtn.onClick.AddListener(OnFacebookBtn);

        googleBtn.onClick.RemoveAllListeners();
        googleBtn.onClick.AddListener(OnGoogleBtn);
    }

    void UserNameValue(string nm)
    {
        userName = nm;
        PlayerPrefs.SetString("USERNAME", userName);
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

    void OnTermsConditions()
    {
        Base_UIPanel nextPanel = UIManager.instance.registerPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnPrivacyPolicy()
    {
        Base_UIPanel nextPanel = UIManager.instance.forgotPasswordPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnRegisterPressed()
    {
        AuthController.authController.RegisterWithEmail(userName, emailId, password);

        Base_UIPanel nextPanel = UIManager.instance.loginPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);

        //Debug.Log(PlayerPrefs.GetString("USERNAME") +"\n"+ PlayerPrefs.GetString("EMAIL") +"\n"+ PlayerPrefs.GetString("PASSWORD"));
    }

    void OnFacebookBtn()
    {
        Base_UIPanel nextPanel = UIManager.instance.registerPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnGoogleBtn()
    {
        Base_UIPanel nextPanel = UIManager.instance.registerPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }
}
