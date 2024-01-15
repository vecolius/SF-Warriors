using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public void CloseButton()
    {
        GameManager.instance.TheWorld(false);
        UIManager.instance.menuUI.SetActive(false);
        GameManager.instance.MouseCursorVisible(false);
    }

    public void BgSoundSlider()
    {
        float value = UIManager.instance.bgSoundSlider.value;
        SoundManager.instance.BgSoundVolume(value);
    }

    public void SFXSlider()
    {
        float value = UIManager.instance.sfxSoundSlider.value;
        SoundManager.instance.SFXVolume(value);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("First");
    }
}
