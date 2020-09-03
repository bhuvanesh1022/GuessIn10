using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LoginPanel : Base_UIPanel
{
    public TMP_InputField emailIF;
    public TMP_InputField passwordIF;

    public Button revealText;
    public Button forgotPassword;
    public Button forgotPassword2;
    public Button loginBtn;
    public Button registerBtn;
    public Button facebookBtn;
    public Button googleBtn;
    public Button backBtn;
    public Button closeBtn;

    private string emailId;
    private string password;

    public GameObject loginElements;
    public TextMeshProUGUI emptyField;
    public TextMeshProUGUI incorrectPassword;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        Camera.main.backgroundColor = UIManager.instance.login;

        emptyField.gameObject.SetActive(false);
        incorrectPassword.gameObject.SetActive(false);

        emailIF.onValueChanged.RemoveAllListeners();
        emailIF.onValueChanged.AddListener(EmailValue);

        passwordIF.onValueChanged.RemoveAllListeners();
        passwordIF.onValueChanged.AddListener(PasswordValue);

        revealText.onClick.RemoveAllListeners();
        revealText.onClick.AddListener(OnRevealText);

        forgotPassword.onClick.RemoveAllListeners();
        forgotPassword.onClick.AddListener(OnForgotPassword);

        forgotPassword2.onClick.RemoveAllListeners();
        forgotPassword2.onClick.AddListener(OnForgotPassword);

        loginBtn.onClick.RemoveAllListeners();
        loginBtn.onClick.AddListener(() => { OnLoginPressed(); });

        registerBtn.onClick.RemoveAllListeners();
        registerBtn.onClick.AddListener(OnRegisterPressed);

        facebookBtn.onClick.RemoveAllListeners();
        facebookBtn.onClick.AddListener(OnFacebookBtn);

        googleBtn.onClick.RemoveAllListeners();
        googleBtn.onClick.AddListener(OnGoogleBtn);

        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(OnBack);

        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(OncloseBtn);
    }

    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
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
                passwordIF.DeactivateInputField();
                passwordIF.ActivateInputField();
                break;

            case TMP_InputField.ContentType.Standard:
                passwordIF.contentType = TMP_InputField.ContentType.Password;
                passwordIF.DeactivateInputField();
                passwordIF.ActivateInputField();
                break;

            default:
                break;
        }
    }
    void OncloseBtn()
    {
        emailIF.Select();
        emailIF.text = "";
        closeBtn.gameObject.SetActive(false);
    }

    void OnForgotPassword()
    {
        Base_UIPanel nextPanel = UIManager.instance.forgotPasswordPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnLoginPressed()
    {
        if (emailId != null)
        {
            AuthController.authController.LoginWithEmail(emailId, password);

            //Base_UIPanel nextPanel = UIManager.instance.loginPanel;
            //UIManager.instance.TriggerPanelTransition(nextPanel);
        }
        else
        {
            StartCoroutine("MissingMail");
        }
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

    void OnBack()
    {
        Base_UIPanel nextPanel = UIManager.instance.splashPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    public IEnumerator MissingMail()
    {
        loginElements.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, -100, 0), 0.6f);
        emptyField.color = Color.red;
        emptyField.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        emptyField.gameObject.SetActive(false);
        loginElements.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, 100, 0), 0.6f);
   }

   public IEnumerator IncorrectPassword()
   {
        loginElements.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, -100, 0), 0.6f);
        incorrectPassword.gameObject.SetActive(true);
        forgotPassword.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        incorrectPassword.gameObject.SetActive(false);
        forgotPassword.gameObject.SetActive(true);
        loginElements.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, 100, 0), 0.6f);
   }
}
