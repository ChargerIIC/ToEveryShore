using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiController : MonoBehaviour
{
    //public GameObject StatPanel;
    private StatsManager stats;

	// Use this for initialization
	void Start ()
	{
	    //stats = StatPanel.GetComponent<StatsManager>();
	    stats = GetComponentInChildren<StatsManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NotifyOfUnitChange(GameObject pGameObject)
    {
        var figure = pGameObject.GetComponent<PlayableFigure>();//grab the figure class
        stats.Update(figure);
    }
}
