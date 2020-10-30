////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: SwitchBLE.cs
//Author : Zijie Yang
//Last Modified On : 30/10/2020
//Copy Rights : Copyright 2020
//Email : zijiey@student.unimelb.edu.au
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBLE : MonoBehaviour {
    // Start is called before the first frame update
    void Awake () {
        GameObject.Find ("BLE").GetComponent<Toggle> ().isOn = FindObjectOfType<GlobalData> ().bleDebug;
        Debug.Log ("BLE debug " + FindObjectOfType<GlobalData> ().bleDebug);
    }

    void Start () {

    }

    public void SetBLE (bool isOn) {
        FindObjectOfType<GlobalData> ().bleDebug = GameObject.Find ("BLE").GetComponent<Toggle> ().isOn;
        Debug.Log ("BLE debug " + FindObjectOfType<GlobalData> ().bleDebug);
        Debug.Log ("Toggle " + GameObject.Find ("BLE").GetComponent<Toggle> ().isOn);
    }

    // Update is called once per frame
    void Update () {

    }
}