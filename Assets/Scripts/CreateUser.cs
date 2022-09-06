using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CreateUser : MonoBehaviour
{
    private readonly string URL = "http://localhost/php/CreateUser.php";

    [SerializeField]
    private InputField _idField;

    [SerializeField]
    private InputField _passField;

    public void Execute()
    {
        User user = new User { user_id = _idField.text, password = _passField.text };
        NetworkManager.Instance.PostRequest(URL, user);
    }
}
