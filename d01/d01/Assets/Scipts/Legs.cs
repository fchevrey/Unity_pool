using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour 
{
	playerScript_ex01 player;
	private void OnTriggerEnter2D(Collider2D other) 
	{
		player.ResetJump();
	}
	// Use this for initialization
	void Start () 
	{
		player = transform.parent.gameObject.GetComponent<playerScript_ex01>();
	}
}
