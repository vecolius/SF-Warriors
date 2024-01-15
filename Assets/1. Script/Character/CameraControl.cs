using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotSpeed = 200;
    const int maxRot = 60;
    const int minRot = -60;
    float mx, my;       //���콺 x,y ����

    void Update()
    {
        CameraRote();
    }
    void CameraRote()
    {
        float h = Input.GetAxis("Mouse X");//���콺 X �� ����
        float v = Input.GetAxis("Mouse Y");//���콺 y �� ����

        mx += h * rotSpeed * Time.deltaTime;
        my += v * rotSpeed * Time.deltaTime;

        //�� ������ �ִ밢�� �̻��̸� �ִ밢���� ����
        //�Ʒ��� ���� ������  �������� ���ϸ� ���������� ����
        my = Mathf.Clamp(my, minRot, maxRot);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
