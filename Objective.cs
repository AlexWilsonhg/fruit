using System.Collections.Generic;
using System;

public class ObjectiveListItem
{
	public Item item;
    public int itemCount;

	public ObjectiveListItem(Item _item, int _itemCount)
	{
		item = _item;
		itemCount = _itemCount;
	}
    
}

public class Objective
{
	public List<ObjectiveListItem> objectives = new List<ObjectiveListItem>();
	public float timeRemaining;

	public void Update(float deltaTime)
	{
		timeRemaining -= deltaTime;
	}

	public bool HasTimeRemaining()
	{
		if(timeRemaining > 0.0f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public int GetMinutesRemaining()
	{
		return (int) Math.Floor(timeRemaining / 60);
	}

	public int GetSecondsRemaining()
	{
		return (int) timeRemaining % 60;
	}
}

public static class ObjectiveGenerator
{
	public static Objective GetNewObjective(int difficulty)
	{
		Objective obj = new Objective();
		obj.timeRemaining = 90/difficulty;
		obj.objectives.Add(new ObjectiveListItem(new Banana(), 10));
		obj.objectives.Add(new ObjectiveListItem(new Apple(), 10));

		return obj;
	}
}