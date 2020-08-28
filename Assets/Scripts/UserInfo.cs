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
    int age;

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

    public int Age
    {
        get {
            return age;
        }
        set {
            age = value;
        }
    }


    public void SaveObject()
    {
        DatabaseManager.Instance.PushToServer(AuthController.authController.m_user.UserId, this);
    }

}
