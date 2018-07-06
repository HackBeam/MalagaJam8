using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PointerMovement : MonoBehaviour
{
    [Title("Aiming Config")]
    [SerializeField] private string aimingAxisX;
    [SerializeField] private string aimingAxisY;
    [SerializeField] private float aimingSensivity;
	[SerializeField] private GameObject playerToFollow;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float movXaxis = Input.GetAxisRaw(aimingAxisX);
        float movYaxis = Input.GetAxisRaw(aimingAxisY);

        Vector3 movForce = new Vector3(movXaxis, 0, movYaxis);

        transform.position = playerToFollow.transform.position + (movForce * aimingSensivity);
    }
}
