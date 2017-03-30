﻿using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;
using MoenenGames.Shape;

public class FigureInputManager : MonoBehaviour
{
    //Prefab to create waypoints
    public GameObject WayPointPrefab;
    //Camera Rig so we can find the Camera Raycaster
    public GameObject CameraRig;
    //Rangerfinder Ring for UI use
    public GameObject RingPreFab;

    private PlayableFigure figure = null;
    private CameraRaycaster cameraRaycaster = null;
    private AICharacterControl aiCharacterControl = null;
    private LineRenderer lineRenderer = null;

    public float moveToSpend;
    private bool moving;
    private float stopDistance = 0.1f;

    // Use this for initialization
    void Start ()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += ProcessMouseInput;
        aiCharacterControl = GetComponent<AICharacterControl>();
        figure = GetComponent<PlayableFigure>();
        moveToSpend = figure.Movement * Globals.WorldToGameFactor;
        lineRenderer = GetComponent<LineRenderer>();
        //TODO add to workflow later
        ActivateMovementMode();
    }

    // Update is called once per frame
    void Update ()
    {
        ProcessKeyboardInput();
        ProcessMovement();
	}

    private void ProcessKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            moving = true;
            var dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(WayPoint.transform.position.x, WayPoint.transform.position.z));
            this.moveToSpend -= dist;
            aiCharacterControl.SetTarget(waypoint.transform);
        }
    }

    private void ProcessMovement()
    {
        if (moving)
        {
            var dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(WayPoint.transform.position.x, WayPoint.transform.position.z));
            if(dist <= stopDistance)
            {
                moving = false;
                aiCharacterControl.SetTarget(transform);
                lineRenderer.numPositions = 0;
            }
        }
    }

    private void ActivateMovementMode()
    {
        var ring = Instantiate(RingPreFab, transform.position, Quaternion.identity);
        ring.transform.Rotate(new Vector3(90,0,0));
        ring.GetComponent<Ring>().Radius = figure.Movement * Globals.WorldToGameFactor;
    }

    //We only handle movement right now, but in the future there will be context sensitive options for targeting enemies
    /// <summary>
    /// Handles Mouse Clicks while in Move Mode
    /// </summary>
    /// <param name="hitData"></param>
    /// <param name="layerHit"></param>
    private void ProcessMouseInput(RaycastHit hitData, int layerHit)
    {
        switch (layerHit)
        {
            case (int)Layers.Enemy:
                var enemy = hitData.collider.gameObject; //TODO: PLace waypoint just outside the unit
                aiCharacterControl.SetTarget(enemy.transform);
                return;
            case (int)Layers.Passable:
                WayPoint.GetComponent<MeshRenderer>().material.color = Color.blue;
                WayPoint.transform.position = hitData.point;
                lineRenderer.numPositions = 2;
                lineRenderer.material.color = Color.blue;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, WayPoint.transform.position);
                //aiCharacterControl.SetTarget(waypoint.transform);
                return;
            case (int)Layers.UnPassable:
                //waypoint = GameObject.Instantiate(WayPointPrefab, hitData.point, Quaternion.identity, transform);
                WayPoint.GetComponent<MeshRenderer>().material.color = Color.red;
                WayPoint.transform.position = hitData.point;
                lineRenderer.numPositions = 2;
                lineRenderer.material.color = Color.red;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, WayPoint.transform.position);

                return;
            default:
                Debug.LogError("Unkown Layer");
                return;
        }
    }

    GameObject waypoint;
    protected GameObject WayPoint
    {
        get
        {
            if(waypoint == null)
            {
                waypoint = Instantiate(WayPointPrefab,transform.position, Quaternion.identity);
            }
            return waypoint;
        }
    }
}
