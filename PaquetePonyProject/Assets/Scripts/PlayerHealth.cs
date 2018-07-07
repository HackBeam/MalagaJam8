﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] private int currentHealth;

    private int _playerId = -1;

    private PlayerStats _playerStats;

	// Use this for initialization
	void Awake ()
    {
        _playerStats = gameObject.GetComponent<PlayerStats>();
        currentHealth = _playerStats.GetCurrentMaxHealth();
        //Get PlayerId
        RefreshInterface();
    }
	
	public void OnMaxHealthModified(int lastMaxHealth)
    {
        currentHealth = (currentHealth * _playerStats.GetCurrentMaxHealth()) / lastMaxHealth;
        RefreshInterface();
    }

    public void DoDamage (int amount)
    {
        currentHealth -= amount;
        RefreshInterface();
        if (currentHealth <= 0)
        {
            Container.eventSystem.Trigger(new DeathEvent()
            {
                playerId = _playerId
            });
        }
    }

    public void DoHeal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > _playerStats.GetCurrentMaxHealth())
        {
            currentHealth = _playerStats.GetCurrentMaxHealth();
        }
        RefreshInterface();
    }

    private void RefreshInterface()
    {
        float porcentualHealth = 0;
        porcentualHealth = (currentHealth / _playerStats.GetCurrentMaxHealth());
        Container.eventSystem.Trigger(new HealthChangedEvent()
        {
            playerId = _playerId,
            newCurrentHealthPercent = currentHealth
        });
    }

}
