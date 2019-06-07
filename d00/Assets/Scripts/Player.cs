using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	public int score {get; private set; }
	public bool isPlayer1;
    public float speed;
    private float Ymax = 3.5f;
    private float Ymin = -3.5f;

	public void incr_score(int amount)
	{
		score += amount;
	}
    private void Move(bool up)
    {
        Vector3 dir = up ? Vector3.up : Vector3.down;
        Vector3 tmp;

        transform.Translate(dir * Time.deltaTime * speed);
        tmp = transform.position;
        if (tmp.y < Ymin)
            tmp.y = Ymin;
        else if (tmp.y > Ymax)
            tmp.y = Ymax;
        transform.position = tmp;
    }
    void Update () 
	{
		if (isPlayer1)
		{
            if (Input.GetKey(KeyCode.W))
                Move(true);
			if (Input.GetKey(KeyCode.S))
                Move(false);
		}
		else
		{
			if (Input.GetKey(KeyCode.UpArrow))
                Move(true);
			if (Input.GetKey(KeyCode.DownArrow))
                Move(false);
		}	
	}
}
