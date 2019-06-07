using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHall : Building 
{
	public float respawnDelay;
	private Vector3 _spawnArea;
	private float _delay;
	public GameObject population;
	private void OnEnable() 
	{
		BuildingDestroyEvent.OnBuildingDestroyed += SlowDownRespown;
	}
	private void OnDisable() 
	{
		BuildingDestroyEvent.OnBuildingDestroyed -= SlowDownRespown;
	}
	private void Start () 
	{
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		_delay = 0.0f;
		_spawnArea = transform.position;
		_spawnArea.y -=  (box.size.y /2);
		
	}
	public void SlowDownRespown()
	{

		if (gameObject.CompareTag(Tags.OrcBuilding))
		{
			Debug.Log("The Orc spawn rate as slow down");
		}
		else
		{
			Debug.Log("The Human spawn rate as slow down");
		}
		respawnDelay += 2.5f;
	}
	private void FixedUpdate() 
	{
		_delay += Time.fixedDeltaTime;
		if (_delay > respawnDelay)
		{
			Populate();
			_delay = 0.0f;
		}
	}
	private void OnDestroy() 
	{
		if(gameObject.CompareTag(Tags.HumanBuilding))
		{
			Debug.Log("The Orcs Team Wins");
		}
		else
		{
			Debug.Log("The Human Team Wins");
		}
	}
	private void Populate()
	{
		Instantiate(population, _spawnArea, Quaternion.identity);
	}
}
