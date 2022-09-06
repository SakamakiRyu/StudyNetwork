using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class HashGenerator : MonoBehaviour
{
    private void Start()
    {
        var json = JsonUtility.ToJson("aiuえお");
        GenerateHash256(json);
    }

    public static string GenerateHash256(string str)
    {
        var sha256 = new SHA256CryptoServiceProvider();
        var ar1 = Encoding.UTF8.GetBytes(str);
        var ar2 = sha256.ComputeHash(ar1);
        sha256.Clear();

        var sb = new StringBuilder();
        foreach (var item in ar2)
        {
            sb.Append(item.ToString());
        }
        return sb.ToString();
    }
}