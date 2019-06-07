using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour 
{
	public Vector2 min;
	public Vector2 max;
	public float speed;
	private Vector3 _dir;
	private void FixedUpdate() 
	{
		Move();
	}
	private void Move() 
	{
		Vector3 tmp;

		transform.Translate(_dir * speed * Time.fixedDeltaTime);
		tmp = transform.position;
		if (tmp.y < min.y || tmp.x < min.x)
		{	
			tmp = min;
			_dir = max - min;
			_dir.Normalize();
		}
		else if (tmp.y > max.y || tmp.x > max.x)
		{	
			tmp = max;
			_dir = min - max;
			_dir.Normalize();
		}
		transform.position = tmp;
	}
	void Start () 
	{
		Vector2 tmpDir;

		tmpDir = max - min;
		_dir = new Vector3(tmpDir.x, tmpDir.y, 0);
		_dir.Normalize();
	}
	
}
