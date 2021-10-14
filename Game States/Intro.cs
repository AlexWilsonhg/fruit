using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroState : GameState
{
	private float timeSinceLastCameraChange = 4.0f;
	private float timeBetweenCameraChanges = 2.5f;
	private List<Transform> cameraLockPoints = new List<Transform>();


	public IntroState (GameManager _gameManager) : base(_gameManager)
	{
		cameraLockPoints.Add(GameObject.Find("Camera Lock Points/CameraLock_Intro_Shelving").transform);
		cameraLockPoints.Add(GameObject.Find("Camera Lock Points/CameraLock_Intro_Conveyor").transform);
		cameraLockPoints.Add(GameObject.Find("Camera Lock Points/CameraLock_Intro_LoadingDock").transform);
		cameraLockPoints.Add(GameObject.Find("Camera Lock Points/CameraLock_Intro_PlayerFadeIn").transform);
	}

	public override GameState Update()
	{
		GameState newState = null;
		timeSinceLastCameraChange += Time.deltaTime;

		if(timeSinceLastCameraChange >= timeBetweenCameraChanges)
		{
			timeSinceLastCameraChange = 0.0f;
			if(cameraLockPoints.Count == 0)
			{
				newState = new MainGameState(gameManager, 1);
				return newState;
			}
			ChangeCameraLockPoint();
		}

		return newState;
	}


	public override void OnEnter()
	{
		gameManager.ResetAll();
		gameManager.cameraRig.SetSmoothing(50.0f);
	}

	public override void OnExit()
	{

	}

	private void ChangeCameraLockPoint()
	{
		Transform target = cameraLockPoints[0];
		cameraLockPoints.Remove(target);
		gameManager.cameraRig.SetLockTarget(target);
	}
}

