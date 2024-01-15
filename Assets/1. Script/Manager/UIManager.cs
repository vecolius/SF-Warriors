using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>
{
    float barSpeed;
    public Player player;

    public FirstSceneUI firstSceneUI;
    public InGameUI inGameUI;
    public EndingUI endingUI;
    [Header("FirstScene")]
    bool isFirstScene;
    public TextMeshProUGUI pointText;
    public Button playButton;
    public Button exitButton;
    [Header("InGameScene")]
    public bool isInGameScene;
    public GameObject hpBar;
    public GameObject skill1;
    public GameObject skill2;
    public TextMeshProUGUI bulletCountText;
    public GameObject reloadBar;
    public GameObject aimImage;
    public TextMeshProUGUI WaveCount;
    public GameObject nextWaveTime;
    public TextMeshProUGUI gamePoint;
    public GameObject menuUI;
    public Slider bgSoundSlider;
    public Slider sfxSoundSlider;
    [Header("EndingScene")]
    bool isEndingScene;
    public TextMeshProUGUI endingText;
    public TextMeshProUGUI endPointText;
    public Button backButton;

    void Start()
    {
        FirstSceneInit();
        firstSceneUI.FirstScenePoint(0);
    }
    void Update()
    {
        if(isFirstScene)
        {
            SceneText(pointText, "POINT : "+GameManager.instance.Point.ToString());
        }
        if (isInGameScene)
        {
            barSpeed = 3.0f * Time.deltaTime;
            InGameSceneReloadBar();
            InGameSceneFillAmountMathf(hpBar.GetComponent<Image>(), player.Hp, player.maxHp);
            InGameSceneNextWaveTime();
            InGameSceneSkillCoolTime(skill1.transform.GetChild(0).GetComponent<Image>(), player.skillStractegies[0].SkillCooltime, player.skillStractegies[0].skillMaxCooltime);
            InGameSceneSkillCoolTime(skill2.transform.GetChild(0).GetComponent<Image>(), player.skillStractegies[1].SkillCooltime, player.skillStractegies[1].skillMaxCooltime);
            SceneText(WaveCount, "Wave " + WaveManager.instance.WaveNumber);
            SceneText(gamePoint, "POINT : " + GameManager.instance.Point.ToString());
        }
        if(isEndingScene)
        {
            EndingSceneEndingText();
        }
    }
    #region SceneInit
    public void FirstSceneInit()
    {
        isFirstScene = true;
        isInGameScene = false;
        isEndingScene = false;
        pointText = GameObject.Find("PointText").GetComponent<TextMeshProUGUI>();
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
    }
    public void InGameSceneInit()
    {
        isInGameScene = true;
        isFirstScene = false;
        isEndingScene= false;
        Transform uiCanvas = GameObject.Find("Canvas").transform;
        hpBar = uiCanvas.GetChild(0).gameObject;
        skill1 = uiCanvas.GetChild(1).gameObject;
        skill2 = uiCanvas.GetChild(2).gameObject;
        bulletCountText = uiCanvas.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        reloadBar = uiCanvas.GetChild(4).gameObject;
        aimImage = uiCanvas.GetChild(5).gameObject;
        WaveCount = uiCanvas.GetChild(6).GetComponent<TextMeshProUGUI>();
        nextWaveTime = uiCanvas.GetChild(7).gameObject;
        gamePoint = uiCanvas.GetChild(8).GetComponent<TextMeshProUGUI>();
        menuUI = uiCanvas.GetChild(10).gameObject;
        bgSoundSlider = menuUI.transform.GetChild(1).GetComponent<Slider>();
        sfxSoundSlider = menuUI.transform.GetChild(2).GetComponent<Slider>();
        player = GameManager.instance.player;
    }
    public void EndingSceneInit()
    {
        isEndingScene = true;
        isFirstScene = false;
        isInGameScene = false;
        Transform endCanvas = GameObject.Find("Canvas").transform;
        endingText = endCanvas.GetChild(0).GetComponent<TextMeshProUGUI>();
        endPointText = endCanvas.GetChild(1).GetComponent<TextMeshProUGUI>();
        backButton = endCanvas.GetChild(2).GetComponent<Button>();
    }
    #endregion

    void InGameSceneReloadBar() //InGameScene 재장전 중 재장전바
    {
        //bulletCountText.text =  player.weapon.BulletCount.ToString() + " / " + player.TotalBulletCount.ToString();
        SceneText(bulletCountText, player.weapon.BulletCount.ToString() + " / " + player.TotalBulletCount.ToString());
        if (player.isReloading)
        {
            reloadBar.SetActive(true);
            if (player.ani.GetCurrentAnimatorStateInfo(1).IsName("Reload") && player.ani.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
            {
                reloadBar.transform.GetChild(0).GetComponent<Image>().fillAmount = player.ani.GetCurrentAnimatorStateInfo(1).normalizedTime;
            }
            else //재장전 animation이 끝났을 경우
            {
                reloadBar.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
            }
        }
        else
            reloadBar.SetActive(false);
    }
    void InGameSceneNextWaveTime()  //InGameScene 안의 nextWaveTime의 상태
    {
        if(WaveManager.instance.WaveReadyTime == 0f)
            nextWaveTime.SetActive(false);
        else
            nextWaveTime.SetActive(true);
        InGameSceneFillAmountMathf(nextWaveTime.GetComponent<Image>(), WaveManager.instance.WaveReadyTime, WaveManager.instance.maxWaveReadyTime);
    }
    void InGameSceneSkillCoolTime(Image skillImage, float coolTime, float maxCoolTime)  //skill coolTime UI
    {
        skillImage.fillAmount = coolTime / maxCoolTime;
    }
    void InGameSceneFillAmountMathf(Image bar, float result, float maxResult)
    {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, result / maxResult, barSpeed);
    }           //barSpeed가 필요한 bar UI
    void SceneText(TextMeshProUGUI textUI, string info)         //Scene 안의 TextMEshProUGUI 
    {
        textUI.text = info;
    }

    void EndingSceneEndingText()
    {
        if (GameManager.instance.isClear)
            endingText.text = "Congratulations!";
        else
            endingText.text = "YOU DIED";

        SceneText(endPointText, "Point : " + GameManager.instance.Point.ToString());
    }
    
}
