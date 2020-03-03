using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private Transform _playerT;
    [SerializeField] private Transform _gunCannon;
    public GameObject projPrefab;

    private Vector3 movement;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        _rigidbody2D.MovePosition(transform.position + (movement * _speed * Time.deltaTime));
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -15, 13), 0);
    }

    void Rotation()
    {
        int rot = 0;
        movement.Normalize();

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
            //else
            //{
            //    rot = 180;
            //}
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
            GameObject tmp = Instantiate(projPrefab, _gunCannon.position, Quaternion.Euler(0, 0, 0));
            tmp.GetComponent<ReflectorProjectile>().refDir = _playerT.up;
        }
    }
}
