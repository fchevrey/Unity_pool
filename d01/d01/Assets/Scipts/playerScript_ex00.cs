using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour {

	//public int enable_value;
	public float speed;
	private float _jumpForce = 5;
	private Rigidbody2D _rigid;

	private void Start() 
	{
		_rigid = GetComponent<Rigidbody2D>();
	}
	private void Move(Vector3 dir)
	{
		transform.Translate(dir * speed * Time.deltaTime);
	}
	void Update () 
	{
		float x;

		x = Input.GetAxis("Horizontal");
		if (Input.GetKeyDown(KeyCode.Space))
			_rigid.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
		Move(new Vector3(x, 0, 0));
	}
}
