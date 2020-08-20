using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Storage;
using System.IO;
using UnityEngine.UI;

public class DownloadManager : MonoBehaviour
{
    [SerializeField]
    Text status;
    string storageBaseURL = "gs://guessin10-15082020.appspot.com";

    Firebase.Storage.FirebaseStorage storage;
    Firebase.Storage.StorageReference assetReferance;

    string m_status;
    string path;
	void Awake()
	{
        storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        path = Application.persistentDataPath;
        Debug.Log(path);
	}
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        assetReferance = storage.GetReferenceFromUrl(storageBaseURL + "/AssetBundles/Android/StandardPackages/Animals");
#endif
#if UNITY_IOS
        assetReferance = storage.GetReferenceFromUrl(storageBaseURL + "/AssetBundles/iOS/StandardPackages/Animals");
#endif

        StartCoroutine(UpdateUI());
    }

    // Update is called once per frame
    void Update()
    {
        //animals
    }

    IEnumerator UpdateUI()
    {
        while(gameObject.activeSelf)
        {
            status.text = m_status;
            yield return null;
        }
    }

    public void DownloadURL(string fileName)
    {
        m_status = "DownloadURL";
        assetReferance.Child(fileName).GetDownloadUrlAsync().ContinueWith((System.Threading.Tasks.Task<System.Uri> task) => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                m_status = "Download URL: " + task.Result.AbsoluteUri;
                // ... now download the file via WWW or UnityWebRequest.
            }
            else
                m_status = "Error" + task.ToString();
        });
    }

    public void DownloadFile(string fileName)
    {
        m_status = "DownloadFile";
        const long maxAllowedSize = 1 * 1024 * 1024;
        assetReferance.Child(fileName).GetBytesAsync(maxAllowedSize).ContinueWith((System.Threading.Tasks.Task<byte[]> task) => {
            m_status = "callback " + task.Status;
            if (task.IsFaulted || task.IsCanceled)
            {
                m_status = "Error" + task.ToString();
                // Uh-oh, an error occurred!
            }
            else
            {
                byte[] fileContents = task.Result;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(Application.persistentDataPath);
                }
                string downloadPath = string.Format("{0}/{1}", path, fileName);
                System.IO.File.WriteAllBytes(downloadPath, fileContents);
                Debug.Log(downloadPath);
                m_status = "Finished downloading! and save at " + downloadPath;
            }
        });
    }
}
