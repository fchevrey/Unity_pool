using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour 
{
	public Transform target;
	public Vector3 min;
	public Vector3 max;
	public float speed;
	private void Move(Vector3 dir)
	{
		transform.Translate(dir * speed * Time.deltaTime);
	}
	private void CheckBorder()
	{
		Vector3 tmp = transform.position;

		if (tmp.x < min.x)
			tmp.x = min.x;
		else if (tmp.x > max.x)
			tmp.x = max.x;
		if (tmp.y < min.y)
			tmp.y = min.y;
		else if (tmp.y > max.y)
			tmp.y = max.y;
		if (tmp.z < min.z)
			tmp.z = min.z;
		else if (tmp.z > max.z)
			tmp.z = max.z;
		transform.position = tmp;		
	}
	private void Update() 
	{
		if (Input.GetKey(KeyCode.W))
			Move(Vector3.forward);
		if (Input.GetKey(KeyCode.S))
			Move(-Vector3.forward);
		if (Input.GetKey(KeyCode.A))
			Move(Vector3.left);
		if (Input.GetKey(KeyCode.D))
			Move(-Vector3.left);
		if (Input.GetKey(KeyCode.Q))
			Move(Vector3.up);
		if (Input.GetKey(KeyCode.E))
			Move(-Vector3.up);
		transform.LookAt(target);	
	}
}
