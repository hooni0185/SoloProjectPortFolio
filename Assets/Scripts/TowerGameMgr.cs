using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerGameMgr : MonoBehaviour
{
    public int hp;
    public int gold;
    public int score;
    public string playerName;
    public bool isPause;
    public bool isGameOver;
    public int stagelevel;
    public float curTime;
    public float stagePlayTime;
    
    public int enemy01Cnt;
    public int enemy02Cnt;
    public int enemy03Cnt;
    public int enemy04Cnt;
    public int enemy05Cnt;

    public int stageClearGold;
    public int stageClearScore;

    public bool isStage;

    public GameObject panel_GameOver;
    public GameObject panel_NonCreate;
    public GameObject image_TowerDetail;

    public Text text_Gold;
    public Text text_Heart;
    public Text text_Stage;

    public AudioMgr audioMgr;
    public Button BtnStage;
    public GameObject BtnPause;

    void Start()
    {
        stagelevel = 0;
        hp = 10;
        gold = 250;
        isPause = false;
        isGameOver = false;
        stagePlayTime = 0;

        enemy01Cnt = enemy02Cnt = enemy03Cnt = enemy04Cnt = enemy05Cnt = 0;
        stageClearGold = 0;
        stageClearScore = 0;
        text_Gold = GameObject.Find("Text_Gold").GetComponent<Text>();
        text_Heart = GameObject.Find("Text_Heart").GetComponent<Text>();
        text_Stage = GameObject.Find("Text_StageLevel").GetComponent<Text>();
        audioMgr = GameObject.Find("AudioMgr").GetComponent<AudioMgr>();

        BtnPause.SetActive(false);
    }
    private void Update()
    {
        text_Gold.text = gold.ToString();
        text_Heart.text = hp.ToString();
        text_Stage.text = "STAGE : " + stagelevel;
        if (isGameOver == true)
        {
            panel_GameOver.SetActive(true);
            Time.timeScale = 0.001f;
        }

        if (isStage == true)
        {
            audioMgr.audioSource.clip = audioMgr.audioClip02;
            audioMgr.audioPlay();
            curTime += Time.deltaTime;
            if (curTime > stagePlayTime)
            {
                if (GameObject.FindWithTag("Enemy") == false)
                {
                    isStage = false;
                    curTime = 0;
                    gold += stageClearGold;
                    score += stageClearScore;
                    audioMgr.audioSource.clip = audioMgr.audioClip01;
                    audioMgr.audioPlay();
                    if (BtnPause.activeSelf == true)
                        BtnPause.SetActive(false);
                }
                
            }
        }
        
    }

    public void Upstage() {  // 스테이지 시작 버튼 누를때 호출. onClick()
        if (stagelevel < 5)
        {
            stagelevel++;
        }
        switch (stagelevel)
        {
            case 1:
                enemy01Cnt = 30;
                stageClearGold = 100;
                stageClearScore = 1000;
                stagePlayTime = 30f;
                break;
            case 2:
                enemy01Cnt = 40;
                enemy02Cnt = 10;
                stageClearGold = 250;
                stageClearScore = 2500;
                stagePlayTime = 30f;
                break;
            case 3:
                enemy01Cnt = 60;
                enemy02Cnt = 20;
                enemy03Cnt = 10;
                stageClearGold = 400;
                stageClearScore = 4000;
                stagePlayTime = 60f;
                break;
            case 4:
                enemy01Cnt = 80;
                enemy02Cnt = 40;
                enemy03Cnt = 30;
                enemy04Cnt = 10;
                stageClearGold = 600;
                stageClearScore = 6000;
                stagePlayTime = 60f;
                break;
            case 5:
                enemy01Cnt = 80;
                enemy02Cnt = 40;
                enemy03Cnt = 30;
                enemy04Cnt = 20;
                enemy05Cnt = 10;
                stageClearGold = 1000;
                stageClearScore = 10000;
                stagePlayTime = 90f;
                break;
            default:
                break;
        }
    }
    public void isStageEnemy()
    {
        if (isStage == false && !GameObject.FindWithTag("Enemy")) //스테이지 진행중이 아닌데 에너미가 없다는건 게임 진행중이 아님. 이때 버튼이 눌리면 게임 실행하도록
        {
            isStage = true;
            Upstage();
        }
    }

    public void GameRestart() // 버튼 온클릭
    {
        SceneManager.LoadScene("TowerMain");
        Time.timeScale = 1f;
    }
    public void GoTitle()
    {
        SceneManager.LoadScene("TowerIntro");
        Time.timeScale = 1f;
    }

}
