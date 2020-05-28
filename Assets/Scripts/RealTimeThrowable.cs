using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Valve.VR.InteractionSystem;

public class RealTimeThrowable : Throwable
{
    bool DroppedSwag = false;//to prevent object from spawning more than one piece of swag
    RealtimeView rtView;
    //[HideInInspector]
    public int Ownership = -1;
    [HideInInspector]
    public RealtimeTransform rtTransform;
    // Start is called before the first frame update
    void Start()
    {
        rtTransform = gameObject.GetComponent<RealtimeTransform>();
        rtView = gameObject.GetComponent<RealtimeView>();
        
    }
    protected override void HandHoverUpdate(Hand hand)
    {
        GrabTypes bestGrabType = hand.GetBestGrabbingType();

        if (bestGrabType != GrabTypes.None && rtTransform.ownerID == -1)
        {
            //if (rigidbody.velocity.magnitude >= catchingThreshold)
            {
                hand.AttachObject(gameObject, bestGrabType, attachmentFlags);
            }
        }

    }
    
    public void Grabbed()
    {
        rtTransform.RequestOwnership();
        Ownership = rtTransform.ownerID;
    }

    public void PickUp()
    {
        if (DroppedSwag == false)
        {
            string swag = gameObject.name;
            swag = swag.Substring(0, swag.Length - 7);
            GameObject.FindObjectOfType<SwagSpawner>().PickUp(swag);
        }
        
    }

    public void Dropped()
    {
        DroppedSwag = true;
    }
    
}
