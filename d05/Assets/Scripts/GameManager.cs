using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance {private set; get;}
	private GameObject freeCam;
//	public FollowCam followCam;
	public GameObject followCam;
	public GameObject shootCam;
	public BallScript ball;
	[SerializeField]private  int holesLeft;
	public GolfParcours[] parcours;
	private int _parcourIndex;
	public GolfParcours activeParcour;
	public float power;
	public UIManager ui;
	private int _shotsInHole;
	private int[] _shotsInParcour;
	private bool _loadPower = true;
	public float powerFillSpeed;
	public bool _waitForEnter = false;
	public bool _gameOver = false;
	public AudioSource plouf;
	public AudioSource applause;
	public AudioSource ballInHole;
	

	private void Awake() 
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}
	private void Shoot()
	{
		_loadPower = false;
		ball.Shoot(ball.transform.forward, power);
		_shotsInHole++;
		ui.SetShot(_shotsInHole);
	}
	public void Plouf()
	{
		_shotsInHole++;
		plouf.Play();
	}
	void Start () 
	{
		freeCam = GameObject.FindWithTag(Tags.MainCamera);
		freeCam.SetActive(false);
		GameObject go = GameObject.FindWithTag(Tags.Ball);
		ball = go.GetComponent<BallScript>();
		_parcourIndex = 0;
		_shotsInParcour = new int[parcours.Length];
		ChangeParcour();
	}
	public void SetFreeCamState(bool state)
	{
		freeCam.SetActive(state);
		followCam.SetActive(!state);
		if (state)
			_loadPower = false;
	}
	public void GameOver()
	{
		int totalPar = 0;
		int totalScore = 0;
		_gameOver = true;
		for (int i = 0; i < parcours.Length; i++)
		{
			totalPar += parcours[i].par;
			totalScore += _shotsInParcour[i];
		}
		string scoreName = ScoreName(totalScore, totalPar);
		ui.SetScore(_shotsInParcour, scoreName);
		ui.ShowScore(true, "quit");
	}
	public void Goal()
	{
		_shotsInParcour[_parcourIndex] = _shotsInHole;
		_shotsInHole = 0;
		_parcourIndex++;
		activeParcour.gameObject.SetActive(false);
		_waitForEnter = true;
		ball.enabled = false;
		Debug.Log("Goal");
		ui.SetScore(_shotsInParcour, null);
		applause.PlayDelayed(1.2f);
		ballInHole.Play();
		if (_parcourIndex >= parcours.Length)
		{
			GameOver();

			return ;
		}
		ui.ShowScore(true, "continue");
	}
	public string ScoreName(int score, int par)
	{
		if (score == 1)
			return ("Ace");
		if (score == par)
			return ("Par");
		else if (score == par - 1)
			return ("Birdie");
		else if (score == par - 2)
			return ("Eagle");
		else if (score == par + 1)
			return ("Boggey");
		else if (score == par + 2)
			return ("Double Bogey");
		else if (score == par + 3)
			return ("Triple Bogey");
		else if (score == par + 2)
			return ("double Bogey");
		else
		{
			return (score - par).ToString();
		}
	}
	private void ChangeParcour()
	{
		Debug.Log("ChangeParcour");
		ui.ShowScore(false, null);
		ball.enabled = true;
		activeParcour = parcours[_parcourIndex];
		ball.transform.position = activeParcour.startPos.position;
		freeCam.transform.position = ball.cam.transform.position;
		freeCam.transform.LookAt(activeParcour.flag.transform);

		ui.SetHudValues(activeParcour.par, _parcourIndex +1, 0);
		ball.flag = activeParcour.flag;
		ball.Stop();
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			SetFreeCamState(true);
		}
		if (freeCam.activeSelf && ball.canBeShoot && Input.GetKeyDown(KeyCode.Space))
		{
			SetFreeCamState(false);
		}
		if (_waitForEnter && Input.GetKeyDown(KeyCode.Return))
		{
			if (_gameOver)
				Application.Quit();
			else
				ChangeParcour();
		}
		else if (!Input.GetKey(KeyCode.Tab) && ball.canBeShoot && Input.GetKeyDown(KeyCode.Space))
		{
			if (_loadPower)
				Shoot();
			else 
				_loadPower = true;
		}
		if (_loadPower)
		{
			power += powerFillSpeed * 10 * Time.deltaTime;
			if (power > 50.0f)
			{
				power = 50.0f;
				powerFillSpeed *= -1;
			}
			else if (power < 5.0f)
			{
				powerFillSpeed *= -1;
				power = 5.0f;
			}
			ui.SetSliderValue(power);
		}
	}
}
