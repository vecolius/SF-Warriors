using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_Shoot : MonoBehaviour
{
    public GameObject monsterBullet;
    public Monster monsterComponent;
    public Transform firPos;
    private void Start()
    {
        monsterComponent = transform.GetComponentInParent<Monster>();
        firPos = monsterComponent.monsterFirePos;
    }
    public void Shoot()
    {
        //MonsterBulletObjectPool.instance.PopObj(firPos.position, firPos.rotation);
        //BulletObjectPool.instance.PopObj(monsterComponent.bullet.name, firPos.position, firPos.rotation);
        ObjectPoolManager.instance.PopObj(monsterBullet, firPos.position, firPos.rotation);
    }
}
