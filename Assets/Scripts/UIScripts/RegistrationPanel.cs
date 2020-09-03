using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RegistrationPanel : Base_UIPanel
{
    public TMP_InputField userNameIF;
    public InputField emailIF;
    public TMP_InputField passwordIF;

    public Button revealText;
    public Button tNcHl;
    public Button loginBtn;
    public Button registerBtn;
    public Button facebookBtn;
    public Button googleBtn;
    public Button backBtn;
    public Button closeBtn;

    public GameObject loginElements;
    

    public TextMeshProUGUI emptyField;

    private string userName;
    private string emailId;
    private string password;
    private bool VisiblePassword = false;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        Camera.main.backgroundColor = UIManager.instance.register;

        closeBtn.gameObject.SetActive(false);
        emptyField.gameObject.SetActive(false);

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

        loginBtn.onClick.RemoveAllListeners();
        loginBtn.onClick.AddListener(OnLoginPressed);

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

    void UserNameValue(string nm)
    {
        userName = nm;
        PlayerPrefs.SetString("USERNAME", userName);
    }

    void EmailValue(string eml)
    {
        closeBtn.gameObject.SetActive(true);
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

    void OnTermsConditions()
    {   
        Base_UIPanel nextPanel = UIManager.instance.registerPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnLoginPressed()
    {
        Base_UIPanel nextPanel = UIManager.instance.loginPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnRegisterPressed()
    {
        if (emailId != null)
        {
            AuthController.authController.RegisterWithEmail(userName, emailId, password);

            //Base_UIPanel nextPanel = UIManager.instance.loginPanel;
            //UIManager.instance.TriggerPanelTransition(nextPanel);
        }
        else
        {
            StartCoroutine("MissingMail");
        }

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
        emptyField.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);  
        emptyField.gameObject.SetActive(false);
        loginElements.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, 100, 0), 0.6f);
    }
}
