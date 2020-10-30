////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: SwitchDebug.cs
//Author : Zijie Yang
//Last Modified On : 30/10/2020
//Copy Rights : Copyright 2020
//Email : zijiey@student.unimelb.edu.au
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDebug : MonoBehaviour {
    public GameObject fps;
    private GameObject console;

    public void SwitchActive () {
        fps = GameObject.Find ("Plugin").transform.Find ("Graphy").gameObject;
        console = GameObject.Find ("Plugin").transform.Find ("IngameDebugConsole").gameObject;
        if (!fps.activeSelf) {
            fps.SetActive (true);
            console.SetActive (true);
            Debug.Log ("Debug active");
        } else {
            fps.SetActive (false);
            console.SetActive (false);
            Debug.Log ("Debug deactive");
        }
    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}