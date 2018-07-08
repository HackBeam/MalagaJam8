using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject initobj;

	void Start ()
    {
        StartCoroutine(MainMenuCoro());
	}

    private IEnumerator MainMenuCoro()
    {

        while (! Input.anyKeyDown)
        {
            yield return null;
        }

        initobj.SetActive(false);

        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        SceneManager.LoadScene("Jny");
    }
}
