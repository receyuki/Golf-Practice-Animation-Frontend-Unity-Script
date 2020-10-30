////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PathCreater.cs
//Author : Zijie Yang
//Last Modified On : 30/10/2020
//Copy Rights : Copyright 2020
//Email : zijiey@student.unimelb.edu.au
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PathCreater : MonoBehaviour {
    public LineRenderer lineRenderer;
    public List<Vector3> points = new List<Vector3> ();
    public float testTimer = 0;
    //public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate {};
    private BallDataSet testData;
    private BallData data;
    public float speed;
    public float launchAngle;
    public float sideAngle;
    public float carry;
    public float peak;
    public float coe = 0.75f;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI launchAngleText;
    public TextMeshProUGUI sideAngleText;
    public TextMeshProUGUI carryText;
    public TextMeshProUGUI peakText;
    private GameObject idle;
    private GameObject receive;
    private GameObject connecting;

    //private float drawTimer = 0;
    private int drawIndex = 1;

    private void Awake () {
        lineRenderer = GetComponent<LineRenderer> ();
        speedText = GameObject.Find ("Speed").GetComponent<TextMeshProUGUI> ();
        launchAngleText = GameObject.Find ("LaunchAngle").GetComponent<TextMeshProUGUI> ();
        sideAngleText = GameObject.Find ("SideAngle").GetComponent<TextMeshProUGUI> ();
        carryText = GameObject.Find ("Carry").GetComponent<TextMeshProUGUI> ();
        peakText = GameObject.Find ("Peak").GetComponent<TextMeshProUGUI> ();
        idle = GameObject.Find ("Idle");
        receive = GameObject.Find ("Receive");
        connecting = GameObject.Find ("Connecting");
    }

    void Update () {
        if (FindObjectOfType<GlobalData> ().testMode) {

            //Debug.Log("drawIndex "+drawIndex+",points.Count "+points.Count+",drawTimer "+drawTimer);
            //if (drawIndex < points.Count && drawTimer >= ((float)drawIndex*(1f/(float)points.Count))){
            //    drawIndex += 1;

            //    Debug.Log("drawIndex "+drawIndex);
            //    Debug.Log("drawTimer "+drawTimer);

            //    lineRenderer.positionCount = drawIndex;
            //    lineRenderer.SetPositions(points.ToArray());
            //}
            //else if (drawIndex < points.Count){
            //    drawTimer += Time.deltaTime;
            //}
            //Debug.Log("Test Timer "+testTimer);
            if (testTimer <= 0) {
                if (FindObjectOfType<SwitchCamera> ().cams[4].enabled) {
                    FindObjectOfType<SwitchCamera> ().cams[4].enabled = false;
                    FindObjectOfType<SwitchCamera> ().FreeView ();
                }
                FindObjectOfType<ResetGame> ().Reset ();
                Receive ();
                CreatePath ();
                FindObjectOfType<PathMover> ().SetPoints (points);
                testTimer = 20;
            } else {
                testTimer -= Time.deltaTime;
            }
        }

    }

    public void NewPath () {
        if (FindObjectOfType<SwitchCamera> ().cams[4].enabled) {
            FindObjectOfType<SwitchCamera> ().cams[4].enabled = false;
            FindObjectOfType<SwitchCamera> ().FreeView ();
        }
        FindObjectOfType<ResetGame> ().Reset ();
        CreatePath ();
        FindObjectOfType<PathMover> ().SetPoints (points);
    }

    private void CreatePath () {
        Debug.Log ("Creating path");
        points.Clear ();
        if (FindObjectOfType<GlobalData> ().testMode) {
            ReadTestPoints ();
        } else {
            ReadPoints ();
        }
        //lineRenderer.positionCount = points.Count;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPositions (points.ToArray ());
        //drawTimer = 0;
        drawIndex = 0;
        //Debug.Log("Path created");
        Debug.Log ("Point count " + points.Count);
        //OnNewPathCreated(points);
        speedText.text = speed.ToString ("0.0") + " mph";
        launchAngleText.text = launchAngle.ToString ("0.0") + "°";
        sideAngleText.text = (sideAngle >= 0 ? sideAngle.ToString ("0.0") + "°R" : (-sideAngle).ToString ("0.0") + "°L");
        carryText.text = carry.ToString ("0.0") + " yds";
        peakText.text = peak.ToString ("0.0") + " yds";
        FindObjectOfType<PathMover> ().rotateFactor = carry * peak;
        GameObject.Find ("TopCamera").transform.position = new Vector3 (carry / 2f - 80f, 2.5f * peak, 120f);
        GameObject.Find ("BackCamera").transform.position = new Vector3 (-120f, peak * 0.6f, 130f);
        GameObject.Find ("BackCamera").GetComponent<Camera> ().orthographicSize = peak * 0.75f;
        GameObject.Find ("SideCamera").transform.position = new Vector3 (carry / 2f - 80f, peak / 2f, 200f);
        GameObject.Find ("SideCamera").GetComponent<Camera> ().orthographicSize = Mathf.Max (peak * 0.75f, carry * 0.3f);
    }

    public void DrawPath () {
        drawIndex += 1;

        //Debug.Log("drawIndex "+drawIndex);
        //Debug.Log("drawTimer "+drawTimer);

        lineRenderer.positionCount = drawIndex;
        lineRenderer.SetPositions (points.ToArray ());
    }

    private void ReadPoints () {
        Debug.Log ("Reading points");
        data = FindObjectOfType<ReadData> ().data;
        if (FindObjectOfType<ReadData> ().data != null) {
            foreach (Point cood in data.trajectory) {
                Vector3 point = new Vector3 (cood.x * coe - 80f, cood.z * coe + 1.2f, cood.y * coe + 130f);
                points.Add (point);
                Debug.Log (point);
            }
            speed = data.speed;
            launchAngle = data.launchAngle;
            sideAngle = data.sideAngle;
            carry = data.carry;
            peak = data.peak;
        }

    }

    private void ReadTestPoints () {
        Debug.Log ("Reading test points");
        //        using(StreamReader reader = new StreamReader(Application.dataPath+"/Data/sparabola.csv")){
        //            Debug.Log("Found test points");
        //            while(!reader.EndOfStream){
        //                var line = reader.ReadLine();
        //                var values = Array.ConvertAll(line.Split(','), float.Parse);
        //                Vector3 point = new Vector3(values[0]-80f,values[2]+1f,values[1]+130f);
        //                points.Add(point);
        //                Debug.Log(point);
        //            }
        //        }
        if (FindObjectOfType<ReadData> ().testData != null) {
            testData = FindObjectOfType<ReadData> ().testData;
            Debug.Log (testData);
            int randData = UnityEngine.Random.Range (0, testData.length);
            Debug.Log ("Test data chosen #" + randData);
            Debug.Log (testData.data[randData]);
            Debug.Log (testData.data[randData].trajectory);
            Debug.Log (testData.data[randData].speed);
            Debug.Log ("Data length " + testData.data[randData].trajectory.Count);

            foreach (Point cood in testData.data[randData].trajectory) {
                Vector3 point = new Vector3 (cood.x - 80f, cood.z + 1.2f, cood.y + 130f);
                points.Add (point);
                Debug.Log (point);
            }
            speed = testData.data[randData].speed;
            launchAngle = testData.data[randData].launchAngle;
            sideAngle = testData.data[randData].sideAngle;
            carry = testData.data[randData].carry;
            peak = testData.data[randData].peak;
        }

    }

    public void Idle () {
        idle.SetActive (true);
        receive.SetActive (false);
        connecting.SetActive (false);
    }

    public void Receive () {
        idle.SetActive (false);
        receive.SetActive (true);
        connecting.SetActive (false);
    }

    public void Connecting () {
        idle.SetActive (false);
        receive.SetActive (false);
        connecting.SetActive (true);
    }

}