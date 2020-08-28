using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    InputField userNameTxt, ageTxt;

    UserInfo userInfo;
	private void Start()
	{
        DatabaseManager.Instance.Fetch(AuthController.authController.m_user.UserId, (result) =>
        {
            if (result == null)
                userInfo = new UserInfo();
            else
                userInfo = result;

            userNameTxt.text = userInfo.UserName;
            ageTxt.text = userInfo.Age.ToString();
        });
	}

    public void SaveUserInfo()
    {
        userInfo.UserName = userNameTxt.text;
        userInfo.Age = System.Convert.ToInt32(ageTxt.text);
        userInfo.SaveObject();
    }
	public void LogoutUser()
    {
        AuthController.authController.LogoutUser();
    }
}
