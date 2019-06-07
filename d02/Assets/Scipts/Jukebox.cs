using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour 
{
	//[SerializeField] public AudioClip[] tracks;
	public AudioClip[] tracks;
	private AudioSource _player;
	// Use this for initialization
	void Start () 
	{
		_player = GetComponent<AudioSource>();
	}
	public bool PlayTrack(int track, bool loop)
	{
		if (track < tracks.Length)
		{
			_player.clip = tracks[track];
			_player.Play();
			_player.loop = loop;
			return true;
		}
		return false;
	}
	public void PlayTrack(e_UnitSound track, bool loop)
	{
		int index = 0;

		if (track == e_UnitSound.acknowledge)
		{
			index = Random.Range(0, 4);
		}
		else if (track == e_UnitSound.die)
		{
			index = 4;
		}
		else if (track == e_UnitSound.help)
		{
			index = Random.Range(5, 7);
		}
		else if (track == e_UnitSound.selected)
		{
			index = Random.Range(7, 15);
		}
		PlayTrack(index, loop);
	}
	public void StopPlaying()
	{
		_player.Stop();
	}
}
