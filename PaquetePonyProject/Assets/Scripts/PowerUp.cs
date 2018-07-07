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
	public int getValue()
    {
        return value;
    }

    public powerUpType getPowerUpType()
    {
        return _powerUpType;
    }

}
