using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{

	private Image img;
	private PlayerIdentifier playerID;

    private void Awake()
    {
		playerID = GetComponentInParent<PlayerIdentifier>();
		img = GetComponent<Image>();
        Container.eventSystem.Subscribe<HealthChangedEvent>(UpdateFillAmmount);
    }

    private void UpdateFillAmmount(HealthChangedEvent data)
    {
		if (data.playerId == playerID.GetPlayerId())
		{
			img.fillAmount = data.newCurrentHealthPercent;
		}
    }
}
