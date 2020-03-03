using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorProjectile : MonoBehaviour
{

    public int damage = 1;

    [Header("Gameobjects & Components")]
    public GameObject playerObj;
    public LayerMask layMask;

    [Header("Reflector Variables")]
    public float speed;
    public Vector2 refDir;
    public float rayWallDis; //0.01?
    public bool firstObjectHit;
    public int reflectMax;
    public int reflectNum;

    //-//-//-//-//-//-//-//-//-//-//-//-//-//-//-//

    private void Start()
    {
        StartCoroutine(DestoryAfterTime());
    }

    void Update()
    {
        MoveProj();
        PredictReflect();
        Reflect();
    }

    void MoveProj()
    {
        float step = Time.deltaTime;
        Vector3 tmpDir = refDir;
        transform.position += tmpDir * speed * step;

        Debug.DrawLine(transform.position, transform.position + tmpDir * 0.1f, Color.cyan);
    }

    void Reflect()
    {
        //Chang this to a cricle collider raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, refDir, rayWallDis, layMask);

        if (firstObjectHit == false)
            firstObjectHit = true;

        if (hit.collider != null)
        {
            refDir = Vector2.Reflect(refDir, hit.normal);
            reflectNum += 1;

            if (reflectNum >= reflectMax)
                Destroy(this.gameObject);

            Debug.DrawLine(hit.normal, hit.normal * 0.25f, Color.green);
        }
    }

    void PredictReflect()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, refDir, 3, layMask);

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.DrawLine(hit.point, hit.point + Vector2.Reflect(refDir, hit.normal) * 3, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, hit.point * 1, Color.red);
        }
    }

    IEnumerator DestoryAfterTime()
    {
        yield return new WaitForSeconds(30);

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable damageable = collision.collider.GetComponent<Damageable>();
        if(damageable != null)
        {
            damageable.reciveDamage(transform.forward, damage);
        }
    }
}
