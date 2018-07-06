using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] private int currentHealth;

    private PlayerStats _playerStats;

	// Use this for initialization
	void Awake ()
    {
        _playerStats = gameObject.GetComponent<PlayerStats>();
        currentHealth = _playerStats.GetCurrentMaxHealth();
	}
	
	public void OnMaxHealthModified(int lastMaxHealth)
    {
        currentHealth = (currentHealth * _playerStats.GetCurrentMaxHealth()) / lastMaxHealth;
    }

    public void DoDamage (int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //funcion de muerte
        }
    }


}
