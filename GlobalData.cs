using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour {
    private static GlobalData dataStorage;
    public bool testMode = false;
    public bool bleDebug = false;

    // Start is called before the first frame update

    void Awake () {
        FindObjectOfType<PathCreater> ().lineRenderer.positionCount = 0;

        FindObjectOfType<PathCreater> ().Idle ();
        Application.targetFrameRate = 60;
        if (dataStorage == null) {
            dataStorage = this; // In first scene, make us the singleton.
            DontDestroyOnLoad (gameObject);
        } else if (dataStorage != this)
            Destroy (gameObject); // On reload, singleton already set, so destroy duplicate.
    }

    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}