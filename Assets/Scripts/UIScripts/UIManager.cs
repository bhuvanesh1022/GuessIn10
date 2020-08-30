using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public Transform _auth, _splash;
    public GameObject _splashPanel, _loginPanel, _registerPanel, _forgotPasswordPanel, _profileSetupPanel;

    [HideInInspector]
    public Base_UIPanel splashPanel, loginPanel, registerPanel, forgotPasswordPanel, profileSetupPanel;

    public Color register, login;

    Base_UIPanel _currentPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        splashPanel = Instantiate(_splashPanel, _splash).GetComponent<Base_UIPanel>();
        loginPanel = Instantiate(_loginPanel, _auth).GetComponent<Base_UIPanel>();
        registerPanel = Instantiate(_registerPanel, _auth).GetComponent<Base_UIPanel>();
        forgotPasswordPanel = Instantiate(_forgotPasswordPanel, _auth).GetComponent<Base_UIPanel>();
        profileSetupPanel = Instantiate(_profileSetupPanel, _auth).GetComponent<Base_UIPanel>();
    }

    private void Start()
    {
        TriggerOpenPanel(splashPanel);
    }

    private void Update()
    {
        if (_currentPanel) _currentPanel.UpdateBehavior();

        if (AuthController.authController.currentPanel != _currentPanel)
        {
            AuthController.authController.currentPanel = _currentPanel.gameObject;
        }
    }

    public void TriggerPanelTransition(Base_UIPanel panel)
    {
        TriggerOpenPanel(panel);
    }

    void TriggerOpenPanel(Base_UIPanel panel)
    {
        if (_currentPanel != null) TriggerClosePanel(_currentPanel);
        _currentPanel = panel;
        _currentPanel.OpenBehavior();

    }

    void TriggerClosePanel(Base_UIPanel panel)
    {
        panel.CloseBehavior();
    }

}