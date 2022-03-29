using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] enemys;
    public TowerGameMgr gameMgr;
    public Vector3 oriPos;
    public string enemyName;
    public int enemyMaxCnt;
    public int enemyCnt = 0;
    public float curTime = 0;
    public float spawnTime = 0;
    float minSpawnTime = 0.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        //gameMgr.Upstage();
        enemyName = enemyPrefab.name;
        for (int i = 0; i < 80; i++)
        {
            enemys[i] = Instantiate(enemyPrefab);
            enemys[i].SetActive(false);
        }
        oriPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyName)
        {
            case "Enemy01":
                enemyMaxCnt = gameMgr.enemy01Cnt;
                break;
            case "Enemy02":
                enemyMaxCnt = gameMgr.enemy02Cnt;
                break;
            case "Enemy03":
                enemyMaxCnt = gameMgr.enemy03Cnt;
                break;
            case "Enemy04":
                enemyMaxCnt = gameMgr.enemy04Cnt;
                break;
            case "Enemy05":
                enemyMaxCnt = gameMgr.enemy05Cnt;
                break;
            default:
                break;
        }
        if (enemyMaxCnt != 0)
        {
            spawnTime = gameMgr.stagePlayTime / (float)enemyMaxCnt;
        }
        if (spawnTime < minSpawnTime)
        {
            spawnTime = minSpawnTime;
        }

        if (gameMgr.isStage == true)
        {
            if (enemyCnt < enemyMaxCnt)
            {
                curTime += Time.deltaTime;
                if (curTime > spawnTime)
                {

                    enemys[enemyCnt].transform.position = oriPos;
                    enemys[enemyCnt].SetActive(true);
                    
                    curTime = 0;
                    enemyCnt++;
                }
            }
        }
        else
        {
            enemyCnt = 0;
        }
        
        
    }
}
