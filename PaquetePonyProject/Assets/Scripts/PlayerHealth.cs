using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] private int currentHealth;

    private PlayerStats _playerStats;
    private PlayerIdentifier _playerIdentifier;

	// Use this for initialization
	void Awake ()
    {
        _playerStats = gameObject.GetComponent<PlayerStats>();
        _playerIdentifier = gameObject.GetComponent<PlayerIdentifier>();
        currentHealth = _playerStats.GetCurrentMaxHealth();
        RefreshInterface();
    }
	
	public void OnMaxHealthModified(int lastMaxHealth)
    {
        currentHealth = (currentHealth * _playerStats.GetCurrentMaxHealth()) / lastMaxHealth;
        RefreshInterface();
    }

    public void ReceiveDamage (int amount)
    {
        currentHealth -= amount;
        RefreshInterface();
        if (currentHealth <= 0)
        {
            Container.eventSystem.Trigger(new DeathEvent()
            {
                playerId = _playerIdentifier.GetPlayerId()
            });
        }
    }

    public void ReceiveHeal(int amount)
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
            playerId = _playerIdentifier.GetPlayerId(),
            newCurrentHealthPercent = porcentualHealth
        });
    }

}
