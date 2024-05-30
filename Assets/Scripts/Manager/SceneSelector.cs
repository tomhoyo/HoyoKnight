using System;
using Assets.Scripts.StringConstant;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public void StartGame()
    {
        PlayScene(ScenesName.TESTGAMEPLAY);
    }

    public void MainMenu()
    {
        PlayScene(ScenesName.MAINMENU);

    }

    public void PlayScene(String scene)
    {
        SceneManager.LoadScene(scene);

    }
}
