using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingUI : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene("First");
    }
}
