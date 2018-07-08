using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorAux : MonoBehaviour {

    private Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    public void OffShoot()
    {
        playerAnimator.SetBool("Disparar", false);
    }
}
