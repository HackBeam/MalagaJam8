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
    [SerializeField] private float reloadTime;
    [SerializeField] private int magazineCapacity;

    private Animator playerAnimator;
    private AudioSource audio;

    private bool canFire = true;
    private int ammo;

    private PlayerIdentifier playerID;
    
    //Component References
    private PlayerStats stats;
    private WaitForSeconds fireRateWait;
    private WaitForSeconds reloadWait;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        fireRateWait = new WaitForSeconds(fireRate);
        reloadWait = new WaitForSeconds(reloadTime);
        playerID = GetComponentInParent<PlayerIdentifier>();
        playerAnimator = GetComponentInChildren<Animator>();
        audio = GetComponent<AudioSource>();
        SubscribeInput();
    }

    private void OnEnable()
    {
        ammo = magazineCapacity;
    }

    private void SubscribeInput()
    {
        Player player = ReInput.players.GetPlayer(playerID.GetPlayerId());
        player.AddInputEventDelegate(Fire, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, RewiredConsts.Action.Fire);
    }

    private void Fire(InputActionEventData data)
    {
        if (canFire && ammo > 0)
        {
            GameObject bullet = poolSystem.GetFreeObject<Shoot>();
            bullet.GetComponent<Shoot>().damage = stats.GetCurrentDamage();
            bullet.transform.position = bulletStart.transform.position;
            bullet.transform.rotation = bulletStart.transform.rotation;
            bullet.SetActive(true);

            playerAnimator.SetBool("Disparar",true);

            ammo--;
            StartCoroutine(FireRateTimer());

            if (ammo <= 0)
            {
                StartCoroutine(ReloadTime());
            }
        }
    }

    private IEnumerator ReloadTime()
    {
        audio.Play();
        yield return reloadWait;
        ammo = magazineCapacity;
    }

    private IEnumerator FireRateTimer()
    {
        canFire = false;
        yield return fireRateWait;
        canFire = true;
    }
}
