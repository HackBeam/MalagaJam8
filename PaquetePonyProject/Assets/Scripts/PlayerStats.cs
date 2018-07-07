using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class PlayerStats : MonoBehaviour
{

    [Title("Max PowerUp Value")]
    [SerializeField] private int _maxHealthPowerUp = 10;
    [SerializeField] private int _maxDamagePowerUp = 5;
    [SerializeField] private int _maxSpeedPowerUp = 5;

    [Title("current PowerUp Value")]
    [ReadOnly, SerializeField] private int _currentHealthPowerUp = 0;
    [ReadOnly, SerializeField] private int _currentDamagePowerUp = 0;
    [ReadOnly, SerializeField] private int _currentSpeedPowerUp = 0;

    [Title("Base Stats Value")]
    [SerializeField] private int _baseHealth = 20;
    [SerializeField] private int _baseDamage = 10;
    [SerializeField] private int _baseSpeed = 10;

    [Title("Triggers Detection")]
    [SerializeField] private LayerMask _layerInvertZone;
    [SerializeField] private LayerMask _layerPowerUp;

    [Title("UI Stats")]
    [SerializeField] private Image speedFill;
    [SerializeField] private Image damageFill;
    [SerializeField] private Image extraHealthFill;
    [SerializeField] private Color positiveColor;
    [SerializeField] private Color negativeColor;

    private bool currentlyInverted;

    private PlayerHealth _playerHealth;
    private PlayerIdentifier _playerIdentifier;
    private int currentZones = 0;

    private void OnEnable()
    {
        RefreshInterface();
    }

    private void OnValidate()
    {
        if ((_baseHealth - _maxHealthPowerUp) <= 0)
        {
            _maxHealthPowerUp = _baseHealth - 1;
        }

        if ((_baseSpeed - _maxSpeedPowerUp) <= 0)
        {
            _maxSpeedPowerUp = _baseSpeed - 1;
        }

        if ((_baseDamage - _maxDamagePowerUp) <= 0)
        {
            _maxDamagePowerUp = _baseDamage - 1;
        }
    }

    public void Awake()
    {
        _playerHealth = gameObject.GetComponent<PlayerHealth>();
        _playerIdentifier = gameObject.GetComponent<PlayerIdentifier>();
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
        return _baseHealth + _currentHealthPowerUp;
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
        else if (_currentDamagePowerUp < -_maxDamagePowerUp)
        {
            _currentDamagePowerUp = -_maxDamagePowerUp;
        }
        RefreshInterface();
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
        RefreshInterface();
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
        RefreshInterface();
        _playerHealth.OnMaxHealthModified(lastMaxHealth);
    }
    #endregion

    public void EnterInversionZone()
    {
        currentZones++;
        if (!currentlyInverted)
        {
            int lastMaxHealth = GetCurrentMaxHealth();
            _currentHealthPowerUp = -_currentHealthPowerUp;
            _currentDamagePowerUp = -_currentDamagePowerUp;
            _currentSpeedPowerUp = -_currentSpeedPowerUp;
            RefreshInterface();
            _playerHealth.OnMaxHealthModified(lastMaxHealth);
            currentlyInverted = true;
        }
    }

    public void ExitInversionZone()
    {
        currentZones--;
        if (currentlyInverted && currentZones <= 0)
        {
            int lastMaxHealth = GetCurrentMaxHealth();
            _currentHealthPowerUp = -_currentHealthPowerUp;
            _currentDamagePowerUp = -_currentDamagePowerUp;
            _currentSpeedPowerUp = -_currentSpeedPowerUp;
            _playerHealth.OnMaxHealthModified(lastMaxHealth);
            RefreshInterface();
            currentlyInverted = false;
            currentZones = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // Es una zona de inversion
        if (((1 << other.gameObject.layer) & _layerInvertZone) != 0)
        {
            EnterInversionZone();
        }

        // Es un power Up
        if (((1 << other.gameObject.layer) & _layerPowerUp) != 0)
        {
            Debug.Log("PowerUp");
            PowerUp item = other.gameObject.GetComponent<PowerUp>();
            switch (item._powerUpType)
            {
                case PowerUp.powerUpType.damage:
                    ModifyDamage(item.GetValue());
                    Destroy(other.gameObject);
                    break;
                case PowerUp.powerUpType.speed:
                    ModifySpeed(item.GetValue());
                    Destroy(other.gameObject);
                    break;
                case PowerUp.powerUpType.health:
                    ModifyHealth(item.GetValue());
                    Destroy(other.gameObject);
                    break;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Es una zona de inversion
        if (((1 << other.gameObject.layer) & _layerInvertZone) != 0)
        {
            ExitInversionZone();
        }
    }

    private void RefreshInterface()
    {
        /*Container.eventSystem.Trigger(new StatsChangedEvent()
        {
            playerId = _playerIdentifier.GetPlayerId(),
            newHealthPowerUp = _currentHealthPowerUp,
            newDamagePowerUp = _currentDamagePowerUp,
            newSpeedPowerUp = _currentSpeedPowerUp
        });
        */

        speedFill.fillAmount = Mathf.Abs(_currentSpeedPowerUp) / (float) _maxSpeedPowerUp;;
        

        if (_currentSpeedPowerUp < 0)
        {
            speedFill.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            speedFill.color = negativeColor;
        }
        else
        {
            speedFill.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            speedFill.color = positiveColor;
        }

        damageFill.fillAmount = Mathf.Abs(_currentDamagePowerUp) / (float) _maxDamagePowerUp;

        if (_currentDamagePowerUp < 0)
        {
            damageFill.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            damageFill.color = negativeColor;
        }
        else
        {
            damageFill.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            damageFill.color = positiveColor;
        }

        extraHealthFill.fillAmount = Mathf.Abs(_currentHealthPowerUp) / (float) _maxHealthPowerUp;

        if (_currentHealthPowerUp < 0)
        {
            extraHealthFill.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            extraHealthFill.color = negativeColor;
        }
        else
        {
            extraHealthFill.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            extraHealthFill.color = positiveColor;
        }
    }
}


