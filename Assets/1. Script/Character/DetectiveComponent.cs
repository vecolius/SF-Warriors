using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveComponent : MonoBehaviour
{
    public GameObject layerMaskSencer;         //layer�� ������ �߽� ��ġ
    [SerializeField] LayerMask targetLayerMask;//������ ��� layer
    [SerializeField] float radius;                     //�ν� ����
    [SerializeField] float maxDistance;            //�����̿� ��ֹ� �ִ��� �˻��ϴ� �Ÿ�

    [SerializeField] bool isRangeDetection;     //�νĹ����� ����� �ִ���
    public bool IsRangeDetection
    {
        get { return isRangeDetection; }
    }
    [SerializeField] bool isRayDetection;        //���� ���̿� ��ֹ� ����
    public bool IsRayDetection{
        get{ return IsRayinTarget && isRayDetection; }
    }
    public Vector3 LastDetectivePos
    {
        get; private set;
    }
    public GameObject targetObject;
    [SerializeField]public GameObject TargetObject
    {
        get { return targetObject; } private set { targetObject = value; }
    }
    public bool IsDection                        //���� ���� ���� - ��ֹ� ���� & Ÿ�ٰ���
    {
        get { return IsRayDetection && IsRangeDetection; } 
    }
    bool isRayinTarget;
    public bool IsRayinTarget                   //Raycast�� ��ü ����
    {
        get { return isRayinTarget; }
    }

    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(layerMaskSencer.transform.position, radius, targetLayerMask);
        isRangeDetection = (bool)(cols.Length > 0);

        if (IsRangeDetection)
        {
            RaycastHit hit;
            Vector3 direction = ((cols[0].transform.position) - layerMaskSencer.transform.position).normalized;
            Debug.DrawLine(layerMaskSencer.transform.position, layerMaskSencer.transform.position + (direction * maxDistance), Color.blue);
            TargetObject = cols[0].gameObject;
            if(isRayinTarget = Physics.Raycast(layerMaskSencer.transform.position, direction, out hit, maxDistance))    //Raycast�� ��ü�� ����
            {
                isRayDetection = CheckInLayerMask(hit.collider.gameObject.layer);
                if(isRayDetection)                                                                   //��ü�� LayerMask�� ���� ��ġ�ϸ�
                {
                    LastDetectivePos = hit.transform.position;                                //����� position�� ����
                    Debug.DrawLine(layerMaskSencer.transform.position, layerMaskSencer.transform.position + (direction * maxDistance), Color.red);
                }
            }
        }
    }

    bool CheckInLayerMask(int layerIndex)
    {
        return (targetLayerMask & (1 << layerIndex)) != 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(layerMaskSencer.transform.position, radius);
    }
}
