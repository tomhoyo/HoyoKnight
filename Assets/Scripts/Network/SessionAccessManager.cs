using System;
using System.Text.RegularExpressions;
using Assets.Scripts.StringConstant;
using Assets.Scripts.Tool.Subscription;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;


namespace Assets.Scripts.Network
{
    internal class SessionAccessManager : MonoBehaviour
    {
        public string HostIpAddress { 
            get 
            {
                string IpAddress = "";
                GameObject IpAddressInputField = GameObject.Find(GameObjectsName.IPADDRESSINPUTAREA);
                if(IpAddressInputField != null )
                {
                    TMP_InputField IpAddressText = IpAddressInputField.GetComponent<TMP_InputField>();
                    IpAddress = IpAddressText.text.ToString();
                }
                return IpAddress; 
            } 
        }

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
                switch (mode)
                {
                    case playModes.None:
                        CleanGroup();
                        PlayMode = playModes.None;
                        break;
                    case playModes.Solo:
                        CleanGroup();
                        player = Instantiate(playerPrefab);
                        PlayMode = playModes.Solo;
                        SoloGroupSuccess();
                        break;
                    case playModes.Host:
                        CleanGroup();
                        networkManager = Instantiate(networkManagerPrefab);
                        if (NetworkManager.Singleton.StartHost())
                        {
                            PlayMode = playModes.Host;
                            HostCreationSuccess();
                        }
                        else
                        {
                            networkManager = null;
                            HostCreationFail();
                            StartSolo();
                        }
                        break;
                    case playModes.Client:
                        if (!IsIpAddressValid(HostIpAddress))
                        {
                            IpAddressNotValid();
                        }
                        else
                        {
                            CleanGroup();
                            networkManager = Instantiate(networkManagerPrefab);
                            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = HostIpAddress;
                            PlayMode = playModes.Client;
                            if (NetworkManager.Singleton.StartClient())
                            {
                                ClientJoinSuccess();
                            }
                            else
                            {
                                networkManager = null;
                                ClientJoinFail();
                                StartSolo();
                            }
                        }
                        break;
                    default: 
                        throw new NotImplementedException("Play mode: " + mode + " doesn't exist");
                }
            }
        }

        private void CleanGroup()
        {
            if (player != null)
            {
                Destroy(player);
            }

            if (networkManager != null)
            {
                Destroy(networkManager);
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
            ChangePlayMode(playModes.Client);
        }

        public bool IsIpAddressValid(String IpAddress)
        {
            string pattern = "^((25[0-5]|(2[0-4]|1\\d|[1-9]|)\\d)\\.?\\b){4}$";
            Match match = Regex.Match(IpAddress, pattern, RegexOptions.IgnoreCase);

            return match.Success;
        }

        private void SoloGroupSuccess()
        {
            GameObject NPC = GroupCreationNPC();
            if (NPC != null)
            {
                NPC.GetComponent<SAMSubscription>().OnSoloGroupSuccess();
            }
        }

        private void HostCreationSuccess()
        {
            GameObject NPC = GroupCreationNPC();
            if (NPC != null)
            {
                NPC.GetComponent<SAMSubscription>().OnHostCreationSuccess();
            }
        }

        private void HostCreationFail()
        {
            GameObject NPC = GroupCreationNPC();
            if (NPC != null)
            {
                NPC.GetComponent<SAMSubscription>().OnHostCreationFail();
            }
        }

        private void ClientJoinSuccess()
        {
            GameObject NPC = GroupCreationNPC();
            if (NPC != null)
            {
                NPC.GetComponent<SAMSubscription>().OnClientJoinSuccess();
            }
        }

        private void ClientJoinFail()
        {
            GameObject NPC = GroupCreationNPC();
            if (NPC != null)
            {
                NPC.GetComponent<SAMSubscription>().OnClientJoinFail();
            }
        }

        private void IpAddressNotValid()
        {
            GameObject NPC = GroupCreationNPC();
            if (NPC != null)
            {
                NPC.GetComponent<SAMSubscription>().OnEventIpAddressNotValid();
            }
        }

        private GameObject GroupCreationNPC()
        {
            return GameObject.Find(GameObjectsName.GROUPCREATOR);
        }

    }
}
