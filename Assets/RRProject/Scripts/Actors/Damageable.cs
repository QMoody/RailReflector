using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows an object to be damaged, have health , have knockback, events for dead and damaged
/// </summary>
public class Damageable : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private bool _inmortal;
    [SerializeField] private UnityEvent _onDamaged;
    [SerializeField] private UnityEvent _onDead;
    [SerializeField] private bool _reciveKnockback = true;
    [SerializeField] private float _knockbackMultiplier;

    private Rigidbody2D _rigidbody;
    private PlayerController _playerController;
    private bool _initialized = false;

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
    public void reciveDamage(Vector3 direcction, int damage)
    {

        if(!_initialized)
        {
            return;
        }

        _onDamaged.Invoke();
        if(_reciveKnockback)
        {
            applyKnockback(direcction);
        }

        if(!_inmortal)
        {
            _health -= (int)((float)damage);
            if(_health <= 0)
            {
                _onDead.Invoke();
                gameObject.GetComponent<deathAnim>().enabled = true;
                
            }
        }
    }

    /// <summary>
    /// Aply knockback based on the compomnent that the object poses
    /// </summary>
    /// <param name="direcction"></param>
    void applyKnockback(Vector3 direcction)
    {
        if(_rigidbody != null)
        {
            _rigidbody.velocity = direcction * _knockbackMultiplier;
        }
        //else if(_playerController != null)
        //{
        //    _playerController.impact = direcction * _knockbackMultiplier;
        //}
    }
}
