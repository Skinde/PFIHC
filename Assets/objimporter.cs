using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Dummiesman;
using JetBrains.Annotations;
using TMPro;

public class objimporter : MonoBehaviour
{
    GameObject wrapper;
    string filePath;
    GameObject loadedObject;
    [SerializeField] private TextMeshProUGUI textGUI;
    private void Start()
    {
        filePath = $"{Application.persistentDataPath}/";
    }
    public void DownloadFile(string url)
    {

        string path = GetFilePath(url);
        if (File.Exists(path))
        {
            Debug.Log("Found file locally, Deleting...");
            Set_text_status("Found file locally, Deleting...", Color.green);
            File.Delete(path);
        }

        Set_text_status("Downloading...", Color.green);
        Debug.Log("Before Download");
        StartCoroutine(GetFileRequest(url, (UnityWebRequest req) =>
        {
            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError || req.result == UnityWebRequest.Result.DataProcessingError)
            {
                Set_text_status("Error: " + $"{req.error} : {req.downloadHandler.text}", Color.red);
                // Log any errors that may happen
                Debug.Log($"{req.error} : {req.downloadHandler.text}");

            }
            else
            {
                // Save the model into a new wrapper
                Debug.Log("Loading...");
                Set_text_status("Loading...", Color.green);
                LoadModel(path);
                Set_text_status("", Color.green);
            }
        }));
    }

    string GetFilePath(string url)
    {
        string filename = "fourthtemp.obj";
        Debug.Log($"{filePath}{filename}");

        return $"{filePath}{filename}";
    }

    void LoadModel(string path)
    {
        if (loadedObject != null)
            Destroy(loadedObject);
            loadedObject = new OBJLoader().Load(path);
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

    void Set_text_status(string text, Color text_color)
    {
        textGUI.text = text; 
        textGUI.color = text_color;
    }
}
