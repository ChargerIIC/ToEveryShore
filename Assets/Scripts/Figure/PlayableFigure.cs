using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #endregion Stats

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
