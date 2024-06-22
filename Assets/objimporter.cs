using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Dummiesman;


public class objimporter : MonoBehaviour
{
    GameObject wrapper;
    string filePath;
    GameObject loadedObject;
    string URL = "https://s3.amazonaws.com/files.free3d.com/models/2/607102309e9b225e754b2fe2/32-mercedes-benz-gls-580-2020.zip?X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIA5DEPHINMSI4OS3OO%2F20240618%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240618T221454Z&X-Amz-SignedHeaders=host&X-Amz-Expires=1200&X-Amz-Signature=08b603950904742fefd8a7b97871e011168b33f40b891a2392e952c2aeda6fd2";

    private void Start()
    {
        filePath = $"{Application.persistentDataPath}/";
        DownloadFile(URL);
    }
    public void DownloadFile(string url)
    {
        string path = GetFilePath(url);
        if (File.Exists(path))
        {
            Debug.Log("Found file locally, loading...");
            LoadModel(path);
            return;
        }

        Debug.Log("Before Download");
        StartCoroutine(GetFileRequest(url, (UnityWebRequest req) =>
        {
            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError || req.result == UnityWebRequest.Result.DataProcessingError)
            {
                // Log any errors that may happen
                Debug.Log($"{req.error} : {req.downloadHandler.text}");
            }
            else
            {
                // Save the model into a new wrapper
                Debug.Log("File not found downloading...");
                LoadModel(path);
            }
        }));
    }

    string GetFilePath(string url)
    {
        string[] pieces = url.Split('/');
        string filename = pieces[pieces.Length - 1];

        return $"{filePath}{filename}";
    }

    void LoadModel(string path)
    {
        if (loadedObject != null)
            Destroy(loadedObject);
            loadedObject = new OBJLoader().Load(path);
            loadedObject.transform.SetParent(wrapper.transform);
    }

    IEnumerator GetFileRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            req.downloadHandler = new DownloadHandlerFile(GetFilePath(url));
            yield return req.SendWebRequest();
            callback(req);
        }
    }
}
