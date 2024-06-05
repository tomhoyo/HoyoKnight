using System;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Network
{
    internal class SessionAccessManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject networkManagerPrefab;
        [SerializeField]
        private GameObject networkManager;
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private GameObject player;

        [SerializeField]
        private playModes _playMode = playModes.None;
        public playModes PlayMode { get { return _playMode; } private set {  _playMode = value; } }

        public enum playModes
        {
            None,
            Solo,
            Host,
            Client            
        }

        /* [SerializeField]
         private TMP_InputField hostIpAddress;
         public String HostIpAddress 
         { 
             get 
             {
                 return hostIpAddress.text;
             } 
         }*/

        private static SessionAccessManager instance = null;
        public static SessionAccessManager Instance => instance;
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

        public void ChangePlayMode(playModes mode)
        {

            if (!PlayMode.Equals(mode))
            {

                if (player != null)
                {
                    Destroy(player);
                }

                if (networkManager != null)
                {
                    Destroy(networkManager);
                }

                switch (mode)
                {
                    case playModes.None:
                        PlayMode = playModes.None;
                        break;
                    case playModes.Solo:
                        player = Instantiate(playerPrefab);
                        PlayMode = playModes.Solo;
                        break;
                    case playModes.Host:
                        networkManager = Instantiate(networkManagerPrefab);
                        NetworkManager.Singleton.StartHost();
                        PlayMode = playModes.Host;
                        break;
                    case playModes.Client:
                        networkManager = Instantiate(networkManagerPrefab);
                        NetworkManager.Singleton.StartClient();
                        PlayMode = playModes.Client;

                        break;
                    default: 
                        throw new Exception("Play mode doesn't exist");

                }
            }

        }

        public void StartNone()
        {
            ChangePlayMode(playModes.None);

        }

        public void StartSolo()
        {
            ChangePlayMode(playModes.Solo);
        }

        public void StartHost()
        {
            ChangePlayMode(playModes.Host);

        }

        public void StartClient()
        {


            //Debug.Log("StartClient at address : " + HostIpAddress);

            //Before need to validate the address format. and try a ping.
            //NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = HostIpAddress;
            ChangePlayMode(playModes.Client);

        }
       
    }
}
