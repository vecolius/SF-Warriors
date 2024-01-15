using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTest : MonoBehaviour
{
    public GameObject prefab;
    public GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            temp = ObjectPoolManager.instance.PopObj(prefab,transform.position,transform.rotation);
            Invoke("Retrun", 2);
        }
    }

    void Retrun()
    {
        ObjectPoolManager.instance.ReturnPool(temp);
    }
}

