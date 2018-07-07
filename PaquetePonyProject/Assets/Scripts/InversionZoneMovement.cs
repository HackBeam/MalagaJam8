using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InversionZoneMovement : MonoBehaviour {

    [SerializeField] private GameObject _waypointsParent;
    [SerializeField] private float pathDuration = 40;
    private List<Transform> transforms = null;
    private List<Vector3> positions = null;
    // Use this for initialization
    void Start ()
    {
        transform.position = new Vector3(125, 0, 125);
        positions = new List<Vector3>();
        transforms = new List<Transform>();
        transforms.AddRange(_waypointsParent.GetComponentsInChildren<Transform>());
        for (int i = 1; i < transforms.Count; i++)
        {
            positions.Add(transforms[i].position);
        }
        transform.DOPath(positions.ToArray(), pathDuration).SetLoops(-1).SetEase(Ease.Linear);

    }

}
