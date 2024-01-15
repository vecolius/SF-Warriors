using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Character
{
    public GameObject turretWeapon;
    [SerializeField] Transform leftFirePos;
    [SerializeField] Transform rightFirePos;
    [SerializeField] bool isLeftShoot;
    [SerializeField] float attackSpeed;
    public GameObject bullet;

    public DetectiveComponent detectiveComponent;
    void Start()
    {
        detectiveComponent = GetComponent<DetectiveComponent>();
        Hp = maxHp;
        turretWeapon = transform.GetChild(0).GetChild(1).gameObject;
        leftFirePos = turretWeapon.transform.GetChild(0);
        rightFirePos = turretWeapon.transform.GetChild(1);
        isLeftShoot = false;
        StartCoroutine(ShootCo());
    }

    void Update()
    {
        if (detectiveComponent.IsDection)
        {
            Vector3 newPos = detectiveComponent.LastDetectivePos + new Vector3(0, 1.5f, 0);
            Vector3 targetVec = (newPos - detectiveComponent.layerMaskSencer.transform.position).normalized;
            transform.forward = targetVec;
        }
    }
    IEnumerator ShootCo()
    {
        while(true)
        {
            if(detectiveComponent.IsDection)
            {
                if (isLeftShoot)
                {
                    TurretShoot(leftFirePos);
                    isLeftShoot = false;
                }
                else
                {
                    TurretShoot(rightFirePos);
                    isLeftShoot = true;
                }
                yield return new WaitForSeconds(attackSpeed);
            }
            yield return null;
        }
    }
    void TurretShoot(Transform firePos)
    {
        //BulletObjectPool.instance.PopObj(firePos.position, firePos.rotation);
        //BulletMultiObjectPool.instance.PopObj(bullet, firePos.position, firePos.rotation);
        ObjectPoolManager.instance.PopObj(bullet, firePos.position, firePos.rotation);
        SoundManager.instance.SFXPlay("Turret Fire", SoundManager.instance.SPXclips[1], firePos);
    }
    public override void Die()
    {
        Destroy(gameObject);
    }
    public override void Hit(int _damage)
    {
        Hp -= _damage;
    }
}
