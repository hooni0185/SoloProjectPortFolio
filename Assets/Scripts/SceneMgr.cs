using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("TowerMain");
        Time.timeScale = 1f;
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
