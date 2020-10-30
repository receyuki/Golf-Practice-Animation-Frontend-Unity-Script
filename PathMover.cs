using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour {
    public Queue<Vector3> pathPoints = new Queue<Vector3> ();
    public Vector3 point = new Vector3 (-80f, 1.2f, 130f);
    public float speed = 1f;
    private bool isUpdating = false;
    public float delayTimer = 1f;
    public float rotateFactor = 1000f;
    public float receiveTimer = 5f;

    private void Awake () {
        //FindObjectOfType<PathCreater>().OnNewPathCreated += SetPoints;

        transform.position = point;
        //        Debug.Log(Vector3.Distance(transform.position, point));
    }

    private void Start () {
        //SetPoints(FindObjectOfType<PathCreater>().points);
    }

    //private void SetPoints(IEnumerable<Vector3> points){
    public void SetPoints (List<Vector3> points) {
        //Debug.Log("Points count");
        Debug.Log (points.Count);
        pathPoints = new Queue<Vector3> (points);
    }

    // Update is called once per frame
    private void Update () {
        //Debug.Log("delayTimer "+delayTimer);

        //Debug.Log("Transform position "+transform.position);
        //Debug.Log("Next point "+point);
        //Debug.Log("Move towards "+Vector3.MoveTowards(transform.position, point, Time.deltaTime * speed));
        //Debug.Log("Distance "+Vector3.Distance(transform.position, point));
        //Debug.Log("Path points "+pathPoints.Count);
        if (isUpdating && delayTimer <= 0) {
            UpdatePathing ();
            transform.position = Vector3.MoveTowards (transform.position, point, Time.deltaTime * speed);
            transform.Rotate (0, 0.00001f * rotateFactor, 0, Space.Self);
        } else if (isUpdating) {
            delayTimer -= Time.deltaTime;
        }

        if (pathPoints.Count != 0) {
            isUpdating = true;
            //drawTimer += Time.deltaTime;
        } else if (Vector3.Distance (transform.position, point) <= 0.01 && pathPoints.Count == 0) {

            isUpdating = false;
            point = new Vector3 (-80f, 1f, 130f);
            delayTimer = 1f;
            receiveTimer = 5f;
        }
        if (receiveTimer <= 0 && isUpdating) {
            FindObjectOfType<PathCreater> ().Idle ();
        } else if (isUpdating) {
            receiveTimer -= Time.deltaTime;
        }

    }

    private void UpdatePathing () {

        if (Vector3.Distance (transform.position, point) <= 0.01 && pathPoints.Count != 0) {
            point = pathPoints.Dequeue ();
            Debug.Log ("Point updated");
            FindObjectOfType<PathCreater> ().DrawPath ();
        }

    }
}