using UnityEngine;

public class Test : MonoBehaviour
{
    public void LogoutUser()
    {
        AuthController.authController.LogoutUser();
    }
}
