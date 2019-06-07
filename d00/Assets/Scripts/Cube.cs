using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour 
{
	public float speed;
	private Vector3 start_pos;
	// Use this for initialization
	void Start () 
	{
		start_pos = transform.position;
	}
	public void ReturnTop()
	{
		transform.position = start_pos;
	}
	private void FixedUpdate() 
	{
		Vector3 tmp = this.transform.position;

		tmp.y -= speed;
		this.transform.position = tmp;
	}
}
