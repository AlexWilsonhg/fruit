using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipmentCheck : GameState
{
	private Canvas UI;
	private bool isSatisfied;
	private Text shipmentMessage;
	private Button playAgainButton;
	private Button exitButton;

	public ShipmentCheck(GameManager _gameManager, bool _isSatisfied) : base(_gameManager)
	{
		UI = GameObject.Find("UI/Shipment Check Screen").GetComponent(typeof(Canvas)) as Canvas;
		isSatisfied = _isSatisfied;

		shipmentMessage = UI.transform.Find("Shipment Status Text").GetComponent(typeof(Text)) as Text;

		playAgainButton = UI.transform.Find("Play Again Button").GetComponent(typeof(Button)) as Button;
		playAgainButton.onClick.AddListener(PlayAgain);

		exitButton = UI.transform.Find("Exit Button").GetComponent(typeof(Button)) as Button;
		exitButton.onClick.AddListener(ExitToMainMenu);

	}

	public override GameState Update()
	{
		GameState newState = null;

		return newState;
	}

	public override void OnEnter()
	{
		UI.enabled = true;
		Cursor.lockState  = CursorLockMode.None;

		if(isSatisfied == true)
		{
			shipmentMessage.text = "Shipment Delivered!";
			shipmentMessage.color = new Color(0,255,0,255);
		}
		else
		{
			shipmentMessage.text = "Shipment Failed";
			shipmentMessage.color = new Color(255,0,0,255);
		}
	}

	public override void OnExit()
	{
		UI.enabled = false;
	}

	private void ExitToMainMenu()
	{
		gameManager.ChangeState(new MainMenuState(gameManager));
	}

	private void PlayAgain()
	{
		gameManager.ChangeState(new MainGameState(gameManager, 1));
	}
}
