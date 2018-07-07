using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using Rewired;

public class PauseManager : MonoBehaviour
{

    [SerializeField] private Image backImage;
    [SerializeField] private Text textPause;

    private bool state = false;

    private void Awake()
    {
        SubscribeInput();
    }

    private void SubscribeInput()
    {
        IList<Player> players = ReInput.players.GetPlayers();

        for (int i = 0; i < players.Count; i++)
        {
            players[i].AddInputEventDelegate(SwapPauseState, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, RewiredConsts.Action.Pause);
        }
        
    }

    private void SwapPauseState(InputActionEventData data)
    {
        SwapPauseState();
    }

    [Button]
    public void SwapPauseState()
    {
        if (state == false)
        {
            backImage.gameObject.SetActive(true);
            textPause.gameObject.SetActive(true);
            Time.timeScale = 0;
            backImage.DOFade(0.5f, 0.3f).SetUpdate(true);
            textPause.DOFade(1f, 0.4f).SetUpdate(true);
            state = true;
        }
        else
        {
            Time.timeScale = 1;
            backImage.DOFade(0, 0.4f);
            textPause.DOFade(0, 0.3f).SetUpdate(true);
            backImage.gameObject.SetActive(false);
            textPause.gameObject.SetActive(false);
            state = false;
        }
    }
}
