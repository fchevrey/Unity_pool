using UnityEngine;

public class CameraScript_ex01 : MonoBehaviour 
{
	playerScript_ex01[] players;
	playerScript_ex01 _activePlayer;
	void Start () 
	{
		GameObject[] tmp;
		tmp = GameObject.FindGameObjectsWithTag("Player");
		players = new playerScript_ex01[tmp.Length];
		for (int i = 0; i < tmp.Length; i++)
		{
			players[i] = tmp[i].GetComponent<playerScript_ex01>();
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
	void Update () 
	{
		Vector3 tmp;

		if (Input.GetKeyDown(KeyCode.Keypad1))
			ChangePlayer(0);
		else if (Input.GetKeyDown(KeyCode.Keypad2))
			ChangePlayer(1);
		else if (Input.GetKeyDown(KeyCode.Keypad3))
			ChangePlayer(2);
		tmp = _activePlayer.transform.position;
		tmp.z = -1;
		transform.position = tmp;
	}
}
