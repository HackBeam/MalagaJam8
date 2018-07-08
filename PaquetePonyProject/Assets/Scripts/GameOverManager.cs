using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    [SerializeField] private Image backImage;
    [SerializeField] private Text textTimer;
    [SerializeField] private Text textButton;

    // Use this for initialization
    void Start () {
        Container.eventSystem.Subscribe<GameOver>(ManageGameOver);
	}

    private void ManageGameOver(GameOver obj)
    {
        backImage.gameObject.SetActive(true);
        textTimer.gameObject.SetActive(true);
        Time.timeScale = 0;
        backImage.DOFade(0.5f, 0.3f).SetUpdate(true);
        textTimer.DOFade(1f, 0.4f).SetUpdate(true);
        StartCoroutine(WaitAnyButton());
    }

    private IEnumerator WaitAnyButton()
    {
        yield return new WaitForSecondsRealtime(1);
        textButton.gameObject.SetActive(true);
        textButton.DOFade(1f, 0.4f).SetUpdate(true);

        while (! Input.anyKeyDown)
        {
            yield return null;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
