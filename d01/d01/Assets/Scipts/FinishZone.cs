using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour 
{
	public int playerIndex;
	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (!other.CompareTag("Player"))
			return ;
		playerScript_ex01 player = other.gameObject.GetComponent<playerScript_ex01>();
		if (player == null)
			return ;
		if (player.index == playerIndex)
			GameManager.instance.CharacterFinished(playerIndex, true);
	}
	private void OnTriggerExit2D(Collider2D other) 
	{
		if (!other.CompareTag("Player"))
			return ;
		playerScript_ex01 player = other.gameObject.GetComponent<playerScript_ex01>();
		if (player == null)
			return ;
		if (player.index == playerIndex)
			GameManager.instance.CharacterFinished(playerIndex, false);
	}

}
