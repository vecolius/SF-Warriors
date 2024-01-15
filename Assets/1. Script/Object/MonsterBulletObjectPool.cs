using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletObjectPool : MonoBehaviour
{
    public static MonsterBulletObjectPool instance = null;
    public GameObject bulletPrefab; //���� GameObject
    public int initSize;                    //�̸� ������ ��
    public Queue<GameObject> setBulletsPool;//GameObject�� ���� ����
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

    //����� object�� ȣ��
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

    //object ���� & ����
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

    //object�� �ٽ� ����
    public void ReturnPool(GameObject returnObj)
    {
        returnObj.SetActive(false);
        returnObj.transform.SetParent(parantsMonsterObj.transform);
        setBulletsPool.Enqueue(returnObj);

    }
}
