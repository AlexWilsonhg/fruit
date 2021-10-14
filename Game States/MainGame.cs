using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameState : GameState
{
	private Canvas UI;
	private GameObject player;
	private List<Conveyor> conveyors = new List<Conveyor>();
	private PickupZone cratePickupZone;
	private int difficulty;
	private Objective objective;
	
	private Text timeRemainingDisplay;
	
	
	public MainGameState(GameManager _gameManager, int _difficulty) : base(_gameManager)
	{

		UI = gameManager.HUD;
		player = gameManager.player;
		conveyors = gameManager.conveyors;
		difficulty = _difficulty;
	
		objective = ObjectiveGenerator.GetNewObjective(difficulty);
		
		timeRemainingDisplay = UI.transform.Find("Countdown Value").GetComponent(typeof(Text)) as Text;
		cratePickupZone = GameObject.Find("Crate Pickup Zone").GetComponent(typeof(PickupZone)) as PickupZone;
		PlayerController controller = player.GetComponent(typeof(PlayerController)) as PlayerController;
		controller.Enable();

	}

	public override GameState Update()
	{
		GameState newState = null;

		if(Input.GetKeyDown("escape"))
		{
			//newState = new MainMenuState(gameManager);
		}

		if(objective.HasTimeRemaining())
		{
			objective.Update(Time.deltaTime);
			timeRemainingDisplay.text = objective.GetMinutesRemaining() + " : " + objective.GetSecondsRemaining();
			
		}
		else
		{
			bool shipmentSatisfied = cratePickupZone.VerifyObjective(objective);
			newState = new ShipmentCheck(gameManager, shipmentSatisfied);
			timeRemainingDisplay.text = " -- : -- ";
		}

		return newState;
	}

	public override void OnEnter()
	{
		gameManager.cameraRig.SetLockTarget(player.transform.Find("Head"));
		gameManager.cameraRig.SetSmoothing(0);
		Cursor.lockState = CursorLockMode.Locked;
		UI.enabled = true;
		player.SetActive(true);
	}

	public override void OnExit()
	{
		UI.enabled = false;
		PlayerController controller = player.GetComponent(typeof(PlayerController)) as PlayerController;
		controller.Disable();

	}
}