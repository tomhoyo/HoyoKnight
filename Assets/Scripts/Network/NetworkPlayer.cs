using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Creatures;
using Assets.Scripts.Weapons;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{

    private PlayerController playerController;    

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        //Camera follow
        GameObject VirtualCamera = GameObject.Find("Virtual Camera");
        Cinemachine.CinemachineVirtualCamera PlayerCamera = VirtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        PlayerCamera.Priority = IsOwner ? 1 : 0;

        //Player controller
        GameObject PlayerGameObject = gameObject.transform.GetChild(0).gameObject;
        PlayerController PlayerController = PlayerGameObject.GetComponent<PlayerController>();
        PlayerController.enabled = IsOwner;

        //Player weapon
        GameObject WeaponGameObject = PlayerGameObject.transform.GetChild(0).gameObject;
        Weapon Weapon = WeaponGameObject.GetComponent<Weapon>();
        Weapon.enabled = IsOwner;

    }
}
