﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Rewired;
using System;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform bulletStart;
    [SerializeField] private MultiPoolSystem poolSystem;

    [ValueDropdown("players")]
    [SerializeField] private int playerID;

    [Title("Fire Time Configurations")]
    [SerializeField] private float fireRate;
    [SerializeField] private float reloadTime;
    [SerializeField] private int magazineCapacity;

    private bool canFire = true;
    private int ammo;

    private static ValueDropdownList<int> players = new ValueDropdownList<int>()
    {
        {"Player0", RewiredConsts.Player.Player0},
        {"Player1", RewiredConsts.Player.Player1}
    };

    //Component References
    private PlayerStats stats;
    private WaitForSeconds fireRateWait;
    private WaitForSeconds reloadWait;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        fireRateWait = new WaitForSeconds(fireRate);
        reloadWait = new WaitForSeconds(reloadTime);
        SubscribeInput();
    }

    private void OnEnable()
    {
        ammo = magazineCapacity;
    }

    private void SubscribeInput()
    {
        Player player = ReInput.players.GetPlayer(playerID);
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
