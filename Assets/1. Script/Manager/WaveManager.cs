using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance = null;
    public GameObject monsterObj;

    public bool isWaveStart = false;           //wave�� ���۵ƴ���
    public bool isMonsterSpawn = false;     //wave�� ���۵Ǿ� monster�� ���Դ���

    int waveNumber;              //wave �ܰ�
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
    public float maxWaveReadyTime = 60f;    //�ִ� �غ�ð�
    public float WaveReadyTime          //wave �غ�ð�
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
    int waveMonsterCount;           //wave ���� ������ monster ��
    public int monsterCount;        //wave�� ���� monster ��

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
            if (monsterCount <= 0)   //monster�� �� �׿��� ���,
            {
                if (WaveNumber >= 3)    //monster�� �� �׿��µ�, �� wave�� 3�� ���,
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
                if (!isMonsterSpawn)                    //wave�� ���۵ƴµ� monster�� spawn ��������
                {
                    MonsterSpawn(waveMonsterCount);
                }
            }
        }
    }

    void WaveSetting(int waveNum, float readyTime, int monsterCount)    //wave �غ� �Լ�
    {
        isMonsterSpawn = false;
        WaveNumber = waveNum;
        WaveReadyTime = readyTime;
        this.monsterCount = monsterCount;
    }
    void MonsterSpawn(int _monsterSpawnCount)       //monster�� spawn�ϴ� �Լ�
    {
        isMonsterSpawn = true;
        for (int i = 0; i < _monsterSpawnCount; i++)
        {
            Instantiate(monsterObj, RandomSpawnPosition(), monsterObj.transform.rotation);
        }
    }
    Vector3 RandomSpawnPosition()    //spawn�� position�� �������� �Լ�
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
