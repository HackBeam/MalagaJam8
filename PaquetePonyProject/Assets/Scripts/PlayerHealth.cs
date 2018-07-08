//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerHealth : MonoBehaviour {

    [ReadOnly, SerializeField]private int currentHealth;
    [SerializeField] private LayerMask _invalidSpawnLayers;
    [SerializeField] private GameObject _child;
    private PlayerStats _playerStats;
    private PlayerIdentifier _playerIdentifier;
    private float _radius = 101;
    private Transform _scenaryCenter;

	// Use this for initialization
	void Awake ()
    {
        _playerStats = gameObject.GetComponent<PlayerStats>();
        _playerIdentifier = gameObject.GetComponent<PlayerIdentifier>();
        currentHealth = _playerStats.GetCurrentMaxHealth();
        Container.eventSystem.Subscribe<CenterScenaryEvent>(SetCenter);
        RefreshInterface();
    }


    private void SetCenter(CenterScenaryEvent obj)
    {
        _scenaryCenter = obj.transformReference;
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
            Death();
        }
        Container.eventSystem.Trigger(new ScreenShakeEvent());
    }

    [Button]
    private void Death()
    {
        // Animacion de desaparecer
        Container.eventSystem.Trigger(new DeathEvent()
        {
            deadPlayerId = _playerIdentifier.GetPlayerId()
        });

        currentHealth = _playerStats.GetCurrentMaxHealth();
        StartCoroutine(Respawn());
        _child.SetActive(false);
    }

    private IEnumerator Respawn()
    {
        bool placeFounded = false;
        Vector3 position = Vector3.zero;
        while (!placeFounded)
        {
            Vector3 _right = _scenaryCenter.right * Random.Range(-_radius, _radius);
            Vector3 _left = _scenaryCenter.forward * Random.Range(-_radius, _radius);
            position = (_right + _left) + _scenaryCenter.position;

            if (Physics.OverlapSphere(position, 1, _invalidSpawnLayers).Length == 0)
            {
                placeFounded = true;
            }
            yield return null;
        }
        transform.position = new Vector3(position.x,transform.position.y,position.z);
        _child.SetActive(true);
        RefreshInterface();
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
        porcentualHealth = ((float) currentHealth / _playerStats.GetCurrentMaxHealth());
        Container.eventSystem.Trigger(new HealthChangedEvent()
        {
            playerId = _playerIdentifier.GetPlayerId(),
            newCurrentHealthPercent = porcentualHealth
        });
    }

}
