using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifier : MonoBehaviour {

    [SerializeField] private int _playerId = -1;

    public int GetPlayerId()
    {
        return _playerId;
    }
}
