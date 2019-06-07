using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEvent : MonoBehaviour 
{
	public delegate void GameOverAct();
	static public event GameOverAct OnGameOver;

	public static void Raise()
	{
		if (OnGameOver != null)
			OnGameOver();
	}
}
