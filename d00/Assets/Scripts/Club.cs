using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour 
{
	public float initial_speed;
	public Ball ball;
	public float top;
	public float bottom;
	private float actual_speed;
	private bool up_pos = false;
	private bool _push = false;
	private float _speed;
	private SpriteRenderer _render;
	private bool _useInput = true;

	void Start () 
	{
		actual_speed = initial_speed;
		_render = GetComponent<SpriteRenderer>();
	}

	void Update () 
	{
		if (_useInput && Input.GetKey(KeyCode.Space))
		{
			_push = true;
			PowerUp();
		}
		else if (_push)
		{
			_useInput = false;
			Shoot();
			//push = false
		}
	}
	public void ReplaceClub(Vector3 ball_pos, float holeY)
	{
		Vector3 tmp;

		tmp = transform.position;
		if ( ball_pos.y > holeY)
		{
			_render.flipY = true;
			tmp.y = ball_pos.y + 0.1f;
			up_pos = true;
		}
		else
		{
			up_pos = false;
			_render.flipY = false;
			tmp.y = ball_pos.y -= 0.1f;
		}
		_useInput = true;
		transform.position = tmp;
	}
	private void PowerUp()
	{
		Vector3 dir;

		dir = up_pos ? Vector3.up: Vector3.down;
		transform.Translate(dir * actual_speed * Time.deltaTime);
		actual_speed += Time.deltaTime * 15;
	}
	private void Shoot()
	{
		Vector3 dir;
		Vector3 tmp;
		float clampedY;
		float min;
		float max;

		dir = !up_pos ? Vector3.up: Vector3.down;
		min = transform.position.y;
		transform.Translate(dir * actual_speed * Time.deltaTime);
		tmp = transform.position;

		if (!up_pos)
		{
			max = ball.transform.position.y;
		}
		else
		{
			max = min;
			min = ball.transform.position.y;
		}
		clampedY = Mathf.Clamp(tmp.y, min, max);
		transform.position = tmp;
	 	if (clampedY == min || clampedY == max)
		{ 
			ball.Shoot(!up_pos, actual_speed * 4);
			_push = false;		
			actual_speed = initial_speed;
			return;
		}
		//actual_speed *= 1.1f;
	}
}
