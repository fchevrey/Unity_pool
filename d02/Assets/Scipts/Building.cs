using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Element 
{
	private void OnDestroy() 
	{
		BuildingDestroyEvent.Raise();	
	}
}
