using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Creatures;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{

    public PlayerController playerController;
    public Cinemachine.CinemachineVirtualCamera playerCamera;
    

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        playerController.enabled = IsOwner;
        playerCamera.Priority = IsOwner ? 1 : 0;


    }
}
