using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RayTower : MonoBehaviour
{
    public Camera mainCamera;
    RaycastHit hit;
    Ray ray;

    public GameObject image_TowerDetail;
    public GameObject image_Obstacle;
    public GameObject image_Pause;
    public Button[] buttonTower;
    public GameObject[] towerPrefabs;
    public GameObject[] towerModels;
    public bool isTowerCreate = false;
    public int createTowerNum;
    public int createTowerCost;
    int oriCost;
    public GameObject tempObject;
    public Text towername;
    public Text textBefore;
    public Text towercost;
    public GameObject moreGold;

    public TowerGameMgr gameMgr;
    public Button buttonUpgrade;
    public Image levelImage01;
    public Image levelImage02;
    public Image levelImage03;
    public GameObject image_tower;
    RectTransform rt;
    RectTransform rt2;
    RectTransform rt3;
    public bool isDetail;
    public bool isObstacle;
    // Start is called before the first frame update
    void Start()
    {
        createTowerCost = 0;
        gameMgr = GameObject.Find("GameMgr").GetComponent<TowerGameMgr>();
        for (int i = 0; i < 3; i++)
        {
            towerModels[i].SetActive(false);
        }
        rt = image_tower.GetComponent<RectTransform>();
        rt2 = image_TowerDetail.GetComponent<RectTransform>();
        rt3 = image_Obstacle.GetComponent<RectTransform>();
        isDetail = false;
        DoMoveXRight();
        DoMoveYDownObstacle();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (image_Pause.activeSelf == true)
        {
            return;
        }
        if (isDetail == true || isObstacle == true)
        {
            return;
        }
  
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (isTowerCreate == true)
            {
                towerModels[createTowerNum].SetActive(true);
                towerModels[createTowerNum].transform.position = hit.point;
                DoMoveYDown();
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    IdleTower();
                    DoMoveYUp();
                }
            }
                
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "TowerBlock":
                        if (isObstacle == true) isObstacle = false;
                        DoMoveYDownObstacle();
                        tempObject = hit.collider.gameObject;
                        if (isTowerCreate == true) // Buy버튼을 눌렀을 때에만 건설 가능.
                        {
                            if (towerPrefabs[createTowerNum] != null) // 타워 프리팹이 있으면 건설.
                            {
                                if (gameMgr.gold >= createTowerCost)
                                {
                                    TowerCreate();
                                    DoMoveYUp();
                                }
                                else
                                {
                                    moreGold.SetActive(true);
                                    DoMoveXRight();
                                    DoMoveYUp();
                                                
                                }
                                IdleTower();
                            }
                        }
                        break;
                    case "NoneBlock":
                        IdleTower();
                        break;
                    case "Tower":
                        IdleTower();
                        tempObject = hit.collider.gameObject;
                        if (isObstacle == true) isObstacle = false;
                        DoMoveYDownObstacle();

                        if (isDetail == false)
                        {
                            DoMoveXLeft();
                            tempObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
                            DoMoveYDown();
                            TowerDetailPop();
                        }
                        else
                        {
                            DoMoveXRight();
                            DoMoveYUp();
                            tempObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                        }

                        break;
                    case "Enemy":
                        IdleTower();
                        break;
                    case "Obstacle":
                        IdleTower();
                        if (isDetail == true)
                        {
                            isDetail = false;
                            tempObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                        }
                        tempObject = hit.collider.gameObject;
                        DoMoveYDown();
                        DoMoveYUpObstacle();
                        isObstacle = true;
                        break;
                    default:
                        break;
                }
            }
        }
              
    }
    public void TowerDetailPop()
    {
        int atk; float range, atkspd;
        atk = tempObject.transform.GetComponent<TowerDetail>().atk;
        range = tempObject.transform.GetComponent<TowerDetail>().range;
        atkspd = tempObject.transform.GetComponent<TowerDetail>().atkspeed;
        textBefore.text = "ATK : "+ atk + "\n" + "RANGE : " + range + "\n" + "AS : " + atkspd;

        int tempLevel = tempObject.transform.GetComponent<TowerDetail>().level;
        if (tempLevel == 1)
        {
            levelImage01.enabled = true;
            levelImage02.enabled = false;
            levelImage03.enabled = false;
        }
        else if (tempLevel == 2)
        {
            levelImage01.enabled = false;
            levelImage02.enabled = true;
            levelImage03.enabled = false;
        }
        else if (tempLevel == 3)
        {
            levelImage01.enabled = false;
            levelImage02.enabled = false;
            levelImage03.enabled = true;
        }

        if (tempLevel < 3)
        {
            buttonUpgrade.enabled = true;
        }
        else
        {
            buttonUpgrade.enabled = false;
        }
        towercost.text = tempObject.transform.GetComponent<TowerDetail>().cost.ToString();
        switch (tempObject.name) {
            case "Tower01(Clone)":
                towername.text = "SwordMan";
                break;
            case "Tower02(Clone)":
                towername.text = "Archar";
                break;
            case "Tower03(Clone)":
                towername.text = "DoudleSword";
                break;
            case "Tower04(Clone)":
                towername.text = "미정";
                break;
            case "Tower05(Clone)":
                towername.text = "미정";
                break;
            case "Tower06(Clone)":
                break;
            default:
                break;
        }

        //Debug.Log("타워디테일!!" + createTowerCost);
    }
    public void TowerUpgrade() // upgrade버튼 누르면 팝업. // 골드 소모하게
    {
        if (gameMgr.gold >= tempObject.transform.GetComponent<TowerDetail>().cost)
        {
            if (tempObject.transform.GetComponent<TowerDetail>().level >= 3)
            {
                return;
            }
            else
            {
                if (gameMgr.gold < tempObject.transform.GetComponent<TowerDetail>().cost)
                {
                    moreGold.SetActive(true);
                    DoMoveXRight();
                    DoMoveYUp();
                }
                else
                {
                    gameMgr.gold -= tempObject.transform.GetComponent<TowerDetail>().cost;
                    oriCost += tempObject.transform.GetComponent<TowerDetail>().cost;
                    tempObject.transform.GetComponent<TowerDetail>().level += 1;
                    tempObject.transform.GetComponent<TowerDetail>().atk += tempObject.transform.GetComponent<TowerDetail>().upatk;
                    tempObject.transform.GetComponent<TowerDetail>().atkspeed *= tempObject.transform.GetComponent<TowerDetail>().upspeed_coefficient;
                    tempObject.transform.GetComponent<TowerDetail>().range *= tempObject.transform.GetComponent<TowerDetail>().uprange_coefficient;
                    tempObject.transform.GetChild(1).GetComponent<Transform>().localScale *= tempObject.transform.GetComponent<TowerDetail>().uprange_coefficient;
                }
            }
        }
        else
        {
            moreGold.SetActive(true);
        }

    }
    
    public void TowerCreate()
    {
        GameObject tower = Instantiate(towerPrefabs[createTowerNum]) as GameObject;
        tower.transform.position = tempObject.transform.position + new Vector3(0, tempObject.transform.localScale.y + 0.5f, 0);
        tempObject = tower;
        oriCost = createTowerCost;
        gameMgr.gold -= createTowerCost;
    }

    public void ObstacleDestroy()
    {
        if (gameMgr.gold - 20 >= 0)
        {
            gameMgr.gold -= 20;//골드 차감
            tempObject.SetActive(false);
        }
        else
        {
            moreGold.SetActive(true);
        }
    }

    public void TowerDestroy()
    {
        gameMgr.gold += (oriCost / 10 * 6);// 골드 60% 환급
        Destroy(tempObject);
        DoMoveXRight();
    }
    private void Awake()
    {
        buttonTower[0].onClick.AddListener(DoButton00);
        buttonTower[1].onClick.AddListener(DoButton01);
        buttonTower[2].onClick.AddListener(DoButton02);
        buttonTower[3].onClick.AddListener(DoButton03);
        buttonTower[4].onClick.AddListener(DoButton04);
        buttonTower[5].onClick.AddListener(DoButton05);
        buttonUpgrade.onClick.AddListener(TowerUpgrade);
        buttonUpgrade.onClick.AddListener(TowerDetailPop);
    }

    public void DoButton00()
    {
        tempObject = towerPrefabs[0];
        isTowerCreate = true;
        createTowerNum = 0;
        createTowerCost = 50;
    }
    public void DoButton01()
    {
        tempObject = towerPrefabs[1];
        isTowerCreate = true;
        createTowerNum = 1;
        createTowerCost = 100;

    }
    public void DoButton02()
    {
        tempObject = towerPrefabs[2];
        isTowerCreate = true;
        createTowerNum = 2;
        createTowerCost = 150;
    }
    public void DoButton03()
    {
        tempObject = towerPrefabs[3];
        isTowerCreate = true;
        createTowerNum = 3;
        createTowerCost = 200;
    }
    public void DoButton04()
    {
        tempObject = towerPrefabs[4];
        isTowerCreate = true;
        createTowerNum = 4;
        createTowerCost = 300;
    }
    public void DoButton05()
    {
        tempObject = towerPrefabs[5];
        isTowerCreate = true;
        createTowerNum = 5;
        createTowerCost = 500;
    }
    public void IdleTower()
    {
        if (towerModels[createTowerNum].activeSelf == true) towerModels[createTowerNum].SetActive(false);
        isTowerCreate = false;
        createTowerNum = 0;
        createTowerCost = 0;
    }

    public void TowerRenderOff()
    {
        tempObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
    }
    public void CheckEnemy()
    {
        Debug.Log("hp : " + hit.collider.gameObject.GetComponent<EnemyDetail>().hp);
        Debug.Log("ms : " + hit.collider.gameObject.GetComponent<EnemyDetail>().ms);
        Debug.Log("def : " + hit.collider.gameObject.GetComponent<EnemyDetail>().def);
        Debug.Log("gainGold : " + hit.collider.gameObject.GetComponent<EnemyDetail>().gainGold);
        Debug.Log("gainScore : " + hit.collider.gameObject.GetComponent<EnemyDetail>().gainScore);
    }

    public void DoMoveYUp()
    {
        rt.DOAnchorPosY(60f, 0.8f);
    }
    public void DoMoveYDown()
    {
        rt.DOAnchorPosY(-90f, 0.8f);
    }
    public void DoMoveYUpObstacle()
    {
        isObstacle = true;
        rt3.DOAnchorPosY(60f, 0.8f);
    }
    public void DoMoveYDownObstacle()
    {
        isObstacle = false;
        rt3.DOAnchorPosY(-140f, 0.8f);
    }

    public void DoMoveXLeft()
    {
        isDetail = true;
        rt2.DOAnchorPosX(-200f, 0.8f);
    }
    public void DoMoveXRight()
    {
        isDetail = false;
        rt2.DOAnchorPosX(240f, 0.8f);
    }
}
