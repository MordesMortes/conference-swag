using UnityEngine;
using Valve.VR;

public class Belt : MonoBehaviour
{
    [Range(0.5f, 0.75f)]
    public float hight = 0.5f;
    public Transform head;
    //private Transform head = null;

    //private void Start()
    //{
    //    head = SteamVR_Render.Top().head;
    //}
    private void Update()
    {
        PositionUnderHead();
        RotateWithHead();
    }

    private void PositionUnderHead()
    {
        Vector3 adjustedHight = head.localPosition;
        adjustedHight.y = Mathf.Lerp(0.0f, adjustedHight.y, hight);

        transform.localPosition = adjustedHight;
    }
    private void RotateWithHead()
    {
        Vector3 adjustedRotation = head.localEulerAngles;
        adjustedRotation.x = 0;
        adjustedRotation.z = 0;

        transform.localEulerAngles = adjustedRotation;
    }
}
