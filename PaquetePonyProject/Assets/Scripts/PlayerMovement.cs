using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    [ValueDropdown("players")]
    [SerializeField] private int playerID;

    private Vector3 movForce;

    private static ValueDropdownList<int> players = new ValueDropdownList<int>()
    {
        {"Player0", RewiredConsts.Player.Player0},
        {"Player1", RewiredConsts.Player.Player1}
    };

    //Component referneces
    private Rigidbody rb;
    private PlayerStats stats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
        SubscribeInput();
    }

    private void SubscribeInput()
    {
        Player player = ReInput.players.GetPlayer(playerID);
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
        float movSpeed = 1; //stats.GetCurrentSpeed();
        transform.Translate(movForce * movSpeed * Time.deltaTime);
    }

    
}