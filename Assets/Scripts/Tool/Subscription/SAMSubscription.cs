using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Tool.Subscription
{
    internal class SAMSubscription : MonoBehaviour
    {
        public UnityEvent EventSoloGroupSuccess;
        public UnityEvent EventHostCreationSuccess;
        public UnityEvent EventHostCreationFail;
        public UnityEvent EventClientJoinSuccess;
        public UnityEvent EventClientJoinFail;
        public UnityEvent EventNewIpAddressNotValid;

        public void OnSoloGroupSuccess()
        {
            EventSoloGroupSuccess?.Invoke();
        }

        public void OnHostCreationSuccess()
        {
            EventHostCreationSuccess?.Invoke();
        }

        public void OnHostCreationFail()
        {
            EventHostCreationFail?.Invoke();

        }

        public void OnClientJoinSuccess()
        {
            EventClientJoinSuccess?.Invoke();
        }

        public void OnClientJoinFail()
        {
            EventClientJoinFail?.Invoke();
        }


        public void OnEventIpAddressNotValid()
        {
            EventNewIpAddressNotValid?.Invoke();
        }



    }
}
