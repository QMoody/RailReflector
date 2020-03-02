using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    
    void Update()
    {
        locomotion();
    }

    void locomotion()
    {
        transform.Translate(new Vector3(0, -1 * _speed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
