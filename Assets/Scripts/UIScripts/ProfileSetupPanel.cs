using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileSetupPanel : Base_UIPanel
{
    public TMP_InputField userNameIF;
    public TMP_InputField DOB_IF;
    public TextMeshProUGUI errMsg;
    public Button createProfile;

    UserInfo userInfo;

    string userNameTxt;
    string ageTxt;

    public override void OpenBehavior()
    {
        base.OpenBehavior();

        userNameIF.onValueChanged.RemoveAllListeners();
        userNameIF.onValueChanged.AddListener(UsernameValue);

        DOB_IF.onValueChanged.RemoveAllListeners();
        DOB_IF.onValueChanged.AddListener(DOB_Value);

        createProfile.onClick.RemoveAllListeners();
        createProfile.onClick.AddListener(CreateProfile);

        StartCoroutine("WaitForUser");
    }

    IEnumerator WaitForUser()
    {
        while (AuthController.authController.m_user == null)
        {
            yield return new WaitForEndOfFrame();
        }

        DatabaseManager.Instance.Fetch(AuthController.authController.m_user.UserId, (result) =>
        {
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
        //Debug.Log(userNameTxt);
    }

    void DOB_Value(string dob)
    {
        int prevLength = 0;

        //print("String:" + dob);
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
        userInfo.UserName = userNameTxt;
        //userInfo.DOB = System.Convert.ToInt32(ageTxt);
        userInfo.DateOfBirth = ageTxt;
        userInfo.SaveObject();
    }
}
