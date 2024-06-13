using Assets.Scripts.Creatures;
using Assets.Scripts.Weapons;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        //Camera follow
        GameObject virtualCamera = GameObject.Find("Virtual Camera");
        if (virtualCamera != null)
        {
            Cinemachine.CinemachineVirtualCamera playerCamera = virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            playerCamera.Priority = IsOwner ? 1 : 0;
        }
       
        //Player controller
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = IsOwner;
        }

        //Player weapon
        GameObject gameObjectWeapon = gameObject.transform.GetChild(0).gameObject;
        if (gameObjectWeapon != null)
        {
            Weapon weapon = gameObjectWeapon.GetComponent<Weapon>();
            weapon.enabled = IsOwner;
        }
        

        //Player Damage
        PlayerDamage playerDamage = gameObject.GetComponent<PlayerDamage>();
        if (playerDamage != null)
        {
            playerDamage.enabled = IsOwner;
        }

        //Interactor
        Interactor interactor = gameObject.GetComponent<Interactor>();
        if (interactor != null)
        {
            interactor.enabled = IsOwner;
        }
    }
}
