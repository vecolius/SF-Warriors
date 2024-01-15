using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    public Vector3 aimPos;
    const float aimHitRange = 50f;
    public GameObject testAimObj;

    void Update()
    {
        RaycastHit aimHit;
        Debug.DrawLine(transform.position, transform.forward * aimHitRange, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out aimHit, aimHitRange))
        {
            aimPos = aimHit.point;
        }
        else
        {
            aimPos = this.transform.position + transform.forward.normalized * 20f;
        }
        testAimObj.transform.position = aimPos;
    }
}
