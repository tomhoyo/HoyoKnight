﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Network
{
    internal class SessionAccessManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject networkManagerPrefab;
        private GameObject networkManager;
        [SerializeField]
        private GameObject playerPrefab;
        private GameObject player;

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

        public void Start()
        {
            StartSolo();
            gameObject.GetComponent<Menu>().HidePanel();
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
                    case playModes.Solo:
                        player = Instantiate(playerPrefab);
                        break;
                    case playModes.Host:
                        networkManager = Instantiate(networkManagerPrefab);
                        NetworkManager.Singleton.StartHost();
                        break;
                    case playModes.Client:
                        networkManager = Instantiate(networkManagerPrefab);
                        NetworkManager.Singleton.StartClient();
                        break;
                    default: 
                        throw new Exception("Play mode doesn't exist");

                }

                PlayMode = mode;

            }
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
