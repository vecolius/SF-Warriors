using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int damage;
    public float radius;
    public GameObject grenadeEffect;
    public LayerMask playerLayer;
    public LayerMask targetLayerMask;

    float timer;
    Vector3 startPos, endPos; 

    void Start()
    {
        startPos = transform.position;
        endPos = GameManager.instance.player.aimComponent.aimPos;
        StartCoroutine(MoveCo());
    }

    void Explosion()    //폭발
    {
        GameObject effectObj = Instantiate(grenadeEffect, transform.position, transform.rotation);
        effectObj.SetActive(true);
        SoundManager.instance.SFXPlay( "grenadeExplosion", SoundManager.instance.SPXclips[3], transform);
        Destroy(effectObj, 6.0f);
        Destroy(gameObject, 6.0f);
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, targetLayerMask);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].TryGetComponent(out IHitable hit))
                    hit.Hit(damage);
            }
        }
    }

    IEnumerator MoveCo()            //포물선의 위치로 날아가는 함수
    {
        timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            transform.position = Parabola(startPos, endPos, Vector3.Distance(startPos, endPos)/2, timer);
            yield return new WaitForEndOfFrame();
        }
    }
    Vector3 Parabola(Vector3 start, Vector3 end, float height, float time)      //포물선 구하는 공식
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;       //  y = -4ax^2 + 4ax + 0 = f(x)
        var mid = Vector3.Lerp(start, end, time);                                     //mid = x;
        return new Vector3(mid.x, f(time) + Mathf.Lerp(start.y, end.y, time), mid.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Explosion();
    }
}
