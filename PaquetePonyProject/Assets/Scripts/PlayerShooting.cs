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

    [ValueDropdown("players")]
    [SerializeField] private int playerID;
    [SerializeField] private float fireRate;

    private bool canFire = true;

    private static ValueDropdownList<int> players = new ValueDropdownList<int>()
    {
        {"Player0", RewiredConsts.Player.Player0},
        {"Player1", RewiredConsts.Player.Player1}
    };

    //Component References
    private PlayerStats stats;
    private WaitForSeconds wait;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        wait = new WaitForSeconds(fireRate);
        SubscribeInput();
    }

    private void SubscribeInput()
    {
        Player player = ReInput.players.GetPlayer(playerID);
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
			StartCoroutine(FireRateTimer());
        }
    }

    private IEnumerator FireRateTimer()
    {
        canFire = false;
		yield return wait;
		canFire = true;
    }
}
