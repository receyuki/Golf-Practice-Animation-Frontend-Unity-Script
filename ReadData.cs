////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: ReadData.cs
//Author : Zijie Yang
//Last Modified On : 30/10/2020
//Copy Rights : Copyright 2020
//Email : zijiey@student.unimelb.edu.au
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using UnityEngine;

public class ReadData : MonoBehaviour {
    public BallDataSet testData;
    private string testString;
    private TextAsset testFile;

    public BallData data = null;
    private string decodeData = "";
    private List<byte> rawData;
    private bool eof = false;

    // Start is called before the first frame update
    void Start () {
        if (FindObjectOfType<GlobalData> ().testMode) {
            testFile = Resources.Load<TextAsset> ("Data/testData");
            //testString = File.ReadAllText(Application.dataPath + "/Data/testData.json");
            testString = testFile.ToString ();
            testData = JsonUtility.FromJson<BallDataSet> (testString);
            Debug.Log ("Testdata length " + testData.length);
            Debug.Log ("Trajectory " + testData.data[0]);
        }
        rawData = new List<byte> ();
    }

    // Update is called once per frame
    void Update () {

    }

    //read data from packets
    public void ProcessData (byte[] receivedData) {
        if (receivedData[3] == 0x45 &&
            receivedData[4] == 0x4F &&
            receivedData[5] == 0x46) {
            eof = true;
            Debug.Log ("EOF");
            decodeData = Encoding.UTF8.GetString (Decompress (rawData.ToArray ())).Replace ("'", "\"");
            Debug.Log ("Receive data: " + decodeData);
            data = JsonUtility.FromJson<BallData> (decodeData);
            rawData = new List<byte> ();
            FindObjectOfType<PathCreater> ().NewPath ();
        } else {
            var fragment = receivedData.Skip (3).ToArray ();
            Debug.Log ("Fragment length: " + fragment.Length);
            rawData.AddRange (fragment);
        }
    }

    public byte[] Decompress (byte[] gzip) {
        Debug.Log ("1");
        using (GZipStream stream = new GZipStream (new MemoryStream (gzip), CompressionMode.Decompress)) {
            Debug.Log ("2");
            const int size = 10000;
            byte[] buffer = new byte[size];
            Debug.Log ("3");
            using (MemoryStream memory = new MemoryStream ()) {
                Debug.Log ("4");
                int count = 0;
                do {
                    Debug.Log ("5");
                    count = stream.Read (buffer, 0, size);
                    Debug.Log ("6");
                    if (count > 0) {
                        Debug.Log ("7");
                        memory.Write (buffer, 0, count);
                    }
                }
                while (count > 0);
                Debug.Log ("8");
                return memory.ToArray ();
            }
        }
    }
}

//data format
[System.Serializable]
public class BallData {
    public float speed;
    public float launchAngle;
    public float sideAngle;
    public float carry;
    public float peak;
    public List<Point> trajectory;
}

[System.Serializable]
public class BallDataSet {
    public List<BallData> data;
    public int length;
}

[System.Serializable]
public class Point {
    public float x;
    public float y;
    public float z;
}