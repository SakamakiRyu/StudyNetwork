using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���[�U�[���쐬����N���X
/// </summary>
public class CreateUser : MonoBehaviour
{
    private readonly string URL = "http://localhost/php/CreateUser.php";

    [SerializeField]
    private InputField _userIDField;

    [SerializeField]
    private InputField _passField;

    public void RequestCreateUser()
    {
        var user = CreateUserDate(_userIDField.text, _passField.text);
        NetworkManager.Instance.PostRequest(URL, user);
    }

    /// <summary>
    /// ���[�U�[�f�[�^�̍쐬
    /// </summary>
    private User CreateUserDate(string id, string pass)
    {
        var user = new User { userID = id ,password = pass};
        return user;
    }
}