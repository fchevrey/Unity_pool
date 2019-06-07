using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfParcours : MonoBehaviour 
{
	public Transform startPos;
	public GameObject flag;
	public int par;
	private void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag(Tags.Ball))
		{
			GameManager.instance.Goal();
		}
	}
}
