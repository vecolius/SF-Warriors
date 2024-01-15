using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveComponent : MonoBehaviour
{
    public GameObject layerMaskSencer;         //layer를 검출할 중심 위치
    [SerializeField] LayerMask targetLayerMask;//검출할 대상 layer
    [SerializeField] float radius;                     //인식 범위
    [SerializeField] float maxDistance;            //대상사이에 장애물 있는지 검사하는 거리

    [SerializeField] bool isRangeDetection;     //인식범위에 대상이 있는지
    public bool IsRangeDetection
    {
        get { return isRangeDetection; }
    }
    [SerializeField] bool isRayDetection;        //대상과 사이에 장애물 없음
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
    public bool IsDection                        //추적 가능 상태 - 장애물 없음 & 타겟감지
    {
        get { return IsRayDetection && IsRangeDetection; } 
    }
    bool isRayinTarget;
    public bool IsRayinTarget                   //Raycast에 물체 닿음
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
            if(isRayinTarget = Physics.Raycast(layerMaskSencer.transform.position, direction, out hit, maxDistance))    //Raycast에 물체가 들어옴
            {
                isRayDetection = CheckInLayerMask(hit.collider.gameObject.layer);
                if(isRayDetection)                                                                   //물체의 LayerMask가 대상과 일치하면
                {
                    LastDetectivePos = hit.transform.position;                                //대상의 position값 저장
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
