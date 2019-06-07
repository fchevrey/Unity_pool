using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour 
{
	public float speed;
	public Vector2 min;
	public Vector2 max;
	public float left;	
	public float up;
	public Player player1;
	public Player player2;
	private Vector3 _init_pos;

	void Start () 	
	{
		Replace();
		_init_pos = transform.position;
       // CheckPlayerCollision(player1);
        //CheckPlayerCollision(player2);
    }
    private void FixedUpdate() 
	{
        Move();
        CheckBoundaries();
        CheckPlayerCollision(player1);
        CheckPlayerCollision(player2);
	}
    private void  CheckPlayerCollision(Player player)
    {
        Vector3 playerPos;
        Vector3 ballPos;
        Vector3 ballSize;
        Vector3 playerSize;

        ballPos = transform.position;
        playerPos = player.transform.position;
        ballSize = transform.lossyScale;
        playerSize = player.transform.lossyScale;
        ballSize /= 2;
        playerSize /= 2;
        if (((ballPos.x + ballSize.x) > (playerPos.x - playerSize.x)) && ((ballPos.x - ballSize.x) < (playerPos.x + playerSize.x)))
        {
            if (((ballPos.y + ballSize.y) < (playerPos.y + playerSize.y)) && ((ballPos.y - ballSize.y) > (playerPos.y - playerSize.y)))
            {
                left = player.isPlayer1 ? 1 : -1;
            }
        }
    }
	private void CheckBoundaries()
	{
		Vector3 tmp;
		bool scored = false;

		tmp = transform.position;
		if (tmp.y > max.y)
			up = -1.0f;
		else if (tmp.y < min.y)
			up = 1.0f;
		if (tmp.x > max.x)
		{
			player1.incr_score(1);
			scored = true;
		}
		else if (tmp.x < min.x)
		{
			player2.incr_score(1);
			scored = true;
		}
		if (scored)
		{
			Debug.Log("Player 1: " + player1.score + " | Player 2: " + player2.score);
			Replace();
		}
	}
	private void Replace()
	{
		int rd = Random.Range(1, 5);
		transform.position = _init_pos;
		left = 1.0f;
		up = 1.0f;
		if (rd > 2)
			left = -1.0f;
		if (rd % 2 == 0)
			up = -1.0f;
	}
	private void Move() 
	{
		Vector3 dir = new Vector3(left, up, 0);

		transform.Translate(dir * speed * Time.fixedDeltaTime);
	}
}