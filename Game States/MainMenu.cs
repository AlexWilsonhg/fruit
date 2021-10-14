using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : GameState
{
	private Canvas UI;
	private Button playButton;
	private Button exitButton;
	private int selectedDifficulty = 1;

	public MainMenuState(GameManager _gameManager) : base(_gameManager)
	{
		UI = gameManager.mainMenu;

		playButton = UI.transform.Find("Play Button").GetComponent(typeof(Button)) as Button;
		playButton.onClick.AddListener(StartGame);

		exitButton = UI.transform.Find("Exit Button").GetComponent(typeof(Button)) as Button;
		exitButton.onClick.AddListener(ExitGame);

	}

	public override GameState Update()
	{
		GameState newState = null;

		return newState;
	}

	public override void OnEnter()
	{
		gameManager.cameraRig.SetLockTarget(GameObject.Find("Camera Lock Points/CameraLock_MainMenuEnd").transform);
		gameManager.cameraRig.SetSmoothing(100.0f);
		UI.enabled = true;
		Cursor.lockState  = CursorLockMode.None;
	}

	public override void OnExit()
	{
		UI.enabled = false;
	}

	public void StartGame()
	{
		gameManager.ChangeState(new IntroState(gameManager));
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
