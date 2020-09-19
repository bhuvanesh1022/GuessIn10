using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

#if UNITY_EDITOR
    using Firebase.Unity.Editor;
#endif

public class DatabaseManager : MonoBehaviour
{

    static DatabaseManager m_instance;

    public string UserName;
    public string DateOfBirth;
    public int ProfilePic;
    public List<Sprite> profilePix = new List<Sprite>();
    public Image profilePic;

    Action<UserInfo> fetchCallback;
    DatabaseReference reference;
    void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://guessin10-15082020.firebaseio.com/");
#endif

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public static DatabaseManager Instance
    {
        get {
            return m_instance;
        }
    }

    public void Fetch(string firebaseUserId, Action<UserInfo> cb)
    {
        fetchCallback = cb;
        FetchUserData(firebaseUserId);
    }

    private void FetchUserData(string firebaseUserId)
    {
        //Debug.Log("FetchUserData " + firebaseUserId);
        FirebaseDatabase.DefaultInstance.GetReference("Users/" + firebaseUserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                fetchCallback(null);
            }
            else if (task.IsCompleted)
            {

                DataSnapshot snapshot = task.Result;
                if (snapshot != null && snapshot.Value != null)
                {
                    string jsonString = snapshot.GetRawJsonValue();
                    UserInfo info = JsonUtility.FromJson<UserInfo>(jsonString);
                    fetchCallback(info);
                    Debug.Log("UserInfo: " + jsonString);
                }
                else
                {
                    fetchCallback(null);
                    Debug.Log("UserInfo not found");
                }
            }
        });
    }

    public void PushToServer(string firebaseUserID, UserInfo info)
    {
        if (string.IsNullOrEmpty(firebaseUserID) || info == null)
        {
            return;
        }

        string jsonData = JsonUtility.ToJson(info);
        reference.Child("Users").Child(firebaseUserID).SetRawJsonValueAsync(jsonData).ContinueWithOnMainThread(task =>
        {

            if (task.IsCompleted)
            {
                Debug.Log("PushedData Success: " + jsonData);
            }
            else
            {
                Debug.Log("DatabaseManager: Couldn't Complete dataSync UserData, userID: " + firebaseUserID);
            }

        });
    }
}
