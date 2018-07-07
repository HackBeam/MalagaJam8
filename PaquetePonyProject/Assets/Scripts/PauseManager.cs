using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PauseManager : MonoBehaviour {

    [SerializeField] private Image backImage;
    [SerializeField] private Text textPause;

    private bool state = false;

    [Button]
    public void SwapPauseState()
    {
        if (state == false)
        {
            backImage.gameObject.SetActive(true);
            textPause.gameObject.SetActive(true);
            Time.timeScale = 0;
            backImage.DOFade(0.5f,0.3f).SetUpdate(true);
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
