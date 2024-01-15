using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    public LayerMask friendlyLayer;
    public int damage;
    public float speed;
    float deathTime;
    public GameObject bulletOrigin;

    void Update()
    {
        deathTime += Time.deltaTime;
        if (deathTime >= 1.5f)
            SetDestroy();
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    void SetDestroy()
    {
        //BulletObjectPool.instance.ReturnPool(gameObject);
        //BulletMultiObjectPool.instance.ReturnPool(gameObject);
        ObjectPoolManager.instance.ReturnPool(gameObject);
        deathTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & friendlyLayer) != 0)   //아군이 맞았을 떄,
        {
        }
        else if (other.transform.TryGetComponent(out IHitable col))
        {
            col.Hit(damage);
            //Destroy(gameObject);
        }
        SetDestroy();
    }
}
