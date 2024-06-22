using Dummiesman;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System;
using System.Collections;
using System.Net.Http;
using System.Text;

public class ObjFromFile : MonoBehaviour
{
    string DownloadFileURL = "https://drive.usercontent.google.com/download?id=1lP7U87cbQr4-7ySZm319rUm8jaoOuvdx&export=download&authuser=0";
    string file_name = "Helicopter.obj";
    GameObject loadedObject;
    void Start()
    {
        WebClient client = new WebClient();
        client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(SucessLoadTheFile);
        client.DownloadFileAsync(new Uri(DownloadFileURL), Path.Combine(Application.persistentDataPath, file_name));
        Debug.Log("Download Completed Attempting to load file!");
    }

    void SucessLoadTheFile(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        string objPath = Path.Combine(Application.persistentDataPath, file_name);
        if (!File.Exists(objPath))
        {
            Debug.LogError("File Not Found... Waiting");
        }
        {
            Debug.Log("Found File! " + objPath);
            if (loadedObject != null)
                Destroy(loadedObject);
            loadedObject = new OBJLoader().Load(objPath);
        }
    }

}