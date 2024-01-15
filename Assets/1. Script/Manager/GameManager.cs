using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameManager : SingleTon<GameManager>
{
    [SerializeField]
    int point;
    public int Point
    {
        get { return point; }
        set { point = value; }
    }
    public GameObject playerObj;
    public Player player;

    public bool isClear = false;
    public bool IsTimeStop
    {
        get { return Time.timeScale == 0f; }
    }

    public void TheWorld(bool timeStop)
    {
        float time;
        if (timeStop) 
            time = 0f;
        else 
            time = 1f;
        Time.timeScale = time;
    }
    public void MouseCursorVisible(bool state)
    {
        Cursor.visible = state;
        if(state == true)
            Cursor.lockState = CursorLockMode.None;
        else if(state == false)
            Cursor.lockState = CursorLockMode.Locked;
    }

}
