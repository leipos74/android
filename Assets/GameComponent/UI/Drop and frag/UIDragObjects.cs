using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragObjects : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    private RectTransform _rect;
    public Transform _targetParent;

    // Start is called before the first frame update
    void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _targetParent = _rect.parent;
        _rect.SetParent(GetComponentInParent<DragContainser>().Rect);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter == null || eventData.pointerEnter.transform as RectTransform == null) return;

        RectTransform plane = eventData.pointerEnter.transform as RectTransform;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(plane, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos))
        {
            _rect.position = globalMousePos;
            _rect.rotation = plane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rect.SetParent(_targetParent);
    }
}
