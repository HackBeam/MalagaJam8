using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Rewired;

public class PointerMovement : MonoBehaviour
{
    [SerializeField] private float aimingSensivity;

    private Vector3 movPos;
    private PlayerIdentifier playerID;

    private void Awake()
    {
        playerID = GetComponentInParent<PlayerIdentifier>();
        SubscribeInput();
    }

    private void SubscribeInput()
    {
        Player player = ReInput.players.GetPlayer(playerID.GetPlayerId());
        player.AddInputEventDelegate(MoveHorizontal, UpdateLoopType.Update, InputActionEventType.AxisRawActive, RewiredConsts.Action.AimX);
        player.AddInputEventDelegate(MoveHorizontal, UpdateLoopType.Update, InputActionEventType.AxisRawInactive, RewiredConsts.Action.AimX);
        player.AddInputEventDelegate(MoveVertical, UpdateLoopType.Update, InputActionEventType.AxisRawActive, RewiredConsts.Action.AimY);
        player.AddInputEventDelegate(MoveVertical, UpdateLoopType.Update, InputActionEventType.AxisRawInactive, RewiredConsts.Action.AimY);
    }
    
    private void MoveVertical(InputActionEventData data)
    {
        movPos.z = data.GetAxis();
    }

    private void MoveHorizontal(InputActionEventData data)
    {
        movPos.x = data.GetAxis();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Time.timeScale > 0)
            transform.localPosition = movPos * aimingSensivity;
    }
}
