using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public enum Player_moveState
    {
        idle,
        walk,
        left_walk,
        right_walk,
        back_walk,
        run,
        die
    }
    public enum Player_attackState
    {
        idle,
        reload,
        single,
        bust,
        auto,
        grenade
    }
    public Player_moveState player_moveState;
    public Player_attackState player_attackState;
    [Header("player_abillity")]
    public GameObject rifleBullet;
    [SerializeField]int totalBulletCount;
    public int maxTotalBulletCount = 150;
    public int TotalBulletCount
    {
        get
        {
            return totalBulletCount;
        }
        set
        {
            totalBulletCount = value;
            if(totalBulletCount <= 0) totalBulletCount = 0;
            else if(totalBulletCount > maxTotalBulletCount) totalBulletCount = maxTotalBulletCount;
        }
    }
    public float moveSpeed;

    public bool isReloading;
    public bool isReturning;

    [Header("Attack")]
    public Animator ani;
    public CameraControl mainCamera;
    public AimComponent aimComponent;
    public GameObject firPos;
    public WeaponStractegy weapon;

    [Header ("Skill")]
    public List<SkillStractegy> skillStractegies = null;
    public SkillGrenade grenadeComponent;
    public Transform grenadePos;
    public SkillRetrograde retrogradeComponent;


    void Start()
    {
        ani = GetComponent<Animator>();
        mainCamera = transform.GetChild(transform.childCount - 1).GetComponent<CameraControl>();
        aimComponent = mainCamera.gameObject.GetComponent<AimComponent>();
        weapon = GetComponent<Weapon>().weaponStractegy;
        grenadeComponent = GetComponent<SkillGrenade>();
        retrogradeComponent = GetComponent<SkillRetrograde>();

        SetSkillStractegy(grenadeComponent);
        SetSkillStractegy(retrogradeComponent);

        firPos = transform.GetChild(5).GetChild(1).GetChild(1).GetChild(0).gameObject;
        grenadePos = transform.GetChild(5).GetChild(2).GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(0);

        Hp = maxHp;
        isDie = false;
        player_moveState = Player_moveState.idle;
        player_attackState = Player_attackState.idle;
        TotalBulletCount = maxTotalBulletCount;
        isReloading = false;
        isReturning = false;
    }

    void Update()
    {
        if (isReturning) 
            return;
        if(player_moveState == Player_moveState.die) 
            return;
        TestAbillity();

        player_moveState = Player_moveState.idle;
        player_attackState = Player_attackState.idle;

        //playerObj를 camera에 맞춰 회전
        transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z);
        
        Move();
        SkillCoolTime();
        if(Input.GetKey(KeyCode.Mouse0) && (player_moveState != Player_moveState.run))
        {
            if(!isReloading && weapon.BulletCount > 0)  player_attackState = Player_attackState.auto;
        }
        if (Input.GetKeyDown(skillStractegies[0].keyCode))
        {
                skillStractegies[0].UseSkill();
        } 

        if(Input.GetKeyDown(KeyCode.C) && skillStractegies[1].isCanUseSkill)    Skill2();

        if (Input.GetKeyDown(KeyCode.R))    Reloading();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.TheWorld(!GameManager.instance.IsTimeStop);
            UIManager.instance.menuUI.SetActive(GameManager.instance.IsTimeStop);
            GameManager.instance.MouseCursorVisible(GameManager.instance.IsTimeStop);
        }
        AnimationSelect();
    }
    void Move()
    {
        Vector3 vec = Vector3.zero;
        float dashSpeed = 1f;

        if (Input.GetKey(KeyCode.W))
        {
            vec += Vector3.forward;
            bool isRun = Input.GetKey(KeyCode.LeftShift);
            dashSpeed = isRun ? 2f : 1f;
            player_moveState = isRun ? Player_moveState.run : Player_moveState.walk;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vec += Vector3.back;
            player_moveState = Player_moveState.back_walk;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vec += Vector3.left;
            player_moveState = Player_moveState.left_walk;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vec += Vector3.right;
            player_moveState = Player_moveState.right_walk;
        }
        transform.Translate(vec.normalized * (moveSpeed * dashSpeed));
    }
    void Reloading()
    {
        player_attackState = Player_attackState.reload;
        isReloading = true;
    }
    void SkillCoolTime()
    {
        for(int i = 0; i<skillStractegies.Count; i++)
        {
            skillStractegies[i].SkillCooltime += Time.deltaTime;
        }
    }
    void SetSkillStractegy(SkillStractegy stractegy)     //스킬 항목 추가 함수
    {
        skillStractegies.Add(stractegy);
    }
    void Skill2()        //역행
    {
        skillStractegies[1].UseSkill();
    }
    public override void Die()
    {
        //체력이 0이 되면
        if (isDie)
            return;
        player_moveState = Player_moveState.die;
        isDie = true;
        SceneManager.LoadScene("Ending");
        //AnimationSelect() - 사망 모션 구현
    }
    public override void Hit(int _damage)
    {
        //IHitable 구현
        Hp -= _damage;
    }

    void AnimationSelect()           //Animation
    {
        switch(player_moveState) { 
            case Player_moveState.idle:
                ani.SetFloat("moveState", 0f);  //idle
                break;
            case Player_moveState.walk:
                ani.SetFloat("moveState", 0.2f);
                break;
            case Player_moveState.left_walk:
                ani.SetFloat("moveState", 0.4f);
                break;
            case Player_moveState.right_walk:
                ani.SetFloat("moveState", 0.6f);
                break;
            case Player_moveState.back_walk:
                ani.SetFloat("moveState", 0.8f);
                break;
            case Player_moveState.run:
                ani.SetFloat("moveState", 1f);
                break;
            case Player_moveState.die:
                //사망 모션
                ani.SetTrigger("DieTrigger");
                break;
        }
        switch (player_attackState)
        {
            case Player_attackState.idle:
                ani.SetFloat("attackState", 0f);
                weapon.TargetPosRadius = 0f;
                break;
            case Player_attackState.reload:
                ani.SetTrigger("ReloadTrigger");
                break;
            case Player_attackState.single:
                ani.SetFloat("attackState", 0.25f);
                break;
            case Player_attackState.bust:
                ani.SetFloat("attackState", 0.5f);
                break;
            case Player_attackState.auto:
                ani.SetFloat("attackState", 0.75f);
                break;
            case Player_attackState.grenade:
                ani.SetTrigger("GrenadeTrigger");
                break;
        }
    } 
    public void LaserShoot()    //총이 발사될 때, Animation event
    {
        if(weapon.BulletCount <= 0) return;
        Vector3 aimPos = Random.insideUnitSphere * weapon.TargetPosRadius + aimComponent.aimPos;
        weapon.TargetPosRadius += 0.1f;
        weapon.WeaponShootStractegy(firPos, aimPos);
        weapon.BulletCount -= 1;
    }
    public void Reload()    //재장전 Animation event
    {
        SoundManager.instance.SFXPlay("Reload", SoundManager.instance.SPXclips[2], transform);
        int weaponBulletCount = weapon.maxBulletCount - weapon.BulletCount;

        if (TotalBulletCount >= weaponBulletCount)
            weapon.BulletCount = weapon.maxBulletCount;
        else
            weapon.BulletCount += TotalBulletCount;

        TotalBulletCount -= weaponBulletCount;
        isReloading = false;
    }
    public void SkillThrowGrenade()      //수류탄 투척 스킬, Animation event
    {
        skillStractegies[0].UseSkill();
    }


    void TestAbillity()              //개발자 편의 test
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject targetMonsterObj = GameObject.FindObjectOfType<Monster>().gameObject;
            targetMonsterObj.GetComponent<Monster>().Die();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Hp += 10;
        }
    }
}
