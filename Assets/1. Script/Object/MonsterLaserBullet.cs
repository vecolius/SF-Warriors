using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLaserBullet : MonoBehaviour
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
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void SetDestroy()
    {
        //MonsterBulletObjectPool.instance.ReturnPool(gameObject);
        //BulletMultiObjectPool.instance.ReturnPool(gameObject, bulletOrigin.name);
        ObjectPoolManager.instance.ReturnPool(gameObject);
        deathTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & friendlyLayer) != 0)   //아군이 맞았을 떄,
        { }
        else if (other.transform.TryGetComponent(out IHitable col))
        {
            col.Hit(damage);
            //Destroy(gameObject);
        }
        SetDestroy();
    }
}
