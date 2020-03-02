using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private Transform _playerT;
    
    private Vector3 movement;
    private CharacterController _characterController;
    public GameObject projPrefab;
    public GameObject playerModel;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        locomotion();
        Rotation();
        FireMainWeapon();
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
        //Debug.Log(movement);

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

    void FireMainWeapon()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject tmp = Instantiate(projPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            tmp.GetComponent<ReflectorProjectile>().refDir = playerModel.transform.up;
        }
    }
}
