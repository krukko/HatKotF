using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTarget : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnTargetClickedEventHandler(ClickTarget target);
    public event OnTargetClickedEventHandler OnTargetClickedEvent;

    public void OnPointerClick(PointerEventData eventdata)
    {
        if(OnTargetClickedEvent != null)
        {
            OnTargetClickedEvent(this);
        }
    }
}
