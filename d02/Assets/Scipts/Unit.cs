using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Element 
{
	public float speed;
	private Vector3 _dest;
	private Vector3 _dir;
	private bool _inMotion = false;
	private Animator _animator;
	private Jukebox _jukebox;
	private SpriteRenderer _render;
	private bool _attacking = false;
	private Element _target;
	private bool _inAttackPosition;
	private float _attack_delay = 1.0f;
	private float _attack_timer = 0.0f;
	void Start () 
	{
		_animator = GetComponent<Animator>();
		_render = GetComponent<SpriteRenderer>();
		_jukebox = GetComponentInChildren<Jukebox>();
	}
	public void Attack(Element target)
	{
		GoTo(target.transform.position);
		_target = target;
		_attacking = true;
	}
	private void OnTriggerEnter2D(Collider2D  other) 
	{
		if (_target != null && other.transform.CompareTag(_target.gameObject.tag))
		{
			if (_target == other.transform.GetComponent<Element>())
			{
				_inAttackPosition = true;
				_inMotion = false;
				_animator.SetBool("Move", false);
				_animator.SetBool("Attack", true);
				_animator.SetTrigger("change_direction");
			}
		}
	}
	public void IsSelected()
	{
		_jukebox.PlayTrack(e_UnitSound.selected, false);
	}
	public void GoTo(Vector3 dest)
	{
		Vector3 tmp;

		_target = null;
		_attacking = false;
		_inAttackPosition = false;
		_attack_timer = 0.0f;
		_animator.SetBool("Attack", false);
		_dest = dest;
		_inMotion = true;
		tmp = _dest - transform.position;
		tmp.z = 0;
		_dir = tmp;
		_dest.z = 0;
		_dir.Normalize();
		_jukebox.PlayTrack(e_UnitSound.acknowledge, false);
		SendMoveToAnimator();
	}
	private e_Dir GetDirEnum(short left, short up)
	{
		e_Dir  direction = e_Dir.none;

		if (left == 1)
		{
			if (up == 1)
				direction = e_Dir.up_left;
			else if (up == 0)
				direction = e_Dir.left;
			else if (up == -1)
				direction = e_Dir.down_left;
		}
		else if (left == 0)
		{
			if (up == 1)
				direction = e_Dir.up;
			else if (up == 0)
				direction = e_Dir.none;
			else if (up == -1)
				direction = e_Dir.down;
		}
		else if (left == -1)
		{
			if (up == 1)
				direction = e_Dir.up_right;
			else if (up == 0)
				direction = e_Dir.right;
			else if (up == -1)
				direction = e_Dir.down_right;
		}
		return direction;
	}
	private void SendMoveToAnimator()
	{
		e_Dir  direction;
		short left = 0;
		short up = 0;
		
		if (_dir.x > 0.2f)
		{
			left = 1;
			_render.flipX = false;
		}
		else if (_dir.x < -0.2f)
		{
			left = -1;
			_render.flipX = true;
		}
		if (_dir.y > 0.2f)
			up = 1;
		else if (_dir.y < -0.2f)
			up = -1;
		direction = GetDirEnum(left, up);
		_animator.SetInteger("direction", (int)direction);
		_animator.SetBool("Move", _inMotion);
		_animator.SetTrigger("change_direction");
	}

	private void Move() 
	{
		float dist;
		transform.Translate(_dir * speed * Time.fixedDeltaTime);
		dist = Vector3.Distance(transform.position, _dest);
		if (dist < 0.05f )
		{
			_inMotion = false;
			_animator.SetBool("Move", false);
			_jukebox.StopPlaying();
			if (_attacking)
			{
				_inAttackPosition = true;
				_animator.SetBool("Attack", true);
				_animator.SetTrigger("change_direction");
			}
		}
	}
	private void StopAttack()
	{
		_inAttackPosition = false;
		_attacking = false;
		_animator.SetBool("Attack", false);
		_target = null;
	}
	private void FixedUpdate() 
	{
		if (_inMotion)
		{
			Move();
		}
		if (_inAttackPosition)
		{
			_attack_timer += Time.fixedDeltaTime;
			if (_target == null)
			{
				StopAttack();
			}
			if (_attack_timer > _attack_delay)
			{
				if (_target.TakeDamage())
				{
					StopAttack();
				}
				_attack_timer = 0.0f;
			}
		}
	}
}
