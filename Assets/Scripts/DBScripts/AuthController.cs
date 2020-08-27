using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Google;
using Firebase;
using Firebase.Auth;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using System;

public class AuthController : MonoBehaviour
{
    public static AuthController authController;

    [SerializeField]
    string webClientId;

    GoogleSignInConfiguration m_configuration;
    FirebaseAuth m_auth;
    public FirebaseUser m_user;

    public GameObject currentPanel;

    /*
     * To Make sure AuthController is a single instance   
     * To Make sure WebCLientID for Google was provided
     * To Initiate Facebook
     */

    // Defer the configuration creation until Awake so the web Client ID
    // Can be set via the property inspector in the Editor.

    void Awake()
    {
        if (AuthController.authController == null)
        {
            AuthController.authController = this;
        }
        else
        {
            if (AuthController.authController != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        if (webClientId != string.Empty)
        {
            m_configuration = new GoogleSignInConfiguration
            {
                WebClientId = webClientId,
                RequestIdToken = true
            };
        }
        else
            Debug.Log("webClientId not provided");

        FB.Init(InitCallBack, OnHideUnity);
    }

    /* 
     * To Instantiatinng Firebase auth and user.
     * Starts A Coroutine to Check whether a User have Logged In
     */

    void Start()
    {
        m_auth = FirebaseAuth.DefaultInstance;
        m_user = FirebaseAuth.DefaultInstance.CurrentUser;

        StartCoroutine(UpdateUI());
    }

    /*
     * To Check whether a User have Logged In and to Start A Coroutine to Load Home Page
     */

    IEnumerator UpdateUI()
    {
        yield return new WaitForSeconds(3.0f);
        while (m_user == null)
        {
            Debug.Log("wait");
            yield return new WaitForEndOfFrame();
        }

        Debug.Log(string.Format("Welcome {0} \nYour Firebase ID {1}", m_user.DisplayName, m_user.UserId));
        LoadHome(1);
    }

    /*
     * To Load Home Page
     */

    public void LoadHome(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    #region LOGIN_W/EMAIL

    /*
     * Use Email Id and Password to Login Firebase
     */

    public void LoginWithEmail(string email, string password)
    {
        m_auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPassword was canceled.");
                return;
            }
            else if (task.IsFaulted)
            {
                FirebaseException exception = task.Exception.InnerExceptions[0].InnerException as FirebaseException;
                var errCode = (AuthError)exception.ErrorCode;
                GetErrorMessage(errCode.ToString());
                return;
            }
            else if (task.IsCompleted)
            {
                m_user = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", m_user.DisplayName, m_user.UserId);
            }
        });
    }

    #endregion

    #region REGISTER_W/EMAIL

    /*
     * Use Email Id and Password to Register a User in Firebase
     */

    public void RegisterWithEmail(string _username, string email, string password)
    {
        m_auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                if (Debug.isDebugBuild)
                    Debug.LogError("SignInWithEmailAndPassword was canceled.");
                return;
            }
            else if (task.IsFaulted)
            {
                FirebaseException exception = task.Exception.InnerExceptions[0].InnerException as FirebaseException;
                var errCode = (AuthError)exception.ErrorCode;
                GetErrorMessage(errCode.ToString());
                //Debug.LogError("SignInWithEmailAndPassword encountered an error: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                m_user = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", m_user.DisplayName, m_user.UserId);
            }
        });
    }

    #endregion

    public void SendPasswordResetMail(string email)
    {
        Debug.Log("sent");

        m_auth.SendPasswordResetEmailAsync(email).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                FirebaseException exception = task.Exception.InnerExceptions[0].InnerException as FirebaseException;
                var errCode = (AuthError)exception.ErrorCode;
                GetErrorMessage(errCode.ToString());
                return;
            }
            Debug.Log("Password reset email sent successfully.");
        });
    }

    #region SIGNIN_W/FACEBOOK

    /*
     * Initiating Callback to Activate Facebook App
     */

    private void InitCallBack()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.Log("Firebase and Facebook Initialized");
        }
        else
        {
            Debug.LogWarning("Failed to Initializw Facebook SDK");
        }
    }

    /*
     * Called When App minimized and Restored
     */

    private void OnHideUnity(bool isUnityShown)
    {
        if (isUnityShown) FB.ActivateApp();
    }

    /*
     * Login to Facebook with permissions 
     */

    public void SignInFacebook()
    {
        var perms = new List<string> { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, OnFacebookLoginResult);
    }

    /*
     * Retrieves an Access Token from Facebook
     */

    private void OnFacebookLoginResult(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var accessToken = AccessToken.CurrentAccessToken;
            SignInFirebase(accessToken);
        }
    }

    /*
     * Signin into Firebase using Facebook Access Token
     */

    private void SignInFirebase(AccessToken accessToken)
    {
        var credential = FacebookAuthProvider.GetCredential(accessToken.TokenString);
        m_auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Signin cancelled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Signin error: " + task.Exception);
                return;
            }

            m_user = task.Result;
            Debug.Log($"User signed in: {m_user.DisplayName}, {m_user.UserId}.");
        });
    }

    #endregion

    #region SIGNIN W/GOOLE

    /*
     * Login to Google for Auth Credentials
     */

    public void GoogleSignInDelegate()
    {
        Debug.Log("Calling SignIn");
        GoogleSignIn.Configuration = m_configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnAuthenticationFinished);
    }

    /*
     * Signin into Firebase using Google Credential
     */

    void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        Debug.Log("OnAuthenticationFinished1 " + task.Status);
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;

                    Debug.Log("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.Log("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Canceled");
        }
        else
        {
            Debug.Log("Welcome: " + task.Result.DisplayName + "!");
            Debug.Log("Toekn: " + task.Result.IdToken);
            Debug.Log("AuthCode: " + task.Result.AuthCode);
            Debug.Log("UserId: " + task.Result.UserId);


            Credential credential = GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
            m_auth.SignInWithCredentialAsync(credential).ContinueWith(t => {
                if (t.IsCanceled)
                {
                    if (Debug.isDebugBuild)
                        Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (t.IsFaulted)
                {
                    if (Debug.isDebugBuild)
                        Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                m_user = t.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", m_user.DisplayName, m_user.UserId);
            });
        }
    }

    #endregion

    #region FIREBASE ERROR HANDLER

    /*
     * This Method calls when Login Faulted with Firebase
     */

    private void GetErrorMessage(string errorCode)
    {
        Debug.Log(errorCode);
        var message = "";
        switch (errorCode)
        {
            case "AccountExistsWithDifferentCredentials":
                message = "Ya existe la cuenta con credenciales diferentes";
                break;
            case "MissingPassword":
                message = "Hace falta el Password";
                break;
            case "WeakPassword":
                message = "El password es debil";
                break;
            case "WrongPassword":
                message = "El password es Incorrecto";
                currentPanel.GetComponent<LoginPanel>().StartCoroutine("IncorrectPassword");
                break;
            case "EmailAlreadyInUse":
                message = "Ya existe la cuenta con ese correo electrónico";
                break;
            case "InvalidEmail":
                message = "Correo electronico invalido";
                break;
            case "MissingEmail":
                message = "Hace falta el correo electrónico";
                break;
            default:
                message = "Ocurrió un error";
                break;
        }
        Debug.Log(message);
    }

    #endregion

    #region LOGOUT

    /*
     * Logout User from Firebase
     */

    public void LogoutUser()
    {
        m_auth.SignOut();
        LoadHome(0);
        Debug.Log("succesfully logged out");
    }

    #endregion
}
