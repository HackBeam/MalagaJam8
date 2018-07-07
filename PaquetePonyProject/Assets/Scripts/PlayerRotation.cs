using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private GameObject aimingPointer;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (aimingPointer != null)
        {
            transform.LookAt(aimingPointer.transform);

            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
