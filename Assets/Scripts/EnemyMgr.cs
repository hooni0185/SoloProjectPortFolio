using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMgr : MonoBehaviour
{
    public enum ENEMYSTATE { 
        MOVE,
        STURN,
        DEAD
    }
    public ENEMYSTATE enemyState;


    public EnemyDetail enemyDetail;
    public string enemyName;
    public TowerGameMgr gameMgr;
    public Animator animator;
    int damage;
    public float curTime;
    public float deadTime;
    AudioSource audioSource;
    public AudioClip enemyDie;

    private void Start()
    {
        enemyState = ENEMYSTATE.MOVE;
        gameMgr = GameObject.Find("GameMgr").GetComponent<TowerGameMgr>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        enemyDetail = GetComponent<EnemyDetail>();
        audioSource = GetComponent<AudioSource>();
        enemyName = gameObject.name;
        deadTime = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (enemyDetail.hp < 0.1f)
        {
            if (enemyDetail.hp != 0)
            {
                audioSource.PlayOneShot(enemyDie);
            }
            enemyDetail.hp = 0;
            enemyState = ENEMYSTATE.DEAD;
        }
        else
        {
            enemyState = ENEMYSTATE.MOVE;
        }
        animator.SetInteger("enemystate", (int)enemyState);

        if (enemyState == ENEMYSTATE.DEAD)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            curTime += Time.deltaTime;
            if (curTime > deadTime)
            {
                switch (enemyName)
                {
                    case "Enemy01(Clone)":
                        gameMgr.gold += 1;
                        gameMgr.score += 10;
                        break;
                    case "Enemy02(Clone)":
                        gameMgr.gold += 3;
                        gameMgr.score += 30;
                        break;
                    case "Enemy03(Clone)":
                        gameMgr.gold += 5;
                        gameMgr.score += 50;
                        break;
                    case "Enemy04(Clone)":
                        gameMgr.gold += 10;
                        gameMgr.score += 100;
                        break;
                    case "Enemy05(Clone)":
                        gameMgr.gold += 20;
                        gameMgr.score += 200;
                        break;
                    default:
                        break;
                }
                curTime = 0;
                
                gameObject.SetActive(false);
                gameObject.GetComponent<EnemyDetail>().hp = gameObject.GetComponent<EnemyDetail>().orihp;
                gameObject.GetComponent<EnemyMove>().nodeCnt = 0;

            }
        }
    }
}