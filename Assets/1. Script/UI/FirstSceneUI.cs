using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstSceneUI : MonoBehaviour
{
    public void FirstScenePoint(int _point)
    {
        UIManager.instance.pointText.text = "POINT : " + _point.ToString();
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("InGame");
    }
    public void ExitButton()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit(); //<- 빌드했을때 사용
    }
}
