using System;
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
       /* [SerializeField]
        private TMP_InputField hostIpAddress;
        public String HostIpAddress 
        { 
            get 
            {
                return hostIpAddress.text;
            } 
        }*/

        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StopHost()
        {
            throw new NotImplementedException();
        }

        public void StartClient()
        {
            //Debug.Log("StartClient at address : " + HostIpAddress);

            //Before need to validate the address format. and try a ping.
            //NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = HostIpAddress;
            NetworkManager.Singleton.StartClient();
        }

        public void StopClient() 
        {
            throw new NotImplementedException();
        }

    }
}
