using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragContainser : MonoBehaviour
{
    [NonSerialized] public RectTransform Rect;

    void Start()
    {
        Rect = GetComponent<RectTransform>();
    }

    
}
