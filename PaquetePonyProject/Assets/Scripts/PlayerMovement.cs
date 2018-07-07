using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movForce;

    //Component referneces
    private Rigidbody rb;
    private PlayerStats stats;
    private PlayerIdentifier playerID;
    private Animator playerAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
        playerID = GetComponentInParent<PlayerIdentifier>();
        playerAnimator = GetComponentInChildren<Animator>();
        SubscribeInput();
    }

    private void SubscribeInput()
    {
        Player player = ReInput.players.GetPlayer(playerID.GetPlayerId());
        player.AddInputEventDelegate(MoveHorizontal, UpdateLoopType.Update, InputActionEventType.AxisRawActive, RewiredConsts.Action.MovX);
        player.AddInputEventDelegate(MoveHorizontal, UpdateLoopType.Update, InputActionEventType.AxisRawInactive, RewiredConsts.Action.MovX);
        player.AddInputEventDelegate(MoveVertical, UpdateLoopType.Update, InputActionEventType.AxisRawActive, RewiredConsts.Action.MovY);
        player.AddInputEventDelegate(MoveVertical, UpdateLoopType.Update, InputActionEventType.AxisRawInactive, RewiredConsts.Action.MovY);
    }

    private void MoveVertical(InputActionEventData data)
    {
        movForce.z = data.GetAxis();
    }

    private void MoveHorizontal(InputActionEventData data)
    {
        movForce.x = data.GetAxis();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float movSpeed = stats.GetCurrentSpeed();
        transform.Translate(movForce * movSpeed * Time.deltaTime);
        playerAnimator.SetBool("Correr",movForce.magnitude > 0.05);
    }  
}