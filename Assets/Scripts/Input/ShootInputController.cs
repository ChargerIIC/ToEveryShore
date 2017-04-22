using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input controller for managing a figure's shooting phase
/// TODO: Consider making a common Input controller to inherit from
/// </summary>
public class ShootInputController : MonoBehaviour {

    #region Class Level Variables

    //Camera Rig so we can find the Camera Raycaster
    public GameObject CameraRig;
    private PlayableFigure figure = null;
    private CameraRaycaster cameraRaycaster = null;
    private LineRenderer lineRenderer = null;

    #endregion Class Level Variables

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void OpenFire()
    {
        throw new NotImplementedException();
    }
}
