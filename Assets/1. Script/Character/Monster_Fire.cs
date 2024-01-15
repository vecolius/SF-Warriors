using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Fire : MonoBehaviour
{
    //public GameObject bullet;
    public GameObject monsterFirePos;
    public float fireSpeed;
    [SerializeField] bool isReadyFire = false;
    public float FireSpeed
    {
        get { return fireSpeed; }
        set
        {
            fireSpeed = value;
            if (fireSpeed < 0)
                fireSpeed = 1f;
        }
    }
    public bool IsReadyFire
    {
        get { return isReadyFire; }
        set { isReadyFire = value; }
    }
    /*
    public IEnumerator BulletFireCo(float _fireSpeed)
    {
        while (true)
        {
            if (IsReadyFire)
            {
                //Instantiate(bullet, transform.position, transform.rotation);
                BulletObjectPool.instance.PopObj(monsterFirePos.transform.position, monsterFirePos.transform.rotation);
            }
            yield return new WaitForSeconds(_fireSpeed);
        }
    }
    */
}
