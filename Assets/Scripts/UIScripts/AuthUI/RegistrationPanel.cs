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
    public Button termsBtn;

    public GameObject loginElements;
    public GameObject termElements;

    public TextMeshProUGUI emptyField;
    public TextMeshProUGUI emptyTerms;
    public Image tick;

    private string userName;
    private string emailId;
    private string password;
    private bool VisiblePassword = false;
    private bool termsChecked = false;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        Camera.main.backgroundColor = UIManager.instance.register;

        closeBtn.gameObject.SetActive(false);
        emptyField.gameObject.SetActive(false);
        emptyTerms.gameObject.SetActive(false);

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
        closeBtn.onClick.AddListener(OnCloseBtn);

        termsBtn.onClick.RemoveAllListeners();
        termsBtn.onClick.AddListener(OnCheckedTerms);
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

    void OnCloseBtn()
    {
        emailIF.Select();
        emailIF.text = "";
        closeBtn.gameObject.SetActive(false);
    }

    void OnTermsConditions()
    {   
        Base_UIPanel nextPanel = UIManager.instance.termsPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnLoginPressed()
    {
        Debug.Log("clickedL");
        Base_UIPanel nextPanel = UIManager.instance.loginPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnRegisterPressed()
    {
        if (emailId != null)
        {
            if (termsChecked)
            {
                bool hasAt = emailId.IndexOf('@') > 0;
                if (hasAt) AuthController.authController.RegisterWithEmail(emailId, password, ErrorHandler);
                else ErrorHandler("InvalidEmail");
            }
            else
            {
                StartCoroutine("MissingTerms");
            }
        }
        else
        {
            ErrorHandler("MissingEmail");
        }

    }

    void OnFacebookBtn()
    {
        if (termsChecked)
        {
            AuthController.authController.SignInFacebook();
        }
        else
        {
            StartCoroutine("MissingTerms");
        }
    }

    void OnGoogleBtn()
    {
        if (termsChecked)
        {
            AuthController.authController.GoogleSignInDelegate();
        }
        else
        {
            StartCoroutine("MissingTerms");
        }
    }

    void OnBack()
    {
        Base_UIPanel nextPanel = UIManager.instance.splashPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnCheckedTerms()
    {
        if (!termsChecked) termsChecked = true;
        else termsChecked = false;

        tick.enabled = termsChecked;
    }

    public void ErrorHandler(string err)
    {
        string message = string.Empty;

        switch (err)
        {
            case "AccountExistsWithDifferentCredentials":
                StartCoroutine(OnErrorMessage("Account already exists, please try another one."));
                //message = "Ya existe la cuenta con credenciales diferentes";
                break;
            case "MissingPassword":
                StartCoroutine(OnErrorMessage("Password is missing, please enter the password."));
                //message = "Hace falta el Password";
                break;
            case "WeakPassword":
                StartCoroutine(OnErrorMessage("Password is too weak, please try again."));
                //message = "El password es debil";
                break;
            case "EmailAlreadyInUse":
                StartCoroutine(OnErrorMessage("Email Id already in use, please try another one."));
                //message = "Ya existe la cuenta con ese correo electrónico";
                break;
            case "InvalidEmail":
                StartCoroutine(OnErrorMessage("Sorry! It doesn't seem like this email id is valid." + "\n" + "Please try another one."));
                //message = "Correo electronico invalido";
                break;
            case "MissingEmail":
                StartCoroutine(OnErrorMessage("Please enter your parent's email and password to start playing."));
                //message = "Hace falta el correo electrónico";
                break;
            default:
                message = "Ocurrió un error";
                StartCoroutine(OnErrorMessage(err + ". Please try again."));
                break;
        }

        Debug.Log(message);
    }

    IEnumerator OnErrorMessage(string errMsg)
    {
        loginElements.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, -100, 0), 0.6f);
        emptyField.color = Color.red;
        emptyField.text = errMsg;
        emptyField.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        emptyField.gameObject.SetActive(false);
        loginElements.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, 100, 0), 0.6f);
    }

    public IEnumerator MissingTerms()
    {
        termElements.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, 75, 0), 0.3f);
        emptyTerms.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        emptyTerms.gameObject.SetActive(false);
        termElements.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, -75, 0), 0.3f);
    }
}
