using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetail : MonoBehaviour
{
    public int hp;
    public int orihp;
    public float ms;
    public int def;
    public int gainGold;
    public int gainScore;

    // Start is called before the first frame update
    void Start()
    {
        switch (gameObject.name)
        {
            case "Enemy01(Clone)":
                hp = orihp = 10;
                ms = 1.8f;
                def = 0;
                gainGold = 1;
                gainScore = 1;
                break;
            case "Enemy02(Clone)":
                hp = orihp = 15;
                ms = 4;
                def = 0;
                gainGold = 3;
                gainScore = 3;
                break;
            case "Enemy03(Clone)":
                hp = orihp = 50;
                ms = 2;
                def = 5;
                gainGold = 5;
                gainScore = 5;
                break;
            case "Enemy04(Clone)":
                hp = orihp = 100;
                ms = 1.5f;
                def = 30;
                gainGold = 10;
                gainScore = 10;
                break;
            case "Enemy05(Clone)":
                hp = orihp = 200;
                ms = 2;
                def = 10;
                gainGold = 20;
                gainScore = 20;
                break;
            default:
                break;
        } // name으로 판별하여 hp등 초기화.
    }
}
