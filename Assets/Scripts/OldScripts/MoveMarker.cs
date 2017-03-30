using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarker : MonoBehaviour {

    //public GameObject MarkedObject;
    public GameObject MarkerPrefab;
    private GameObject markerObject;

    public Material goodTarget;
    public Material outOfRange;

    private Renderer markedObjectRenderer;
    private LineRenderer pathToTarget;

    private float maxRange;
    private Vector3 originPoint;
	// Use this for initialization
	void Start ()
    {
        markerObject = GameObject.Instantiate(MarkerPrefab, Vector3.zero, Quaternion.identity, transform);
        markedObjectRenderer = markerObject.GetComponent<MeshRenderer>();
        pathToTarget = markerObject.GetComponent<LineRenderer>();
        var figure = gameObject.GetComponent<PlayableFigure>();
        maxRange = figure.Movement * 4;
    }

    bool isDragging = false;
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
                    originPoint = transform.position;
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
                pathToTarget.SetPosition(0, markerObject.transform.position);
                pathToTarget.SetPosition(1, transform.position);
            }
        }
        else if(isDragging && Input.GetMouseButtonUp(0))
        {
            var waypoint = Instantiate(MarkerPrefab, markerObject.transform.position, Quaternion.identity, transform);

        }
	}
}
