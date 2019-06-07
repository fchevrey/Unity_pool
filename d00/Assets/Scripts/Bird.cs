using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour 
{
	public float falling_speed;
	bool fly = false;
	private int _countdown;
	public float fly_intensity;
	public float ymin;
	public float ymax;
	private bool _over = false;
	public Pipe obstacle;
	public Vector2 halfsize;
	private void FixedUpdate() 
	{
		if (_over)
			return;
		Vector3 dir;
		Vector3 tmp;

		dir = fly ? Vector3.up : Vector3.down;
		transform.Translate(dir * falling_speed * Time.fixedDeltaTime);
		tmp = transform.position;
		if (tmp.y < ymin)
			tmp.y = ymin;
		else if (tmp.y > ymax)
			tmp.y = ymax;
		transform.position = tmp;
		if (fly)
		{
			_countdown--;
			if (_countdown <= 0)
				{fly = false;}
		}
		CheckCollision();
	}
	private void 	CheckCollision()
	{
		Vector3 birdPos = transform.position;
		Vector3 pipePos = obstacle.transform.position;

		if ((birdPos.x + halfsize.x) < (pipePos.x - obstacle.halfSize) || (birdPos.x - halfsize.x) > (pipePos.x + obstacle.halfSize))
		{
			return;
		}
		if ((birdPos.y + halfsize.y) > obstacle.upPipY || (birdPos.y - halfsize.y) < obstacle.downPipY)
		{
			falling_speed = 0.0f;
			obstacle.GameOver();
			_over = true;
		}

	}
	private void Fly()
	{
		fly = true;
		_countdown = 10;
	}
	private void Update() 
	{
		if (_over)
			return ;
		if (Input.GetKeyDown(KeyCode.Space))
			Fly();
	}
}
