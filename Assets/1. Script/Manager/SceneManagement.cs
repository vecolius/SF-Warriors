using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : SingleTon<SceneManagement>
{
    void Start()
    {
        SceneManager.sceneLoaded += SceneChanger;
    }
    public void SceneChanger(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "First")
        {
            GameManager.instance.playerObj = null;
            GameManager.instance.MouseCursorVisible(true);
            UIManager.instance.FirstSceneInit();
            SoundManager.instance.BgSoundPlay(SoundManager.instance.bgClips[0]);
        }
        else if (scene.name == "InGame")
        {
            GameManager.instance.Point = 0;
            GameManager.instance.playerObj = GameObject.FindObjectOfType<Player>().gameObject;
            GameManager.instance.player = GameManager.instance.playerObj.GetComponent<Player>();
            GameManager.instance.MouseCursorVisible(false);
            UIManager.instance.InGameSceneInit();
            SoundManager.instance.BgSoundPlay(SoundManager.instance.bgClips[1]);
        }
        else if (scene.name == "Ending")
        {
            GameManager.instance.playerObj = null;
            GameManager.instance.MouseCursorVisible(true);
            UIManager.instance.EndingSceneInit();
            SoundManager.instance.BgSoundPlay(SoundManager.instance.bgClips[0]);
        }
    }
}
