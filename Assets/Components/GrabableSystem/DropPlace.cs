using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropPlace : MonoBehaviour
{
    [System.Flags]
    public enum CheckMode
    {
        CheckObject = 1,
        CheckGrabableType = 2
    }

    [Header("Setup")]
    [SerializeField] private List<Grabable> _validGrabables = new();
    [SerializeField] private Grabable.GrabableType _validGrabableTypes = new();
    [SerializeField] private CheckMode _checkMode;

    [Header("Events")]
    public UnityEvent<GameObject> OnObjectDropped;
    public UnityEvent<GameObject> OnObjectGrabbed;

    public bool isValid(Grabable grabable)
    {
        bool isValid = true;

        if(_checkMode.HasFlag(CheckMode.CheckObject))
        {
            if(_validGrabables.Contains(grabable))
            {
                return true;
            }
        }

        if (_checkMode.HasFlag(CheckMode.CheckGrabableType))
        {
            if (!_validGrabableTypes.HasFlag(grabable.grabableType))
            {
                isValid = false;
            }
        }

        return isValid;
    }

    public void OnDrop(Grabable grabable)
    {
        OnObjectDropped.Invoke(grabable.gameObject);
        grabable.OnStartGrab.AddListener(OnGrab);
    }

    private void OnGrab(GameObject garbableObject, GameObject parent)
    {
        if(garbableObject.TryGetComponent(out Grabable grabable))
        {
            grabable.OnStartGrab.RemoveListener(OnGrab);
        }
        OnObjectGrabbed.Invoke(garbableObject);
    }
}
