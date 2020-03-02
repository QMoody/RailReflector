using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private Transform _playerT;
    
    private Vector3 movement;
    private CharacterController _characterController;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        locomotion();
        Rotation();
    }

    void locomotion()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        _characterController.Move(movement * _speed * Time.deltaTime);
    }

    void Rotation()
    {
        int rot = 0;
        movement.Normalize();
        Debug.Log(movement);

        if(movement.y >= 0.5)
        {
            if (movement.x >= 0.5)
            {
                rot = -45;
            }
            else if (movement.x <= -0.5)
            {
                rot = 45;
            }
            else
            {
                rot = 0;
            }
        }
        else if(movement.y <= -0.5)
        {
            if (movement.x >= 0.5)
            {
                rot = 45;
            }
            else if (movement.x <= -0.5)
            {
                rot = -45;
            }
            else
            {
                rot = 180;
            }
        }
        else
        {
            rot = 0;
        }

        _playerT.eulerAngles = new Vector3(0, 0, rot);
    }
}
