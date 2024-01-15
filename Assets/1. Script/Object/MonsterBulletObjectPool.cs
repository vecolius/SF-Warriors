using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletObjectPool : MonoBehaviour
{
    public static MonsterBulletObjectPool instance = null;
    public GameObject bulletPrefab; //만들 GameObject
    public int initSize;                    //미리 생성할 양
    public Queue<GameObject> setBulletsPool;//GameObject를 담을 공간
    public GameObject parantsMonsterObj;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        setBulletsPool = new Queue<GameObject>();
        parantsMonsterObj = new GameObject("MonsterBulletPool-");
        AddPool(initSize);
    }

    //저장된 object를 호출
    public void PopObj(Vector3 pos, Quaternion rotate)
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
    }

    //object 생성 & 저장
    void AddPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temp = Instantiate(bulletPrefab);
            temp.SetActive(false);
            temp.transform.SetParent(parantsMonsterObj.transform);
            setBulletsPool.Enqueue(temp);
        }
    }

    //object를 다시 저장
    public void ReturnPool(GameObject returnObj)
    {
        returnObj.SetActive(false);
        returnObj.transform.SetParent(parantsMonsterObj.transform);
        setBulletsPool.Enqueue(returnObj);

    }
}
