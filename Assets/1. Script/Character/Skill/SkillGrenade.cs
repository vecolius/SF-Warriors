using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGrenade : SkillStractegy
{
    public GameObject grenade;
    public LayerMask playerLayer;
    public LayerMask targetLayerMask;

    private void Start()
    {
        skillMaxCooltime = 8.0f;
        SkillCooltime = skillMaxCooltime;
    }
    //public override void UseSkill() 
    //{
    //    GameObject grenade = Instantiate(this.grenade, GameManager.instance.player.grenadePos.position, this.grenade.transform.rotation);
    //    //grenade.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up).normalized * 10f, ForceMode.Impulse);
    //}

    public override void SkillEffect()
    {
        SkillCooltime = 0;
        Instantiate(this.grenade, GameManager.instance.player.grenadePos.position, this.grenade.transform.rotation);
    }
}
