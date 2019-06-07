using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
	public int nbCharacters;
	private int _scene_index = 1;
	private bool[] _character_finished;

	private void Awake() 
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}
	void Start () 
	{
		_character_finished = new bool[nbCharacters];
		for (int i = 0; i < _character_finished.Length; i++)
		{
			_character_finished[i] = false;
		}

	}
	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(_scene_index, LoadSceneMode.Single);
	}
	public void CharacterFinished(int index, bool value)
	{
		_character_finished[index - 1] = value;
		for (int i = 0; i < _character_finished.Length; i++)
		{
			if (!_character_finished[i])
				return;
		}
		Debug.Log("Finish");
		if (_scene_index < SceneManager.sceneCountInBuildSettings - 1)
		{
			_scene_index++;
			SceneManager.LoadScene(_scene_index, LoadSceneMode.Single);
		}
	}
}
