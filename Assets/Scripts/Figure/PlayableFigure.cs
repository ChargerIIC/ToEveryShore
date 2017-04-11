using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayableFigure : MonoBehaviour {

    #region Fluff

    public string VehicleName = "";
    public string CommanderName = "";
    //TODO: Add Faction Enums (informational only)
    public string Model = "";
    public string Chassis = "";
    //TODO: Unit Type

    #endregion Fluff

    #region Stats

    //Movement
    public MoveType MovementType = MoveType.HalfTrack;
    public int Movement = 0;

    //Defensive
    public int Morale = 1;
    public int ToHit = 1;
    public int FrontArmor = 10;
    public int SideArmor = 10;
    public int RearArmor = 10;

    //This is a hidden stat used to track terrain movement costs
    private float movementCost = 1.0f;
    #endregion Stats

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //TODO: We need to determine how to modify the navmesh for the custom terrain areas
        //NavMeshHit navMeshHit;
        //var pos = new Vector3(transform.position.x, 0, transform.position.z);
        //if (NavMesh.SamplePosition(pos, out navMeshHit, 1.0f, NavMesh.AllAreas))
        //{
        //    print("Nav Mesh: " + navMeshHit.mask);
        //}
    }
}
