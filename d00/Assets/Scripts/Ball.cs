using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	public int initial_score;
	public float top;
	private int	_score;
	public float bottom;
	public float holeYmax;
	public float holeYmin;
	private bool _shoot = false;
	private float _speed;
	private Vector3 _dir;
	private bool _up;
	public Club club;
	public void Shoot(bool up, float speed)
	{
		_shoot = true;
		_speed = speed;
		_up = up;
		_score += 5;
		DisplayScore();
	}
	private void Start() 
	{
		_score = initial_score - 5;	
	}
	private void DisplayScore()
	{
		Debug.Log("Score = " + _score);
	}
	private void Move() 
	{
		Vector3 tmp;
		Vector3 dir;

		dir = _up ? Vector3.up : Vector3.down;
		transform.Translate(dir * _speed * Time.fixedDeltaTime);
		tmp = transform.position;
		if (_up)
		{
			if (tmp.y > top)
			{
				tmp.y = top - (tmp.y - top);
				_up = false;
			}
		}
		else
		{
			if (tmp.y < bottom)
			{
				tmp.y = bottom - (tmp.y - bottom);
				_up = true;
			}
		}
		_speed *= 0.95f;
		transform.position = tmp;
		if (tmp.y < holeYmax && tmp.y > holeYmin && _speed < 1.5f)
		{
			Debug.Log("FINISHED");
			DisplayScore();
			Destroy(this.gameObject);
		}
		if (_speed < 0.01f)
		{
			_shoot = false;
			club.ReplaceClub(tmp, holeYmax);
		}
	}
	private void FixedUpdate() 
	{
		if (_shoot)
			Move();
	}
}
