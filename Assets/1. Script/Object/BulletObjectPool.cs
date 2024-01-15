using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour//SingleTon<BulletObjectPool>
{
    public static BulletObjectPool instance = null;
    public GameObject bulletPrefab; //���� GameObject
    public int initSize;                    //�̸� ������ ��
    public Queue<GameObject> setBulletsPool;//GameObject�� ���� ����
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
    
    //����� object�� ȣ��
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

    //object ���� & ����
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

    //object�� �ٽ� ����
    public void ReturnPool(GameObject returnObj)
    {
        returnObj.SetActive(false);
        returnObj.transform.SetParent(parantsObj.transform);
        setBulletsPool.Enqueue(returnObj);

    }
}
