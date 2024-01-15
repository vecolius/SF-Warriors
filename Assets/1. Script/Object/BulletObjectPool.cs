using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour//SingleTon<BulletObjectPool>
{
    public static BulletObjectPool instance = null;
    public GameObject bulletPrefab; //만들 GameObject
    public int initSize;                    //미리 생성할 양
    public Queue<GameObject> setBulletsPool;//GameObject를 담을 공간
    public GameObject parantsObj;

    public LaserBullet bulletComponent;
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        bulletComponent = bulletPrefab.GetComponent<LaserBullet>();
        setBulletsPool = new Queue<GameObject>();
        parantsObj = new GameObject("BulletPool-");
        AddPool(initSize);
    }
    
    //저장된 object를 호출
    public GameObject PopObj(Vector3 pos, Quaternion rotate)
    {
        if (setBulletsPool.Count <= 0)
        {
            AddPool(initSize / 3);
        }
        GameObject obj = setBulletsPool.Dequeue();
        obj.transform.position = pos;
        obj.transform.rotation = rotate;
        obj.transform.SetParent(null);
        obj.SetActive(true);
        return obj;
    } 

    //object 생성 & 저장
    void AddPool(int count)
    {
        for(int i=0; i<count; i++)
        {
            GameObject temp = Instantiate(bulletPrefab);
            temp.SetActive(false);
            temp.transform.SetParent(parantsObj.transform);
            setBulletsPool.Enqueue(temp);
        }
    }

    //object를 다시 저장
    public void ReturnPool(GameObject returnObj)
    {
        returnObj.SetActive(false);
        returnObj.transform.SetParent(parantsObj.transform);
        setBulletsPool.Enqueue(returnObj);

    }
}
