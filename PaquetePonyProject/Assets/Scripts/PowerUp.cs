using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUp : MonoBehaviour
{

    public enum powerUpType
    {
        damage,
        speed,
        health
    }

    [SerializeField] private int value = 0;
    [SerializeField] public powerUpType _powerUpType;

    private void Update()
    {
        transform.Rotate(0, 5, 0);
    }

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
