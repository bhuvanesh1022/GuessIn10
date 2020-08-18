using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SplashPanel : Base_UIPanel
{
    public Image logo;
    public GameObject skillmatics;
    public GameObject guess;
    public GameObject AuthForm;

    public Button login;
    public Button register;

    public override void OpenBehavior()
    {
        base.OpenBehavior();
        StartCoroutine("Splash");

        login.onClick.RemoveAllListeners();
        login.onClick.AddListener(OnLoginPressed);

        register.onClick.RemoveAllListeners();
        register.onClick.AddListener(OnRegisterPressed);
    }

    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        //Vector3 delta = new Vector3(0.1f, 0.1f, 0.1f);
        //logo.transform.localScale += delta * Time.deltaTime;
    }

    IEnumerator Splash()
    {
        yield return new WaitForSeconds(1.0f);
        skillmatics.SetActive(false);
        Camera.main.backgroundColor = new Color32(255, 176, 32, 0);
        guess.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        guess.SetActive(false);
        Camera.main.backgroundColor = new Color32(0, 170, 255, 0);
        AuthForm.SetActive(true);
    }

    void OnRegisterPressed()
    {
        Base_UIPanel nextPanel = UIManager.instance.registerPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnLoginPressed()
    {
        Base_UIPanel nextPanel = UIManager.instance.loginPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }
}
