using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class JsonRequest : MonoBehaviour
{
    [SerializeField]
    private HTTPWorker worker;

    readonly string url = "";

    void Start()
    {
        worker.Request<UserModel>(url, ToString);
    }

    public void ToString(UserModel model)
    {
        Debug.Log(model.id);
        Debug.Log(model.name);
        Debug.Log(model.log);
    }
}
