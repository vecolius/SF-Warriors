using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    public Color hpColor;
    public Image hpBar;
    public TextMeshProUGUI hpText;
    public Character characterComponent;
    public Camera mainCamera;
    void Start()
    {
        hpBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hpText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        hpColor.a = 255f;
        hpBar.color = hpColor;
        characterComponent = transform.root.GetComponent<Character>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        hpBar.fillAmount = characterComponent.Hp / characterComponent.maxHp;
        hpText.text = (hpBar.fillAmount*100f).ToString()+"%";
    }
}
