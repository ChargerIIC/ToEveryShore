﻿using System.Collections;
using System.Collections.Generic;
using MoenenGames.Shape;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class MoveInputController : MonoBehaviour {

    #region Class Level Variables

    //Prefab to create waypoints
    public GameObject WayPointPrefab;

    //Rangerfinder Ring for UI use
    public GameObject RingPreFab;

    private PlayableFigure figure = null;
    private CameraRaycaster cameraRaycaster = null;
    private AICharacterControl aiCharacterControl = null;
    private LineRenderer lineRenderer = null;

    public float moveToSpend;
    private bool moving;
    private float stopDistance = 0.1f;


    #endregion Class Level Variables

    #region Private Methods

    // Use this for initialization
    void Start () {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += ProcessMouseInput;
        aiCharacterControl = GetComponent<AICharacterControl>();
        figure = GetComponent<PlayableFigure>();
        moveToSpend = figure.Movement * Globals.WorldToGameFactor;
        lineRenderer = GetComponent<LineRenderer>();
        //TODO add to workflow later
        MovementRing.GetComponent<Circle>().Radius = figure.Movement * Globals.WorldToGameFactor;
    }

    // Update is called once per frame
    void Update ()
    {
        ProcessKeyboardInput();
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (moving)
        {
            var dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(WayPoint.transform.position.x, WayPoint.transform.position.z));
            if (dist <= stopDistance)
            {
                moving = false;
                aiCharacterControl.SetTarget(transform);
                lineRenderer.positionCount = 0;
                MovementRing.transform.position = new Vector3(WayPoint.transform.position.x, 0.5f, WayPoint.transform.position.z);
                MovementRing.GetComponent<Circle>().Radius = this.moveToSpend * Globals.WorldToGameFactor;

            }
            else
            {
                MoveToWaypoints();
            }
        }
    }

    //We only handle movement right now
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
            case (int)Layers.Friendly:
                var enemy = hitData.collider.gameObject; //TODO: PLace waypoint just outside the unit
                aiCharacterControl.SetTarget(enemy.transform);
                return;
            case (int)Layers.Passable:
                WayPoint.GetComponent<MeshRenderer>().material.color = Color.blue;
                WayPoint.transform.position = hitData.point;
                lineRenderer.positionCount = 2;
                lineRenderer.material.color = Color.blue;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, WayPoint.transform.position);
                return;
            case (int)Layers.UnPassable:
                //waypoint = GameObject.Instantiate(WayPointPrefab, hitData.point, Quaternion.identity, transform);
                WayPoint.GetComponent<MeshRenderer>().material.color = Color.red;
                WayPoint.transform.position = hitData.point;
                lineRenderer.positionCount = 2;
                lineRenderer.material.color = Color.red;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, WayPoint.transform.position);

                return;
            default:
                Debug.LogError("Unkown Layer");
                return;
        }
    }


    #endregion Private Methods

    #region Public Methods

    public void MoveToWaypoints()
    {
        moving = true;
        var dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(WayPoint.transform.position.x, WayPoint.transform.position.z));
        if (moveToSpend >= dist)
        {
            this.moveToSpend -= dist;
            aiCharacterControl.SetTarget(WayPoint.transform);
        }
        else
        {

            aiCharacterControl.SetTarget(WayPoint.transform);
        }
    }


    public void ProcessKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MoveToWaypoints();
        }
    }

    #endregion Public Methods

    #region Properties

    GameObject ring;
    protected GameObject MovementRing
    {
        get
        {
            if (ring == null)
            {
                ring = Instantiate(RingPreFab, transform.position, Quaternion.identity);
                ring.transform.Rotate(new Vector3(90, 0, 0));
                ring.GetComponent<Circle>().Radius = figure.Movement * Globals.WorldToGameFactor;
            }
            return ring;
        }
    }

    GameObject waypoint;
    protected GameObject WayPoint
    {
        get
        {
            if (waypoint == null)
            {
                waypoint = Instantiate(WayPointPrefab, transform.position, Quaternion.identity);
            }
            return waypoint;
        }
    }

    #endregion Properties
}
