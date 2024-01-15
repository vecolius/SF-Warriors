using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotSpeed = 200;
    const int maxRot = 60;
    const int minRot = -60;
    float mx, my;       //마우스 x,y 각도

    void Update()
    {
        CameraRote();
    }
    void CameraRote()
    {
        float h = Input.GetAxis("Mouse X");//마우스 X 축 감지
        float v = Input.GetAxis("Mouse Y");//마우스 y 축 감지

        mx += h * rotSpeed * Time.deltaTime;
        my += v * rotSpeed * Time.deltaTime;

        //위 각도가 최대각도 이상이면 최대각도로 설정
        //아래를 보는 각도가  최저각도 이하면 최저각도로 설정
        my = Mathf.Clamp(my, minRot, maxRot);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
