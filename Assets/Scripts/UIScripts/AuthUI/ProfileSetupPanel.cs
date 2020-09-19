using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ProfileSetupPanel : Base_UIPanel
{
    public TMP_InputField userNameIF;
    public TMP_InputField DOB_IF;
    public TextMeshProUGUI errMsg;
    public Button createProfile;
    public Color cameraBG;
    public List<Sprite> profilePix = new List<Sprite>();
    public List<Image> dp = new List<Image>();
    public float swipeThreshold = 50f;
    public float timeThreshold = 0.3f;

    UserInfo userInfo;

    string userNameTxt;
    string ageTxt;
    int currentDpId;

    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        Camera.main.backgroundColor = cameraBG;

        userNameIF.onValueChanged.RemoveAllListeners();
        userNameIF.onValueChanged.AddListener(UsernameValue);

        DOB_IF.onValueChanged.RemoveAllListeners();
        DOB_IF.onValueChanged.AddListener(DOB_Value);

        createProfile.onClick.RemoveAllListeners();
        createProfile.onClick.AddListener(CreateProfile);

        currentDpId = 1;

        dp[0].sprite = profilePix[currentDpId - 1];
        dp[1].sprite = profilePix[currentDpId];
        dp[2].sprite = profilePix[currentDpId + 1];

        StartCoroutine("WaitForUser");
    }

    public void Update()
    {
        base.UpdateBehavior();

        if (Input.GetMouseButtonDown(0))
        {
            this.fingerDown = Input.mousePosition;
            this.fingerUp = Input.mousePosition;
            this.fingerDownTime = DateTime.Now;
        }
        if (Input.GetMouseButtonUp(0))
        {
            this.fingerDown = Input.mousePosition;
            this.fingerUpTime = DateTime.Now;
            this.CheckSwipe();
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                this.fingerDown = touch.position;
                this.fingerUp = touch.position;
                this.fingerDownTime = DateTime.Now;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                this.fingerDown = touch.position;
                this.fingerUpTime = DateTime.Now;
                this.CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        float duration = (float)this.fingerUpTime.Subtract(this.fingerDownTime).TotalSeconds;
        if (duration > this.timeThreshold) return;

        float deltaX = this.fingerDown.x - this.fingerUp.x;
        if (Mathf.Abs(deltaX) > this.swipeThreshold)
        {
            if (deltaX > 0)
            {
                OnSwipeRight();
            }
            else if (deltaX < 0)
            {
                OnSwipeLeft();
            }
        }

        this.fingerUp = this.fingerDown;
    }

    IEnumerator WaitForUser()
    {
        while (AuthController.authController.m_user == null)
        {
            yield return new WaitForEndOfFrame();
        }

        DatabaseManager.Instance.Fetch(AuthController.authController.m_user.UserId, (result) =>
        {
            //if (result != null)
            //userInfo = result;

            if (result == null)
                userInfo = new UserInfo();
            else
                userInfo = result;

            userNameTxt = userInfo.UserName;
            ageTxt = userInfo.DateOfBirth;
        });
    }

    void UsernameValue(string _name)
    {
        userNameTxt = _name;
    }

    public void OnSwipeLeft()
    {
        if (currentDpId < profilePix.Count - 1)
        {
            currentDpId += 1;
            ChangeDp();
        }
    }

    public void OnSwipeRight()
    {
        if (currentDpId > 0)
        {
            currentDpId -= 1;
            ChangeDp();
        }

    }

    void ChangeDp()
    {
        if (currentDpId == 0)
        {
            dp[0].gameObject.SetActive(false);
            dp[1].sprite = profilePix[currentDpId];
            dp[2].sprite = profilePix[currentDpId + 1];
        }
        else if (currentDpId == profilePix.Count - 1)
        {
            dp[2].gameObject.SetActive(false);
            dp[0].sprite = profilePix[currentDpId - 1];
            dp[1].sprite = profilePix[currentDpId];
        }
        else
        {
            dp[0].gameObject.SetActive(true);
            dp[2].gameObject.SetActive(true);
            dp[0].sprite = profilePix[currentDpId - 1];
            dp[1].sprite = profilePix[currentDpId];
            dp[2].sprite = profilePix[currentDpId + 1];
        }
    }

    void DOB_Value(string dob)
    {
        int prevLength = 0;

        if (dob.Length > 0)
        {
            DOB_IF.onValueChanged.RemoveAllListeners();
            if (!char.IsDigit(dob[dob.Length - 1]) && dob[dob.Length - 1] != '/')
            { // Remove Letters
                DOB_IF.text = dob.Remove(dob.Length - 1);
                DOB_IF.caretPosition = DOB_IF.text.Length;
            }
            else if (dob.Length == 2 || dob.Length == 5)
            {
                if (dob.Length < prevLength)
                { // Delete
                    DOB_IF.text = dob.Remove(dob.Length - 1);
                    DOB_IF.caretPosition = DOB_IF.text.Length;
                }
                else
                { // Add
                    DOB_IF.text = dob + "/";
                    DOB_IF.caretPosition = DOB_IF.text.Length;
                }
            }
            DOB_IF.onValueChanged.AddListener(DOB_Value);
        }
        prevLength = DOB_IF.text.Length;
        ageTxt = dob;
        //Debug.Log(ageTxt);
    }

    void CreateProfile()
    {
        if (userNameTxt != null && ageTxt != null)
        {
            userInfo.UserName = userNameTxt;
            userInfo.DP_ID = Convert.ToInt32(currentDpId);
            userInfo.DateOfBirth = ageTxt;
            userInfo.SaveObject();
            AuthController.authController.LoadHome(1);
        }
        else
        {
            StartCoroutine("MissingName");
        }

    }

    public IEnumerator MissingName()
    {
        createProfile.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, -100, 0), 0.6f);
        errMsg.color = Color.red;
        errMsg.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        errMsg.gameObject.SetActive(false);
        createProfile.GetComponent<RectTransform>().DOBlendableLocalMoveBy(new Vector3(0, 100, 0), 0.6f);
    }
}
