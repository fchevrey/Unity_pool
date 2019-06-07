using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour 
{
	public float speed;
	public float xmin;
	public float xmax;
	private int _countdown;
	public float halfSize;
	private int _score;
	public float upPipY;
	public float downPipY;
	private bool _over = false;
    public void GameOver() 
	{	
	 	speed = 0.0f;
		_over = true;
		 Debug.Log("Score : " + _score);
		 Debug.Log("Time : " + Mathf.RoundToInt(Time.time) + " s");
	}
	private void Start() 
	{
		_score = 0;		
	}
    private void Move() 
	{
		Vector3 tmp;

		transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);
		tmp = transform.position;
		if (tmp.x < xmin)
		{
			tmp.x = xmax;
			_countdown = 15;
			speed += 0.5f;
			_score += 5;
		}
		transform.position = tmp;
	}
	private void FixedUpdate() 
	{
		if (_over)
			return ;
		if (_countdown <= 0)
		{
			Move();
		}
		else
		{
			_countdown--;
		}
	}
}
