////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: DrapdownCamera.cs
//Author : Zijie Yang
//Last Modified On : 30/10/2020
//Copy Rights : Copyright 2020
//Email : zijiey@student.unimelb.edu.au
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrapdownCamera : MonoBehaviour {
    private SwitchCamera cameraSwitcher;
    // Start is called before the first frame update
    void Awake () {
        cameraSwitcher = GameObject.Find ("CameraSwitcher").GetComponent<SwitchCamera> ();

    }

    void Start () { }

    public void SwitchCamera (int value) {
        switch (value) {
            case 0:
                cameraSwitcher.FreeView ();
                break;
            case 1:
                cameraSwitcher.TopView ();
                break;
            case 2:
                cameraSwitcher.BackView ();
                break;
            case 3:
                cameraSwitcher.SideView ();
                break;
        }
    }

    // Update is called once per frame
    void Update () {

    }
}