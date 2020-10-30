////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: SetActive.cs
//Author : Zijie Yang
//Last Modified On : 30/10/2020
//Copy Rights : Copyright 2020
//Email : zijiey@student.unimelb.edu.au
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour {
    public GameObject gameObject;

    // Add the following function to the toggle's event
    public void SwitchActive () {
        if (!gameObject.activeSelf) {
            gameObject.SetActive (true);
            Debug.Log ("Active " + gameObject);
        } else {
            gameObject.SetActive (false);
            Debug.Log ("Deactive " + gameObject);
        }
    }
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}