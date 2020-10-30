using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTest : MonoBehaviour {
    // Start is called before the first frame update
    void Awake () {
        GameObject.Find ("Test").GetComponent<Toggle> ().isOn = FindObjectOfType<GlobalData> ().testMode;
        Debug.Log ("Testmode " + FindObjectOfType<GlobalData> ().testMode);
    }

    void Start () {

    }

    public void SetTest (bool isOn) {
        FindObjectOfType<GlobalData> ().testMode = GameObject.Find ("Test").GetComponent<Toggle> ().isOn;
        Debug.Log ("Testmode " + FindObjectOfType<GlobalData> ().testMode);
        Debug.Log ("Toggle " + GameObject.Find ("Test").GetComponent<Toggle> ().isOn);
    }

    // Update is called once per frame
    void Update () {

    }
}