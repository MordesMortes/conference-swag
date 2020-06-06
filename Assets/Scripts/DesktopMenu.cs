using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesktopMenu : MonoBehaviour , IDropHandler
{
 public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("InMenu");
        if (eventData.pointerDrag != null)
        { 
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
        
}
