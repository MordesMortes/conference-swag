using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool isAvailable = true;

    public virtual void StartInteraction(SocketHand hand)
    {

    }

    public virtual void EndInteraction(SocketHand hand)
    {

    }
    public bool GetAvailability()
    {
        return isAvailable;
    }
}
