using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControll : MonoBehaviour
{
    public float atkCurTime;
    public float waitCurTime;
    public GameObject targetEnemy;
    public GameObject ArrowPrefab;
    public float curTime;
    public float coolTime;
    public TowerDetail towerDetail;
    public Animator animator;
    public GameObject effect;

    public enum TOWERSTATE { 
        IDLE = 0,
        ATTACK,
        NONE
    }
    public TOWERSTATE towerState;
    public TowerEnemyDetecting towerEnemyDetecting;


    // Start is called before the first frame update
    void Start()
    {
        towerState = TOWERSTATE.IDLE;
        towerEnemyDetecting = GetComponentInChildren<TowerEnemyDetecting>();
        towerDetail = GetComponent<TowerDetail>();
        coolTime = 2f;
        animator = transform.GetChild(2).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (towerState)
        {
            case TOWERSTATE.IDLE:
                atkCurTime = 0;
                if (towerEnemyDetecting.enemies.Count > 0 && targetEnemy == null)
                {
                    targetEnemy = towerEnemyDetecting.enemies[0];
                    
                    towerState = TOWERSTATE.ATTACK;
                }
                if (targetEnemy != null) towerState = TOWERSTATE.ATTACK;
                curTime += Time.deltaTime;
                if (curTime > coolTime)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    curTime = 0;
                }
                break;
            case TOWERSTATE.ATTACK:
                towerState = TOWERSTATE.ATTACK;
                if (towerEnemyDetecting.enemies.Count > 0 && targetEnemy == null)
                {
                    targetEnemy = towerEnemyDetecting.enemies[0];
                }
                curTime = 0;
                if (targetEnemy != null) // 전제조건. 타겟이 있을때만
                {
                    if (targetEnemy.activeSelf == false)
                    {
                        targetEnemy = null;
                        atkCurTime = 0;
                        return;
                    }
                    transform.LookAt(targetEnemy.transform); // 타겟에너미를 바라보게 LookAt
                    Vector3 dir = transform.localRotation.eulerAngles;
                    dir.x = 0;
                    transform.localRotation = Quaternion.Euler(dir);

                    atkCurTime += Time.deltaTime;
                    if (atkCurTime > towerDetail.atkspeed) // 어택!
                    {
                        atkCurTime = 0;
                        int tmpAtk = towerDetail.atk;
                        if (targetEnemy.GetComponent<EnemyDetail>().def > towerDetail.atk)
                        {
                            tmpAtk = 0;
                        }
                        else
                        {
                            tmpAtk -= targetEnemy.GetComponent<EnemyDetail>().def;
                        }
                        GameObject exEffect = Instantiate(effect) as GameObject;
                        exEffect.transform.position = targetEnemy.transform.position;
                        targetEnemy.GetComponent<EnemyDetail>().hp -= tmpAtk;
                        
                        towerState = TOWERSTATE.IDLE;
                        animator.SetInteger("towerstate", (int)towerState);

                    }
                }
                else
                {
                    towerState = TOWERSTATE.IDLE;        
                }
                break;
            case TOWERSTATE.NONE:
                curTime = 0;
                break;
            default:
                break;
        }
        animator.SetInteger("towerstate", (int)towerState);
    }
    
}
