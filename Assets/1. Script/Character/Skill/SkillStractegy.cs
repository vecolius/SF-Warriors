using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillStractegy : MonoBehaviour
{
    public KeyCode keyCode;
    protected float skillCooltime;
    public float SkillCooltime
    {
        get 
        { 
            return skillCooltime; 
        }
        set 
        { 
            skillCooltime = value; 
            if(skillCooltime <= 0) skillCooltime = 0;
            if(skillCooltime > skillMaxCooltime) skillCooltime = skillMaxCooltime;
        }
    }
    public float skillMaxCooltime;
    public bool isCanUseSkill { get { return SkillCooltime == skillMaxCooltime; } }

    public virtual void UseSkill()
    {
        if (isCanUseSkill)
        {
            SkillEffect();
        }
    }

    public abstract void SkillEffect();
}
