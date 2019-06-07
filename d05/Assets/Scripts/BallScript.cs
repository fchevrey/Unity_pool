using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour 
{
	public bool canBeShoot ;//{ get; private set;}
	private Rigidbody _rigid;
	private Vector3 _lastPos;
	public GameObject flag;
	public GameObject cam;
	public UIManager ui;
	public AudioSource sound;
	public float rotSpeed;
	float shootTimer;
	public int club {get; private set;}

	void Start () 
	{
		_rigid = GetComponent<Rigidbody>();
		transform.LookAt(flag.transform);
		club = 0;
		ui.SetClub(club);
		_rigid.isKinematic = true;
	}
	private void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag(Tags.Water))
		{
			this.transform.position = _lastPos;
			GameManager.instance.Plouf();
		}
	}
	
	public void Shoot(Vector3 dir, float strength)
	{
		if (canBeShoot)
		{
			_rigid.angularDrag = 0.3f;
			_rigid.isKinematic = false;
			_rigid.AddForce(dir * (strength / (club + 1)), ForceMode.Impulse);
			canBeShoot = false;
			_lastPos = transform.position;
			GameManager.instance.SetFreeCamState(true);
			Invoke("SlowDown_invoke", 7.0f);
			shootTimer = 7.0f;
			sound.Play();
		}
	}
	public void SlowDown_invoke()
	{
		_rigid.angularDrag = 1.0f;
	}
	public void Stop() 
	{
		canBeShoot = true;
		_rigid.angularVelocity = Vector3.zero;
		_rigid.velocity = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.LookAt(flag.transform);
		cam.transform.localRotation = Quaternion.identity;
		_rigid.isKinematic = true;
		RotateAccordingToClub();
		GameManager.instance.SetFreeCamState(false);
	}
	private void FixedUpdate() 
	{
		Vector3 velocity = _rigid.velocity;
		if (!canBeShoot)
			shootTimer -= Time.deltaTime;
		if (_rigid.isKinematic)
			return ;
		if (!canBeShoot && shootTimer < 0.0f && velocity.x < 0.3f && velocity.z < 0.3f && velocity.y < 0.3f && _rigid.angularVelocity.magnitude < 12)
		{
			Stop();
		}
	}
	private void RotateAccordingToClub()
	{
		Vector3 axis = Vector3.zero;
		Vector3 rot;
		if (club > 2)
		{
			club = 0;
			axis.x = 30;
		}
		else if (club == 1)
		{
			axis.x = -10;
		}
		else 
		{
			axis.x = -20;
		}
		rot = transform.localEulerAngles;
		rot += axis;
		transform.localEulerAngles = rot;
	}
	private void ChangeClub()
	{
		club++;
		if (club > 2)
			ui.SetClub(0);
		else
			ui.SetClub(club);
		RotateAccordingToClub();
	}
	void Update () 
	{
		Vector3 axis = Vector3.zero;
		Vector3 rot;

		if (!canBeShoot)
			return ;
		if (Input.GetKey(KeyCode.A))
			axis.y = 10.0f;
		if (Input.GetKey(KeyCode.D))
			axis.y = -10.0f;
		axis *= rotSpeed * Time.deltaTime;
		rot = transform.localEulerAngles;
		rot += axis;
		transform.localEulerAngles = rot;
		if (Input.GetButtonDown("Club") || Input.GetKeyDown(KeyCode.KeypadPlus))
			ChangeClub();
	}
}
