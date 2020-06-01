using Normal.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class SwagSpawner : MonoBehaviour
{
    public List<GameObject> SwagObjects = new List<GameObject>();//all the objects we want to be swag
    public List<Transform> SwagObjectPlacement = new List<Transform>();//all the placement of where we want swag objects to respawn
    public Hashtable Swag = new Hashtable();//all the info on the swag
    [Range (1,300)]
    public float SwagWait;//seconds to wait before respawning swag
    public GameObject SwagPresets;//the object that contains all the presets we want swag to have usually an empty
    readonly GameObject SwagDownload;//variable used to download swag from local or remote files
                                     

    private void Awake()
    {
        //SwagObjects.AddRange(Resources.LoadAll<GameObject>(FindObjectOfType<FileDragAndDrop>().DropFolder));//grabs all objects in the drop folder
        SwagObjects.AddRange(Resources.LoadAll<GameObject>("swag/"));//grabs all objects in the resources/swag folder
    }
    private void Start()
    {
        foreach (GameObject item in SwagObjects)
        {
            GameObject i = item;
            SwagObjectPlacement.Add(i.transform);
            //i.SetActive(false);
            SpawnSwag(i.name);
        }

    }
    public void SpawnSwag(String Swag)
    {
        
        GameObject i = SwagObjects.Find(item => item.name == Swag);
        GameObject Parent = Instantiate(SwagPresets);
        GameObject clone = Instantiate(i, i.transform.position, UnityEngine.Quaternion.identity);
        //Debug.Log(i.name + " SpawnSwag");
        //clone.SetActive(true);
        clone.transform.SetParent(Parent.transform);
        
    }

    public IEnumerator GetSwag(string FileLocation)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(FileLocation))
        {
            //requesting file from internet or locally and wait for return
            yield return webRequest.SendWebRequest();

            string[] pages = FileLocation.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                //webRequest.downloadHandler.
            }
        }

    }

    public void PickUp(string swag)
    {
        
        StartCoroutine(WaitToRespawn(swag));
        
        //WaitToRespawn(swag);
        //SpawnSwag(swag);
        //Debug.Log(swag);
    }

    private IEnumerator WaitToRespawn(string swag)//delays respawning the swag to prevent swag yeeting
    {
        yield return new WaitForSeconds(SwagWait);
        SpawnSwag(swag);
                
    }
}
