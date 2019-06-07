using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroyEvent : MonoBehaviour 
{
	public delegate void BuildingDestroyedAct();
	static public event BuildingDestroyedAct OnBuildingDestroyed;

	public static void Raise()
	{
		if (OnBuildingDestroyed != null)
			OnBuildingDestroyed();
	}
}
