using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [HideInInspector] public int damage;

    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask collidableLayers;

    private WaitForSeconds lifeWait;

    private void Awake()
    {
        lifeWait = new WaitForSeconds(lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        StartCoroutine(LifetimeCountdown());
    }

    private IEnumerator LifetimeCountdown()
    {
        yield return lifeWait;
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (((1 << other.gameObject.layer) & collidableLayers) != 0)
        {
            PlayerHealth otherHealth =  other.gameObject.GetComponent<PlayerHealth>();

            if (otherHealth)
            {
                otherHealth.ReceiveDamage(damage);
            }

            gameObject.SetActive(false);
        }
    }
}
