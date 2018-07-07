using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public enum powerUpType
    {
        damage,
        speed,
        health
    }

    [SerializeField] private int value = 0;
    [SerializeField] public powerUpType _powerUpType;
    
	// Update is called once per frame

    public PowerUp(powerUpType type, int amount)
    {
        _powerUpType = type;
        value = amount;
    }
	public int GetValue()
    {
        return value;
    }

    public void SetValue(int data)
    {
        value = data;
    }

    public void SetType(powerUpType type)
    {
        _powerUpType = type;
    }

    public powerUpType getPowerUpType()
    {
        return _powerUpType;
    }

}
