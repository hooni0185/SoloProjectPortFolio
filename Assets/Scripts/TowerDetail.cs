using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetail : MonoBehaviour
{
    public int atk;
    public float atkspeed;
    public float range;
    public int level;
    public int cost;
    public int upatk;
    public float upspeed_coefficient;
    public float uprange_coefficient;


    // Start is called before the first frame update
    void Start()
    {
        switch (gameObject.name)
        {
            case "Tower01(Clone)":
                atk = 5;
                atkspeed = 1;
                range = 4f;
                level = 1;
                cost = 20;
                upatk = 5;
                upspeed_coefficient = 1f;
                uprange_coefficient = 1f;
                break;
            case "Tower02(Clone)":
                atk = 10;
                atkspeed = 1f;
                range = 6f;
                level = 1;
                upatk = 7;
                cost = 50;
                upspeed_coefficient = 1f;
                uprange_coefficient = 1.5f;
                break;
            case "Tower03(Clone)":
                atk = 10;
                atkspeed = 1;
                range = 6f;
                level = 1;
                upatk = 20;
                cost = 150;
                upspeed_coefficient = 1f;
                uprange_coefficient = 1.25f;
                break;
            case "Tower04(Clone)":
                atk = 10;
                atkspeed = 2;
                range = 1;
                level = 1;
                cost = 300;
                break;
            case "Tower05(Clone)":
                atk = 5;
                atkspeed = 0.5f;
                range = 1;
                level = 1;
                cost = 500;
                break;
            case "Tower06(Clone)":
                break;
            default:
                break;
        }
    }
    
}
