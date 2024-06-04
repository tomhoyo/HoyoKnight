using System;
using Assets.Scripts.Network;
using Assets.Scripts.StringConstant;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    //public UnityEvent EventLoadMainMenu;

    private static SceneSelector instance = null;
    public static SceneSelector Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayScene(ScenesName.MAINMENU);
    }

    public void StartGame()
    {
        PlayScene(ScenesName.TESTGAMEPLAY);
    }

    public void MainMenu()
    {
        //EventLoadMainMenu?.Invoke();
        GameObject.Find(GameObjectsName.LOBBYMANAGER).GetComponent<SessionAccessManager>().StartNone();
        PlayScene(ScenesName.MAINMENU);
    }

    public void LoadLobby()
    {
        PlayScene(ScenesName.LOBBY);
        GameObject.Find(GameObjectsName.LOBBYMANAGER).GetComponent<SessionAccessManager>().StartSolo();

    }

    public void PlayScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }
}
