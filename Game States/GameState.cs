using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
	protected GameState(GameManager _gameManager)
	{
		gameManager = _gameManager;
	}

	protected GameManager gameManager;

	public virtual GameState Update() { return null; }
	public virtual void OnEnter() {}
	public virtual void OnExit() {}	
}

