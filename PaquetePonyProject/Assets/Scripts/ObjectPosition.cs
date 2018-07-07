using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPosition : MonoBehaviour {

    void Update () {
        Shader.SetGlobalVector("_Position", transform.position);
	}
}
