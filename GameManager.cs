using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas HUD;
    public GameObject player;
    public CameraRig cameraRig;
    public List<Conveyor> conveyors = new List<Conveyor>();
    private GameState activeState;

    private void Start()
    {
    	mainMenu.enabled = false;
    	HUD.enabled = false;
        player.SetActive(true);
    	foreach(Conveyor c in conveyors)
    	{
    		c.StartConveyor();
    	}
    	activeState = new MainMenuState(this);
    	activeState.OnEnter();

    }

    private void Update()
    {
    	GameState newState = activeState.Update();
    	if(newState != null)
    	{
    		ChangeState(newState);
    	}
    }

    public void ChangeState(GameState state)
    {
    	activeState.OnExit();
    	activeState = state;
    	activeState.OnEnter();
    }

    public void ResetAll()
    {
        var resetables = FindObjectsOfType(typeof(iResetable));
        foreach(iResetable obj in resetables)
        {
            obj.Reset();
        }   
    }
}