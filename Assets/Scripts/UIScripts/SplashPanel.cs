using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using DG.Tweening;

public class SplashPanel : Base_UIPanel
{
    public Image logo;
    public VideoPlayer splashVideo;
    public VideoClip splashClip;
    public GameObject guess;
    public Image bulb;
    public GameObject AuthForm;

    public Button login;
    public Button register;

    [SerializeField]
    private Ease _moveEase = Ease.Linear;

    [Range(1.0f, 10.0f), SerializeField]
    private float _colorChangeDurartion = 1.0f;

    [SerializeField]
    private Color bulbOff, bulbOn, loginScreen, registerScreen;

    bool isOpenedAlready;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        if (isOpenedAlready)
            EnableAuthForm();
        else
            StartCoroutine("Splash");
    }

    IEnumerator Splash()
    {
        Camera.main.backgroundColor = new Color32(255, 255, 255, 255);
        splashVideo.targetCamera = Camera.main;
        splashVideo.clip = splashClip;
        splashVideo.Play();
        yield return new WaitForSeconds((float)splashClip.length);

        splashVideo.gameObject.SetActive(false);
        guess.SetActive(true);

        DOTween.Sequence()
            .Append(bulb.DOColor(bulbOff, _colorChangeDurartion).SetEase(_moveEase))
            .Append(bulb.DOColor(bulbOn, _colorChangeDurartion).SetEase(_moveEase))
            .Append(bulb.DOColor(bulbOff, _colorChangeDurartion).SetEase(_moveEase))
            .Append(bulb.DOColor(bulbOn, _colorChangeDurartion).SetEase(_moveEase));

        yield return new WaitForSeconds(4 * _colorChangeDurartion);

        guess.SetActive(false);

        login.onClick.RemoveAllListeners();
        login.onClick.AddListener(OnLoginPressed);
        Debug.Log(login.gameObject.name);

        register.onClick.RemoveAllListeners();
        register.onClick.AddListener(OnRegisterPressed);
        Debug.Log(register.gameObject.name);

        isOpenedAlready = true;
        EnableAuthForm();
    }

    void EnableAuthForm()
    {
        AuthForm.SetActive(true);
    }

    void OnRegisterPressed()
    {
        Debug.Log("Getstarted");
        Base_UIPanel nextPanel = UIManager.instance.registerPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }

    void OnLoginPressed()
    {
        Debug.Log("loginPressed");
        Base_UIPanel nextPanel = UIManager.instance.loginPanel;
        UIManager.instance.TriggerPanelTransition(nextPanel);
    }
}
