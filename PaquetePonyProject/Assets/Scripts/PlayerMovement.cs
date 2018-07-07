using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class PlayerMovement : MonoBehaviour
{
	[Title("Movement")]
    [SerializeField] private string movementAxisX;
    [SerializeField] private string movementAxisY;

    [Title("Aiming Pointer")]
    [SerializeField] private GameObject pointer;

    //Component referneces
    private Rigidbody rb;
    //private PlayerStats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float movXaxis = 0;
        float movYaxis = 0;

        if (movementAxisX != "")
            movXaxis = Input.GetAxisRaw(movementAxisX);

        if (movementAxisY != "")
            movYaxis = Input.GetAxisRaw(movementAxisY);

        Vector3 movForce = new Vector3(movXaxis, 0, movYaxis);
        float movSpeed = 1; //Get from stats component

        transform.Translate(movForce * movSpeed);
    }

    private void Rotate()
    {
        if (pointer != null)
        {
            Vector3 lookPos = pointer.transform.position;

            lookPos.y = 0;
            transform.LookAt(lookPos);

            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}