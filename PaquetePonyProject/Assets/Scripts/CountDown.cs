using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    [SerializeField] private Text _countdown;
    [SerializeField] private int _countdownValue = 180;

    private float timeLapse;
    
    // Update is called once per frame
    void Update () {

        timeLapse += Time.deltaTime;

        if(timeLapse >= 1 && _countdownValue > 0)
        {
            _countdownValue -= 1;
            _countdown.text = _countdownValue.ToString();
            timeLapse = 0;
        }

        if (_countdownValue <= 0)
        {
            Container.eventSystem.Trigger(new GameOver());
        }
	}
}
