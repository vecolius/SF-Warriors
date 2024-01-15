using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Character
{
    public GameObject bullet;
    public float moveSpeed;
    public int damage;
    public GameObject[] dropItems;
    public enum Monster_State
    {
        idle,
        walk,
        attack,
        die
    }
    public Monster_State monster_state;

    public DetectiveComponent detectiveComponent;

    public Transform goal;
    public NavMeshAgent agent;
    public Monster_Fire monsterFire;
    public Transform monsterFirePos;

    public Animator ani;
    void Start()
    {
        detectiveComponent = GetComponent<DetectiveComponent>();
        agent = GetComponent<NavMeshAgent>();
        //monsterFire = GetComponent<Monster_Fire>();
        ani = transform.GetChild(0).GetComponent<Animator>();
        Hp = maxHp;
        isDie = false;
    }
    void Update()
    {
        if (isDie) return;
        if (detectiveComponent.IsRangeDetection)
        {
            Vector3 targetVec = (detectiveComponent.LastDetectivePos - transform.position).normalized;
            if (detectiveComponent.IsDection)   //detectiveComponent.IsRayinTarget
            {
                transform.forward = targetVec;
                monster_state = Monster_State.attack;
                agent.enabled = false;
                //agent.isStopped = false;
            }
            else
            {
                //agent.isStopped = true;
                agent.enabled = true;
                monster_state = Monster_State.walk;
                goal = detectiveComponent.TargetObject.transform;
                agent.SetDestination(goal.position);
            }
        }
        else
        {
            monster_state = Monster_State.idle;
        }
        AnimationSelect();
    }
    void AnimationSelect()
    {
        switch(monster_state)
        {
            case Monster_State.idle:
                ani.SetBool("isDeath", false);
                ani.SetBool("isShoot", false);
                ani.SetBool("isWalk", false);
                break;
            case Monster_State.walk:
                ani.SetBool("isShoot", false);
                ani.SetBool("isWalk", true);
                break;
            case Monster_State.attack:
                ani.SetBool("isShoot", true);
                ani.SetBool("isWalk", false);
                break;
            case Monster_State.die:
                ani.SetBool("isShoot", false);
                ani.SetBool("isWalk", false);
                ani.SetBool("isDeath", true);
                break;
        }
    }

    public override void Die()
    {
        if (monster_state == Monster_State.die)
            return;
        //체력이 0이 되면
        agent.enabled = false;
        isDie = true;
        monster_state = Monster_State.die;
        GameManager.instance.Point += 1;
        //GameManager.instance.monsterCount--;
        WaveManager.instance.monsterCount--;
        int randomNumber = Random.Range(0, 3);
        Instantiate(dropItems[randomNumber], transform.position + new Vector3(0, 0.2f, 0), dropItems[randomNumber].transform.rotation);
        Destroy(gameObject, 0.5f);
    }
    public override void Hit(int m_damage)
    {
        //IHitable 구현
        Hp -= m_damage;
        //Debug.Log(gameObject.name + "hp : " + Hp);
    }
}
