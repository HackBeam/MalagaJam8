using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ScreenShake : MonoBehaviour
{

    [SerializeField] private float duration;
    [SerializeField] private float strength;
    [SerializeField] private int vibrato;
    [SerializeField] private float randomness;

	// Use this for initialization
	void Awake ()
    {
        Container.eventSystem.Subscribe<ScreenShakeEvent>(Shake);
	}

    private void Shake(ScreenShakeEvent obj)
    {
        transform.DOShakePosition(duration,strength,vibrato,randomness);
    }

    [Button]
    private void DemoShake()
    {
        transform.DOShakePosition(duration, strength, vibrato, randomness);
    }

}
