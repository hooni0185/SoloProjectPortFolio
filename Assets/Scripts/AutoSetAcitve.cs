using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSetAcitve : MonoBehaviour
{
    float curTime, coolTime;
    void Start()
    {
        curTime = 0;
        coolTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                curTime = 0;
                gameObject.SetActive(false);
            }
        }       
    }
}
