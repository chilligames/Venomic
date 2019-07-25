using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI : MonoBehaviour, IDragHandler
{

    public void OnDrag(PointerEventData eventData)
    {
        Player.Cam.Native_camera(eventData.delta);
    }


    /// <summary>
    /// 1: chek mikone status zoom 
    /// </summary>
    public void Press_BTN_zoom()
    {
        Player.Cam.Zoom_Back();

    }
}
