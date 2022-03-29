using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    public bool isPause = false;
    public bool isDouble = false;
    public Button doubleSpd;
    public GameObject image_Pause;
    public void GamePause()
    {
        if (isPause == false)
        {
            isPause = true;
            Time.timeScale = 0.001f;
        }
        else
        {
            isPause = false;
            if (isDouble)
                Time.timeScale = 1.5f;
            else 
                Time.timeScale = 1f;
        }
    }


    public void isBtn()
    {
        if (isDouble == false)
        {
            isDouble = true;
            Time.timeScale = 1.5f;
        }
        else
        {
            isDouble = false;
            if (isPause)
                Time.timeScale = 0.001f;
            else
                Time.timeScale = 1f;
        }
         
    }


    public void BtnOption()
    {
        if (image_Pause.activeSelf == true)
        {
            isPause = false;
            image_Pause.SetActive(false);
            if (isDouble == false)
                Time.timeScale = 1f;
            else
                Time.timeScale = 1.5f;
        }
        else
        {
            isPause = true;
            image_Pause.SetActive(true);
            Time.timeScale = 0.001f;
        }
    }

}
