using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [Header("Main Settings")]
    public string tagObject;
    public UnityEvent onTrigger;
    public bool destroyTriggerAfterUse = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObject))
        {
            onTrigger?.Invoke();
            if (destroyTriggerAfterUse)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
