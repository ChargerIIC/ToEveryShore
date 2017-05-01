using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiController : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NotifyOfUnitChange(GameObject pGameObject)
    {
        var figure = pGameObject.GetComponent<FullFigure>();//grab the figure class
        Stats.Update(figure);

    }

    public void NotifyOfPlayerChange(PlayerId currentPlayer)
    {
        Commands.UpdatePlayer(currentPlayer);
    }

    //public void NotifyOfPhaseChange(TurnPhase currentPhase)
    //{
    //    Commands.UpdatePhase(currentPhase);
    //}

    private CommandManager commandManager;
    protected CommandManager Commands
    {
        get { return commandManager ?? (commandManager = GetComponentInChildren<CommandManager>()); }
    }

    private StatsManager statsManager;
    protected StatsManager Stats
    {
        get { return statsManager ?? (statsManager = GetComponentInChildren<StatsManager>()); }
    }

}
