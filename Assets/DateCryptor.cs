using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Networking;

/// <summary>
/// データの暗号化クラス
/// </summary>
public class DateCryptor : MonoBehaviour
{
    [SerializeField]
    string date = "あいうえお";

    string URL = "http://localhost/php/CreateUser.php";

    const string AedIV = "1234567890123456";
    const string AedKey = "1234567890123456";

    /// <summary>
    /// 暗号化
    /// </summary>
    private static byte[] Encrypt(byte[] src)
    {
        RijndaelManaged rijnMana = new RijndaelManaged();
        rijnMana.KeySize = 128;
        rijnMana.BlockSize = 128;
        rijnMana.Key = Encoding.UTF8.GetBytes(AedKey);
        rijnMana.IV = Encoding.UTF8.GetBytes(AedIV);
        rijnMana.Mode = CipherMode.ECB;

        ICryptoTransform encryptor = rijnMana.CreateEncryptor();
        var encrypted = encryptor.TransformFinalBlock(src, 0, src.Length);
        encryptor.Dispose();
        return encrypted;
    }

    /// <summary>
    /// 複合化
    /// </summary>
    private string Decrypt(byte[] src)
    {
        RijndaelManaged rijnMana = new RijndaelManaged();
        rijnMana.KeySize = 128;
        rijnMana.BlockSize = 128;
        rijnMana.Key = Encoding.UTF8.GetBytes(AedKey);
        rijnMana.IV = Encoding.UTF8.GetBytes(AedIV);
        rijnMana.Mode = CipherMode.ECB;

        ICryptoTransform decryptor = rijnMana.CreateDecryptor(rijnMana.Key, rijnMana.IV);
        var plain = decryptor.TransformFinalBlock(src, 0, src.Length);
        decryptor.Dispose();
        var str = Encoding.UTF8.GetString(plain);
        return str;
    }

    /// <summary>
    /// 暗号化したデータをサーバーに送信する
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    IEnumerator PostAsync(byte[] src)
    {
        var json = JsonUtility.ToJson(src);
        WWWForm form = new WWWForm();
        form.AddField("src", json);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
        if (www.isDone)
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
