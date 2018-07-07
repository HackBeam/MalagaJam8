using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerIdentifier : MonoBehaviour {

    
    [ValueDropdown("players")]
    [SerializeField] private int playerID;


    private static ValueDropdownList<int> players = new ValueDropdownList<int>()
    {
        {"Player0", RewiredConsts.Player.Player0},
        {"Player1", RewiredConsts.Player.Player1}
    };

    public int GetPlayerId()
    {
        return playerID;
    }
}
