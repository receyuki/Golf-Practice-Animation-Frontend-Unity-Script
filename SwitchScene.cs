////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: SwitchScene.cs
//Author : Zijie Yang
//Last Modified On : 30/10/2020
//Copy Rights : Copyright 2020
//Email : zijiey@student.unimelb.edu.au
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
     // Start is called before the first frame update
     void Start () {

     }

     // Update is called once per frame
     void Update () {

     }

     public void SwitchToDebug () {
          SceneManager.LoadScene ("DebugScene");
     }

     public void SwitchToMain () {
          SceneManager.LoadScene ("MainScene");
     }
}