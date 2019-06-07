using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour 
{
	public GameObject baloon;
	public float deflate_rate = 0.9f;
	public float inflate_rate = 1.1f;
	public float size_max;
	public float size_min;
	public float breath = 10;
	public bool out_of_breath;

	void Start () 
	{
	}
	private void FixedUpdate() 
	{
		if (baloon)
		{
			Deflate(baloon);
		}
		if (breath < 10.0f)
			breath += 0.11f;
		if (out_of_breath && breath > 0.0f)
		{
			breath = 10.0f;
			out_of_breath = false;			
		}
	}
	private void GameOver()
	{
		Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.time) + " s");
		Destroy(baloon);
	}
	private void Update () 
	{
		if (!baloon || out_of_breath)
		{
			return ;
		}
		float size = baloon.transform.localScale.x;
		if (Input.GetKeyDown(KeyCode.Space))
			Inflate(baloon);
		if (size > size_max || size < size_min)
			GameOver();
	}
	private void	Inflate(GameObject obj)
	{
		Vector3 tmp;
		tmp = obj.transform.localScale;
		tmp.x *= inflate_rate;
		tmp.y *= inflate_rate;
		tmp.z *= inflate_rate;
		obj.transform.localScale = tmp;
		breath -= 1;
		if (breath <= 0.0f)
		{
			breath = -10.0f;
			out_of_breath = true;
		}
	}
		private void	Deflate(GameObject obj)
	{
		Vector3 tmp;
		tmp = obj.transform.localScale;
		tmp.x *= deflate_rate;
		tmp.y *= deflate_rate;
		tmp.z *= deflate_rate;
		obj.transform.localScale = tmp;		
	}
}
