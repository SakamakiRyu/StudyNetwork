using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public static NetworkManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #region èCê≥
    public void GetRequest<T>(string url)
    {
        GetRequestAsync<T>(url);
    }

    private IEnumerator GetRequestAsync<T>(string url)
    {
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        var result = www.downloadHandler.text;
        T model = JsonUtility.FromJson<T>(result);
    }

    public void PostRequest<T>(string url, T postDate)
    {
        var json = JsonUtility.ToJson(postDate);
        WWWForm form = new WWWForm();
        form.AddField("json", json);
        StartCoroutine(PostRequestAsycn<T>(url, form));
    }

    private IEnumerator PostRequestAsycn<T>(string url, WWWForm formDate)
    {
        UnityWebRequest www = UnityWebRequest.Post(url, formDate);
        yield return www.SendWebRequest();

        if (www.isDone)
        {
            var test = GameObject.Find("Show").GetComponent<ShowText>();
            test.ShowMessage(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
            www.Dispose();
        }
    }
    #endregion

    public IEnumerator Request(string uri, string date)
    {
        UnityWebRequest req = null;
        var postDate = System.Text.Encoding.UTF8.GetBytes(date);
        req = new UnityWebRequest(uri,UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(postDate),
            downloadHandler = new DownloadHandlerBuffer()
        };
        req.SetRequestHeader("Content-Type", "application/json");
        yield return req.SendWebRequest();
    }
}
