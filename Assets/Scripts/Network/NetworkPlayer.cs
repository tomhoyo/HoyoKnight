using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Creatures;
using Assets.Scripts.Weapons;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        //Camera follow
        GameObject virtualCamera = GameObject.Find("Virtual Camera");
        Cinemachine.CinemachineVirtualCamera playerCamera = virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        playerCamera.Priority = IsOwner ? 1 : 0;

        //Player controller
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        playerController.enabled = IsOwner;

        //Player weapon
        Weapon weapon = gameObject.transform.GetChild(0).gameObject.GetComponent<Weapon>();
        weapon.enabled = IsOwner;

        //Player Damage
        PlayerDamage playerDamage = gameObject.GetComponent<PlayerDamage>();
        playerDamage.enabled = IsOwner;
    }
}
