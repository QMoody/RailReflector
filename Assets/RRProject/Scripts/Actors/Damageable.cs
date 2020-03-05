using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows an object to be damaged, have health , have knockback, events for dead and damaged
/// </summary>
public class Damageable : MonoBehaviour
{
    [SerializeField] public int _health;
    [SerializeField] private bool _inmortal;
    [SerializeField] private UnityEvent _onDamaged;
    [SerializeField] private UnityEvent _onDead;
    [SerializeField] private string tag;

    private Rigidbody2D _rigidbody;
    private PlayerController _playerController;
    private bool _initialized = false;
    private bool _dead = false;

    private void Awake()
    {
        //grab references to object components
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();

        Invoke("initializeDamage", 2); //initialize after 2 seconds
    }

    /// <summary>
    /// This is done so that when an object is created is inmortal for some seconds
    /// </summary>
    private void initializeDamage()
    {
        _initialized = true;
    }

    /// <summary>
    /// recieve damage to reduce health if needed
    /// recive knockback if needed
    /// trigger events for damage or dead if halth hits zero
    /// </summary>
    /// <param name="direcction"></param>
    /// <param name="distance"></param>
    /// <param name="damage"></param>
    public void reciveDamage(int damage, string dTag)
    {
        if (this.tag != dTag)
            return;

        if(!_initialized || _dead)
        {
            return;
        }

        _onDamaged.Invoke();

        if(!_inmortal)
        {
            _health -= (int)((float)damage);
            if(_health <= 0)
            {
                //dead
                _dead = true;
                _onDead.Invoke();
            }
        }
    }

    public void setHealth(int value)
    {
        _dead = false;
        _health = value;
    }
}
