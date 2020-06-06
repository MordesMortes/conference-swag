using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class SocketHand : MonoBehaviour
{
    private Socket socket = null;
    private SteamVR_Behaviour_Pose pose = null;

    public List<Interactable> contactIneractibles = new List<Interactable>();

    private void Awake()
    {
        socket = GetComponent<Socket>();
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AddInteractable(other.gameObject);
    }

    
    private void OnTriggerExit(Collider other)
    {
        RemoveInteractable(other.gameObject);
    }
    private void AddInteractable(GameObject newObject)
    {
        Interactable newInteractable = newObject.GetComponent<Interactable>();
        contactIneractibles.Add(newInteractable);
    }

    private void RemoveInteractable(GameObject newObject)
    {
        Interactable existingInteractable = newObject.GetComponent<Interactable>();
        contactIneractibles.Remove(existingInteractable);
    }
    public void TryInteraction()
    {
        if (NearestInteraction())
        {
            return;
        }
        HeldInteraction();
    }
    public bool NearestInteraction()
    {
        contactIneractibles.Remove(socket.GetStoredObject());
        Interactable nearestObject = Utility.GetNearestInteractable(transform.position, contactIneractibles);

        if (nearestObject)
        {
            nearestObject.StartInteraction(this);
        }
        return nearestObject;
    } 
    private void HeldInteraction()
    {
        if (!HasHeldObject())
        {
            return;
        }
        Moveable heldObject = socket.GetStoredObject();
        //heldObject.Interaction(this);
    }
    public void StopInteraction()
    {
        if (!HasHeldObject())
        {
            return;
        }
        Moveable heldObject = socket.GetStoredObject();
        heldObject.EndInteraction(this);
    }
    public void Pickup(Moveable moveable)
    {
        moveable.AttachNewSocket(socket);
    }
    public Moveable Drop()
    {
        if (!HasHeldObject())
        {
            return null;
        }
        Moveable detatchedObject = socket.GetStoredObject();
        detatchedObject.ReleaseOldSocket();

        Rigidbody rigidbody = detatchedObject.gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = pose.GetVelocity();
        rigidbody.angularVelocity = pose.GetAngularVelocity();
        
        return detatchedObject;               
    }
    public bool HasHeldObject()
    {
        return socket.GetStoredObject();
    }
}