using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerStats : MonoBehaviour {

    [Title("Max PowerUp Value")]
    [SerializeField] private int _maxHealthPowerUp = 10;
    [SerializeField] private int _maxDamagePowerUp = 5;
    [SerializeField] private int _maxSpeedPowerUp = 5;

    private int _currentHealthPowerUp = 0;
    private int _currentDamagePowerUp = 0;
    private int _currentSpeedPowerUp = 0;

    [Title("Base Stats Value")]
    [SerializeField] private int _baseHealth = 10;
    [SerializeField] private int _baseDamage = 5;
    [SerializeField] private int _baseSpeed = 5;

    private bool currentlyInverted;

    private PlayerHealth _playerHealth;
    private int currentZones = 0;

    public void Awake()
    {
        _playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    #region Properties
    public int GetCurrentSpeed()
    {
        return _baseSpeed + _currentSpeedPowerUp;
    }

    public int GetCurrentDamage()
    {
        return _baseDamage + _currentDamagePowerUp;
    }
  
    public int GetCurrentMaxHealth()
    {
        return _baseSpeed + _currentSpeedPowerUp;
    }
    #endregion

    #region PowerUps
    public void ModifyDamage(int value)
    {
        _currentDamagePowerUp += value;
        if (_currentDamagePowerUp > _maxDamagePowerUp)
        {
            _currentDamagePowerUp = _maxDamagePowerUp;
        }
        else if(_currentDamagePowerUp < -_maxDamagePowerUp)
        {
            _currentDamagePowerUp = -_maxDamagePowerUp;
        }
            
    }

    public void ModifySpeed(int value)
    {
        _currentSpeedPowerUp += value;
        if (_currentSpeedPowerUp > _maxSpeedPowerUp)
        {
            _currentSpeedPowerUp = _maxSpeedPowerUp;
        }
        else if (_currentSpeedPowerUp < -_maxSpeedPowerUp)
        {
            _currentSpeedPowerUp = -_maxSpeedPowerUp;
        }

    }

    public void ModifyHealth(int value)
    {
        int lastMaxHealth = GetCurrentMaxHealth();
        _currentHealthPowerUp += value;
        if (_currentHealthPowerUp > _maxHealthPowerUp)
        {
            _currentHealthPowerUp = _maxHealthPowerUp;
        }
        else if (_currentHealthPowerUp < -_maxHealthPowerUp)
        {
            _currentHealthPowerUp = -_maxHealthPowerUp;
        }
        _playerHealth.OnMaxHealthModified(lastMaxHealth);
    }
    #endregion

    void EnterInversionZone()
    {
        currentZones++;
        if (!currentlyInverted)
        {
            _currentHealthPowerUp = -_currentHealthPowerUp;
            _currentDamagePowerUp = -_currentDamagePowerUp;
            _currentSpeedPowerUp = -_currentSpeedPowerUp;
            currentlyInverted = true;
        }
    }

    void ExitInversionZone()
    {
        currentZones--;
        if (currentlyInverted && currentZones == 0)
        {
            _currentHealthPowerUp = -_currentHealthPowerUp;
            _currentDamagePowerUp = -_currentDamagePowerUp;
            _currentSpeedPowerUp = -_currentSpeedPowerUp;
            currentlyInverted = false;
        }
    }
}
