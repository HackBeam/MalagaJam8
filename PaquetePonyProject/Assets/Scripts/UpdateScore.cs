using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour {

    [SerializeField] private PlayerIdentifier player;
    private Text score = null;

    private int scoreValue = 0;
    

	// Use this for initialization
	void Awake () {
        score = gameObject.GetComponent<Text>();
	}
    private void Start()
    {
        Container.eventSystem.Subscribe<DeathEvent>(AddScore);
    }

    private void AddScore(DeathEvent obj)
    {
        if (obj.deadPlayerId != player.GetPlayerId())
        {
            scoreValue += 100;
            score.text = scoreValue.ToString();
        }
    }
}
