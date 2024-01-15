using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IHitable
{
    //public GameObject bullet;
    [SerializeField]protected float hp;
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0 && !isDie)
            {
                hp = 0;
                Die();
            }
            else if (hp > maxHp) 
                hp = maxHp;
        }
    }
    public float maxHp;
    protected bool isDie = false;
    public virtual void Die()
    {

    }
    public abstract void Hit(int _damage);
}
