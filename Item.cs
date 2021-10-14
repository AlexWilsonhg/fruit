using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    public Color color;

    protected Item(string _type)
	{
		type = _type;
	}

}

public class Banana : Item
{
	public Banana() : base("Banana") {}
}

public class Orange : Item
{
	public Orange() : base("Orange") {}
}

public class Apple : Item
{
	public Apple() : base("Apple") {}
}