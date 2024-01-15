using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponStractegy
{
    public int gunDamage;   //������
    public float firingSpeed; //�߻�ӵ�
    int bulletCount;            //źâ�� ���� �Ѿ� ��
    public int BulletCount
    {
        get { return bulletCount; }
        set
        {
            bulletCount = value;
            if (bulletCount <= 0)
                bulletCount = 0;
            else if (bulletCount > maxBulletCount)
                bulletCount = maxBulletCount;
        }
    }
    public int maxBulletCount;//źâ�� �ִ�� ���� �Ѿ� ��

    float maxTargetPosRadius = 1.0f;
    float targetPosRadius;   //ź���� ����
    public float TargetPosRadius
    {
        get
        {
            return targetPosRadius;
        }
        set
        {
            targetPosRadius = value;
            if (targetPosRadius > maxTargetPosRadius) targetPosRadius = maxTargetPosRadius;
        }
    }
    public GameObject bullet;
    public abstract void WeaponShootStractegy(GameObject firPos, Vector3 targetPos);
}
public class RifleWeaponStractegy : WeaponStractegy
{
    public RifleWeaponStractegy()
    {
        maxBulletCount = 30;
        BulletCount = maxBulletCount;
    }
    public override void WeaponShootStractegy(GameObject firPos, Vector3 targetPos)
    {
        //GameObject bulletObj = BulletObjectPool.instance.PopObj(firPos.transform.position, firPos.transform.rotation);
        GameObject bulletObj = ObjectPoolManager.instance.PopObj(GameManager.instance.player.rifleBullet, firPos.transform.position, firPos.transform.rotation);
         SoundManager.instance.SFXPlay("rifleShoot", SoundManager.instance.SPXclips[0], firPos.transform);
         bulletObj.transform.forward = (targetPos - bulletObj.transform.position).normalized;
    }
}

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        none,
        rifle
    }
    public WeaponType weaponType;

    public WeaponStractegy weaponStractegy = null;
    private void Start()
    {
        CheckWeaponType();
    }
    void CheckWeaponType()
    {
        switch (weaponType)
        {
            case WeaponType.none:
                weaponStractegy = null;
                break;
            case WeaponType.rifle:
                weaponStractegy = new RifleWeaponStractegy();
                break;
        }
    }
}
