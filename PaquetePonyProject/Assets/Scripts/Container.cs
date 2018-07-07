using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Botblins.EventSystems;


public class Container : MonoBehaviour {

    public static Container services = null;

    public static IGenericEventSystem eventSystem;

    // Use this for initialization
    void Awake()
    {
        if (services == null)
        {
            services = this;
        }
        else if (services != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Bootstrap();
    }

    private void Bootstrap()
    {
        eventSystem = new GenericEventSystem();
    }
}
