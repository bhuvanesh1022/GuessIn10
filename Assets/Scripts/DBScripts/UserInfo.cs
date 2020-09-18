using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UserInfo
{
    [SerializeField]
    string userName;
    [SerializeField]
    string DOB;
    [SerializeField]
    int DpId;

    public UserInfo()
    {
        
    }

    public string UserName
    {
        get {
            return userName;
        }
        set {
            userName = value;
        }
    }

    public string DateOfBirth
    {
        get {
            return DOB;
        }
        set {
            DOB = value;
        }
    }

    public int DP_ID
    {
        get
        {
            return DpId;
        }
        set
        {
            DpId = value;
        }
    }

    public void SaveObject()
    {
        DatabaseManager.Instance.PushToServer(AuthController.authController.m_user.UserId, this);
    }

}
