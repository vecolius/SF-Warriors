using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance = null;
    public GameObject monsterObj;

    public bool isWaveStart = false;           //wave가 시작됐는지
    public bool isMonsterSpawn = false;     //wave가 시작되어 monster가 나왔는지

    int waveNumber;              //wave 단계
    public int WaveNumber
    {
        get { return waveNumber; }
        private set
        {
            waveNumber = value;
            if (waveNumber >= 4)
                waveNumber = 3;
        }
    }
    [SerializeField]float waveReadyTime;
    public float maxWaveReadyTime = 60f;    //최대 준비시간
    public float WaveReadyTime          //wave 준비시간
    {
        get
        {
            return waveReadyTime;
        }
        private set
        {
            waveReadyTime = value;
            if (waveReadyTime <= 0)
            {
                waveReadyTime = 0;
                isWaveStart = true;
            }
            else
                isWaveStart = false;
            if (waveReadyTime > maxWaveReadyTime)
                waveReadyTime = maxWaveReadyTime;
        }
    }
    int waveMonsterCount;           //wave 마다 나오는 monster 수
    public int monsterCount;        //wave에 현재 monster 수

    float minX = -65f, maxX = 80f, minZ = -70f, maxZ = 60;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        waveMonsterCount = 10;
        WaveSetting(1, maxWaveReadyTime, waveMonsterCount);
    }

    void Update()
    {
        WaveReadyTime -= Time.deltaTime;
        if (isWaveStart)
        {
            if (monsterCount <= 0)   //monster를 다 죽였을 경우,
            {
                if (WaveNumber >= 3)    //monster를 다 죽였는데, 그 wave가 3일 경우,
                {
                    GameManager.instance.isClear = true;
                    SceneManager.LoadScene("Ending");
                }
                else
                {
                    WaveNumber += 1;
                    WaveSetting(WaveNumber, maxWaveReadyTime, waveMonsterCount += 5);
                }
            }
            else
            {
                if (!isMonsterSpawn)                    //wave가 시작됐는데 monster가 spawn 안했으면
                {
                    MonsterSpawn(waveMonsterCount);
                }
            }
        }
    }

    void WaveSetting(int waveNum, float readyTime, int monsterCount)    //wave 준비 함수
    {
        isMonsterSpawn = false;
        WaveNumber = waveNum;
        WaveReadyTime = readyTime;
        this.monsterCount = monsterCount;
    }
    void MonsterSpawn(int _monsterSpawnCount)       //monster를 spawn하는 함수
    {
        isMonsterSpawn = true;
        for (int i = 0; i < _monsterSpawnCount; i++)
        {
            Instantiate(monsterObj, RandomSpawnPosition(), monsterObj.transform.rotation);
        }
    }
    Vector3 RandomSpawnPosition()    //spawn할 position을 가져오는 함수
    {
        float x, z;
        do
        {
            x = Random.Range(minX, maxX);
            z = Random.Range(minZ, maxZ);
        } while ( (x >= 10 && x <= 25) || (z >= -20 && z <= 10));
        return new Vector3(x, 0, z);
    }
}
