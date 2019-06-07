using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSTPlayer : MonoBehaviour 
{
	//[SerializeField] public AudioClip[] tracks;
	public AudioClip[] tracks;
	private AudioSource _player;
	private int _actualTrack = 0;
	private bool _play = true;
	// Use this for initialization
	void Start () 
	{
		_player = GetComponent<AudioSource>();
	}
	public bool PlayTrack(int track, bool loop)
	{
		_play = true;
		if (track < tracks.Length)
		{
			_player.clip = tracks[track];
			_player.loop = loop;
			_player.Play();
			return true;
		}
		return false;
	}
	private void FixedUpdate() 
	{
		if (_play && !_player.isPlaying)
		{
			PlayNext();
		}
	}
	private void PlayNext()
	{
		int nextTrack = Random.Range(0, tracks.Length);

		_play = true;
		if (_actualTrack == nextTrack)
			nextTrack = Random.Range(0, tracks.Length);
		_actualTrack = nextTrack;
		_player.clip = tracks[_actualTrack];
		_player.Play();
	}
	public void PlayTrack(AudioClip track, bool loop)
	{
		_play = true;
		_player.clip = track;
		_player.loop = loop;
		_player.Play();
	}
	public void Play() 
	{
		_play = true;
		_player.Play();
	}
	public void StopPlaying()
	{
		_play = false;
		_player.Stop();
	}
}
