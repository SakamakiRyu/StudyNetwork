using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class HTTPWorker : MonoBehaviour
{
    public void Request<T>(string url, Action<T> action)
    {
        StartCoroutine(RequestAsync(url, action));
    }

    private IEnumerator RequestAsync<T>(string url, Action<T> action)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;

        T result = JsonUtility.FromJson<T>(text);
        action(result);
    }
}
