using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    public GameObject MarkerPrefab;
    public Material goodTarget;
    public Material outOfRange;

    private GameObject markerObject;
    private MeshRenderer markedObjectRenderer;
    private LineRenderer pathToTarget;

    private float maxRange;
    bool isDragging = false;
    private Vector3 originPoint;
    List<Vector3> wayPoints;
    
    // Use this for initialization
    void Start ()
    {
        markerObject = GameObject.Instantiate(MarkerPrefab, Vector3.zero, Quaternion.identity, transform);
        markedObjectRenderer = markerObject.GetComponent<MeshRenderer>();
        pathToTarget = gameObject.GetComponent<LineRenderer>();
        var figure = gameObject.GetComponent<PlayableFigure>();
        maxRange = figure.Movement * 4;
        originPoint = transform.position;
        wayPoints = new List<Vector3>
        {
            transform.position
        };
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit; // used as an out parameter
            bool hasHit = Physics.Raycast(ray, out hit, 1000f);
            if (hasHit)
            {
                if (!isDragging)
                {
                    isDragging = true;
                    //originPoint = transform.position;
                }
                markerObject.transform.position = hit.point;
                var distance = Vector3.Distance(transform.position, markerObject.transform.position);

                if (distance < maxRange)
                {
                    markedObjectRenderer.material = goodTarget;
                    pathToTarget.material = goodTarget;
                }
                else
                {
                    markedObjectRenderer.material = outOfRange;
                    pathToTarget.material = outOfRange;
                }
                var pos = new Vector3(markerObject.transform.position.x, 0, markerObject.transform.position.z);
                print(pos);
                drawLine(pos);
            }
        }
        else if (isDragging && Input.GetMouseButtonUp(0))
        {
            var pos = new Vector3(markerObject.transform.position.x, 0, markerObject.transform.position.z);
            var waypoint = Instantiate(MarkerPrefab,pos, Quaternion.identity, transform);
            var distance = Vector3.Distance(transform.position, waypoint.transform.position);
            originPoint = pos;

            wayPoints.Add(pos);
            maxRange -= distance;
            isDragging = false;
            drawLine(Vector3.zero);
        }

    }

    private void drawLine(Vector3 tempPoint)
    {
        int counter = 0;
        int verticies;
        if(tempPoint == Vector3.zero)
        {
            verticies = wayPoints.Count;
        }
        else
        {
            verticies = wayPoints.Count + 1;
        }

        pathToTarget.numPositions = verticies;
        foreach(var vertex in wayPoints)
        {
            print("Setting Postion #" + counter + " at " + vertex);
            pathToTarget.SetPosition(counter, vertex);
            counter++;
        }

        if (tempPoint != Vector3.zero)
        {
            pathToTarget.SetPosition(counter, tempPoint);
        }
    }
}
