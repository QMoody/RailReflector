using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeEvent : MonoBehaviour
{
    [SerializeField] private float _timeToTriggerEvent;
    public UnityEvent OnButtonPress;

    void Start()
    {
        Invoke("doEvent", _timeToTriggerEvent);
    }

    void doEvent()
    {
        OnButtonPress.Invoke();
    }
}
