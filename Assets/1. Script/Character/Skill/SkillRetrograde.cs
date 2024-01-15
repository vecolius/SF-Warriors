using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillRetrograde : SkillStractegy
{
    //public Stack<Vector3> returnPosStack = new Stack<Vector3>();
    public LinkedList<Vector3> timeTravelList = new LinkedList<Vector3>();
    int frameCount;
    public Player player;
    void Start()
    {
        player = GetComponent<Player>();
        skillMaxCooltime = 12.0f;
        SkillCooltime = skillMaxCooltime;
    //    StartCoroutine(RetrogradeCo());
    }

    void Update()
    {
        frameCount++;
        if (!player.isReturning && (frameCount%6==0))
        {
            timeTravelList.AddLast(transform.position);
            if(timeTravelList.Count > 90)
            {
                timeTravelList.RemoveFirst();
            }
            frameCount = 0;
        }
    }

    public override void SkillEffect()
    {
        SkillCooltime = 0;
        player.isReturning = true;
        StartCoroutine(TimeTravelCo());
        SoundManager.instance.SFXPlay("Retrograde", SoundManager.instance.SPXclips[5], player.transform);
    }

    IEnumerator TimeTravelCo()
    {

        for(int i=timeTravelList.Count-1;i>=0;i--)
        {
            transform.position = timeTravelList.ElementAt(i);
            yield return null;
        }
        player.isReturning = false;
        if (player.Hp < 60f)
            player.Hp += 30f;
        /*
        for(int i=timeTravelList.Count-1; i >= 0; i--)
        {
            //transform.position = Vector3.Lerp(transform.position, returnPosStack[i]);
            transform.position = Vector3.Lerp(transform.position, timeTravelList.Last.Value, 0.2f);
            yield return new WaitForSeconds(0.01f);
        }
        player.isReturning = false;
        */
    }
}
