////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: ResetGame.cs
//Author : Zijie Yang
//Last Modified On : 30/10/2020
//Copy Rights : Copyright 2020
//Email : zijiey@student.unimelb.edu.au
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResetGame : MonoBehaviour {
    public LineRenderer lineRenderer;
    public GameObject ball;

    public void Reset () {
        lineRenderer.positionCount = 0;
        ball.transform.position = new Vector3 (-80f, 1.2f, 130f);
        ball.transform.rotation = Quaternion.Euler (0, 0, 0);
        FindObjectOfType<PathMover> ().pathPoints = new Queue<Vector3> ();
        FindObjectOfType<PathMover> ().point = new Vector3 (-80f, 1f, 130f);
        FindObjectOfType<PathCreater> ().speedText.text = "0 mph";
        FindObjectOfType<PathCreater> ().launchAngleText.text = "0°";
        FindObjectOfType<PathCreater> ().sideAngleText.text = "0°R";
        FindObjectOfType<PathCreater> ().carryText.text = "0 yds";
        FindObjectOfType<PathCreater> ().peakText.text = "0 yds";
        if (FindObjectOfType<GlobalData> ().testMode) {
            FindObjectOfType<PathCreater> ().Idle ();
        }
    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}