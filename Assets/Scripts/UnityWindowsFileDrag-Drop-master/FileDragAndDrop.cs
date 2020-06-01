using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using B83.Win32;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

public class FileDragAndDrop : MonoBehaviour
{
    DropInfo dropInfo = null;
    public string DropFolder;//folder to drop files into
    
    class DropInfo
    {
        public string file;
        public Vector2 pos;
    }

    List<string> log = new List<string>();
    void OnEnable ()
    {
        // must be installed on the main thread to get the right thread id.
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += OnFiles;
    }
    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }

    void OnFiles(List<string> aFiles, POINT aPos)
    {
       
        string file = "";
        // do something with the dropped file names. aPos will contain the 
        // mouse position within the window where the files has been dropped.
        string str = "Dropped " + aFiles.Count + " files at: " + aPos + "\n\t" +
            aFiles.Aggregate((a, b) => a + "\n\t" + b);
        foreach (var f in aFiles)
        {
            
            var fileInfo = new System.IO.FileInfo(f);
            var ext = fileInfo.Extension.ToLower();
            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".dae" || ext == ".fbx" || ext == ".3ds" || ext == ".dxf" || ext == ".obj" )
            {
                file = f;

                File.Copy(@file, DropFolder + fileInfo.Name);
                //FindObjectOfType<SwagSpawner>().SpawnSwag(fileInfo.Name);
                break;
            }
        }
        // If the user dropped a supported file, create a DropInfo
        if (file != "")
        {
            var info = new DropInfo
            {
                file = file,
                pos = new Vector2(aPos.x, aPos.y)
               
            };
            dropInfo = info;
        }
        Debug.Log(str);
        log.Add(str);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("clear log"))
            log.Clear();
        foreach (var s in log)
            GUILayout.Label(s);
    }
}
