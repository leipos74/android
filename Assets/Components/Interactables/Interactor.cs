using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [Header("Debug Suport")]
    [SerializeField] private Interactable _currentInteractable;

    [Header("Setup")]
    [SerializeField] private float _radiusRange = 0.5f;

    public float RadiusRange { get => _radiusRange; }

    private IEnumerator _checkBreakDistanceCorrutine;
    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            TryStartInteract();
        }
        else
        {
            TryEndInteract();
        }
    }

    private void TryStartInteract()
    {
        Vector3 position = transform.position;
        int inverseMask = ~(gameObject.layer);
        List<Collider> colliders = Physics.OverlapSphere(position, _radiusRange, inverseMask, QueryTriggerInteraction.Collide).ToList();

        colliders.Sort((a, b) =>
        {
            float dA = Vector3.Distance(a.ClosestPoint(position), position);
            float dB = Vector3.Distance(b.ClosestPointOnBounds(position), position);
            return dA.CompareTo(dB);
        });

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out Interactable interactable))
            {
                _currentInteractable = interactable;
                _currentInteractable.StartInteract(gameObject);

                if(interactable.breakWithDistance)
                {
                    _checkBreakDistanceCorrutine = CheckBreakDistanceCorrutine(collider);
                    StartCoroutine(_checkBreakDistanceCorrutine);
                }

                return; //Early Exit
            }
        }
    }

    private void TryEndInteract()
    {
        if(_currentInteractable != null)
        {
            _currentInteractable.EndInteract(gameObject);
            _currentInteractable = null;
        }
        
        if(_checkBreakDistanceCorrutine != null)
        {
            StartCoroutine(_checkBreakDistanceCorrutine);
            _checkBreakDistanceCorrutine = null;
        }
    }


    private IEnumerator CheckBreakDistanceCorrutine(Collider targetCollider)
    {
        while (Vector3.Distance(targetCollider.ClosestPoint(transform.position), transform.position) <= _radiusRange)
        {
            yield return null;
        }

        TryEndInteract();
    }


#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _radiusRange);
        }

#endif
    
}
