using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDownParticle : MonoBehaviour {

    public float timeToShootDown;

    private void OnEnable()
    {
        StartCoroutine(ShootDown());
    }

    private IEnumerator ShootDown()
    {
        yield return new WaitForSeconds(timeToShootDown);
        gameObject.SetActive(false);
    }
}
