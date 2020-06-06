using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : Interactable
{
    private Socket activeSocket = null;

    public override void StartInteraction(SocketHand hand)
    {
        hand.Pickup(this);
    }
    //public override void Interaction(SocketHand hand)
    //{
    //    GetComponent<ColorToggle>().ToggleColor();
    //}
    public override void EndInteraction(SocketHand hand)
    {
        hand.Drop();
    }
    public void AttachNewSocket(Socket newSocket)
    {
        if(newSocket.GetStoredObject())
        {
            return;
        }
        ReleaseOldSocket();
        activeSocket = newSocket;
        isAvailable = false;
    }
    public void ReleaseOldSocket()
    {
        if (!activeSocket)
        {
            return;
        }
        activeSocket.Detach();
        activeSocket = null;
        isAvailable = true;
    }
}
