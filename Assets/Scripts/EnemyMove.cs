using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    public Transform[] nodePoint;
    public TowerGameMgr gameMgr;
    public int nodeMaxCnt;
    public int nodeCnt = 0;
    public EnemyDetail enemyDetail;
    public EnemyMgr enemyMgr;

    public Camera mainCamera;
  
  
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameMgr = GameObject.Find("GameMgr").GetComponent<TowerGameMgr>();
        enemyDetail = GetComponent<EnemyDetail>();
        enemyMgr = GetComponent<EnemyMgr>();
        nodeMaxCnt = nodePoint.Length;
        for (int i = 0; i < nodeMaxCnt; i++)
        {
            nodePoint[i] = GameObject.Find("Point" + i).GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == false)
        { 
            nodeCnt = 0;
        }
        if (enemyMgr.enemyState == EnemyMgr.ENEMYSTATE.MOVE)
        {
            if (nodeCnt < nodeMaxCnt)
            {
                if (Vector3.Distance(nodePoint[nodeCnt].position, transform.position) <= 0.01f)
                {
                    nodeCnt++;
                }
                else // 이동.
                {
                    transform.position = Vector3.MoveTowards(transform.position, nodePoint[nodeCnt].position, enemyDetail.ms * Time.deltaTime);
                    transform.LookAt(nodePoint[nodeCnt].position);
                }
            }
            else // 도착 포인트에 갔을 경우
            {
                gameMgr.hp -= 1;

                if (gameMgr.hp <= 0)
                {
                    gameMgr.isGameOver = true;
                    gameMgr.hp = 0;
                }
                gameObject.SetActive(false);
                gameObject.GetComponent<EnemyDetail>().hp = gameObject.GetComponent<EnemyDetail>().orihp;
                gameObject.GetComponent<EnemyMove>().nodeCnt = 0;

                mainCamera.transform.DOShakePosition(0.5f, new Vector3(1, 0, 1), 90, 0);

            }
        }
  
    }
}
