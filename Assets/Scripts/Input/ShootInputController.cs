using System;
using System.Collections;
using System.Collections.Generic;
using MoenenGames.Shape;
using UnityEngine;

/// <summary>
/// Input controller for managing a figure's shooting phase
/// TODO: Consider making a common Input controller to inherit from
/// </summary>
public class ShootInputController : MonoBehaviour {

    #region Class Level Variables

    //Camera Rig so we can find the Camera Raycaster
    //public GameObject CameraRig;
    public GameObject RingPrefab;
    private PlayableFigure figure = null;
    private GameManager gameManager = null;
    private CameraRaycaster cameraRaycaster = null;
    private LineRenderer lineRenderer = null;
    //private GameObject ring;
    private List<GameObject> rangeRingStore = new List<GameObject>();
    #endregion Class Level Variables

    // Use this for initialization
    void Start ()
    {
        figure = gameObject.GetComponent<PlayableFigure>();
        gameManager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void OpenFire()
    {
        foreach (var weapon in figure.Weapons)
        {
            Debug.Log("Firing " + weapon.Name);
            gameManager.ResolveAttack(weapon);
        }
    }

    void OnMouseEnter()
    {
        foreach (var weapon in figure.Weapons)
        {
            var ring = Instantiate(RingPrefab, transform.position, Quaternion.identity);
            ring.transform.Rotate(new Vector3(90, 0, 0));
            var ringComponent = ring.GetComponent<Ring>();
            ringComponent.Thickness = 0.05f;
            ringComponent.Radius = weapon.EffectiveRange * Globals.WorldToGameFactor;
        }
    }

}
