using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.StringConstant;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public void StartGame()
    {
        PlayScene(SceneName.TESTGAMEPLAY);
    }

    public void MainMenu()
    {
        PlayScene(SceneName.MAINMENU);

    }

    public void PlayScene(String scene)
    {
        SceneManager.LoadScene(scene);

    }
}
