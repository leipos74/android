using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIDropObject : MonoBehaviour, IDropHandler
{
    

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droped = eventData.pointerDrag;
        if (droped.TryGetComponent(out UIDragObjects gO))
        {
            gO._targetParent = transform;
        }
    }
}
