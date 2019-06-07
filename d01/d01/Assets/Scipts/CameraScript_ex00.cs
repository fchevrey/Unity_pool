using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript_ex00 : MonoBehaviour 
{

	// Use this for initialization
	playerScript_ex00[] players;
	playerScript_ex00 _activePlayer;
	void Start () 
	{
		GameObject[] tmp;
		tmp = GameObject.FindGameObjectsWithTag("Player");
		players = new playerScript_ex00[tmp.Length];
		for (int i = 0; i < tmp.Length; i++)
		{
			players[i] = tmp[i].GetComponent<playerScript_ex00>();
			players[i].enabled = false;
		}
		if (players != null)
		{
			_activePlayer = players[0];
			_activePlayer.enabled = true;
		}
		
	}
	private void ChangePlayer(int index)
	{
		if (index >= players.Length)
			return;
		_activePlayer.enabled = false;
		_activePlayer = players[index];
		_activePlayer.enabled = true;
	}
	// Update is called once per frame
	void Update () 
	{
		Vector3 tmp;

		if (Input.GetKeyDown(KeyCode.Keypad1))
			ChangePlayer(0);
		else if (Input.GetKeyDown(KeyCode.Keypad2))
			ChangePlayer(1);
		else if (Input.GetKeyDown(KeyCode.Keypad3))
			ChangePlayer(2);
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(0, LoadSceneMode.Single);
		tmp = _activePlayer.transform.position;
		tmp.z = -1;
		transform.position = tmp;

	}
}
