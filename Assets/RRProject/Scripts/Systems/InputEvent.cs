using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvent : MonoBehaviour
{
    [SerializeField] private KeyCode _input;

    public UnityEvent OnButtonPress;

    void Update()
    {
        if(Input.GetKeyDown(_input))
        {
            OnButtonPress.Invoke();
        }
    }
}
