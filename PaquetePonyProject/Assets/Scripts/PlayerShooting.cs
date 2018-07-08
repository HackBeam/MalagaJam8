using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Rewired;
using System;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform bulletStart;
    [SerializeField] private MultiPoolSystem poolSystem;

    [Title("Fire Time Configurations")]
    [SerializeField] private float fireRate;

    private Animator playerAnimator;

    private bool canFire = true;
    private int ammo;

    private PlayerIdentifier playerID;
    
    //Component References
    private PlayerStats stats;
    private WaitForSeconds fireRateWait;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        fireRateWait = new WaitForSeconds(fireRate);
        playerID = GetComponentInParent<PlayerIdentifier>();
        playerAnimator = GetComponentInChildren<Animator>();
        SubscribeInput();
    }

    private void SubscribeInput()
    {
        Player player = ReInput.players.GetPlayer(playerID.GetPlayerId());
        player.AddInputEventDelegate(Fire, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, RewiredConsts.Action.Fire);
    }

    private void Fire(InputActionEventData data)
    {
        if (canFire)
        {
            GameObject bullet = poolSystem.GetFreeObject<Shoot>();
            bullet.GetComponent<Shoot>().damage = stats.GetCurrentDamage();
            bullet.transform.position = bulletStart.transform.position;
            bullet.transform.rotation = bulletStart.transform.rotation;
            bullet.SetActive(true);

            playerAnimator.SetBool("Disparar",true);
            StartCoroutine(FireRateTimer());
        }
    }


    private IEnumerator FireRateTimer()
    {
        canFire = false;
        yield return fireRateWait;
        canFire = true;
    }
}
