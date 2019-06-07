//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

	private Cube cubeA;
	private Cube cubeD;
	private Cube cubeS;
	public GameObject cubeA_prefab;
	public GameObject cubeS_prefab;
	public GameObject cubeD_prefab;
	float aTime = 2.0f;
	public float Y;
	// Use this for initialization
	void Start () 
	{
		SpawnCubes();
	}
	private void SpawnCubes()
	{
		if (Random.Range(0, 2) == 0)
		{
			cubeA = SpawnOneCube(cubeA_prefab, new Vector3(-2, 8, 0));
			if (cubeA)
				cubeA.speed = Random.Range(0.1f, 0.2f);

		}
		if (Random.Range(0, 2) == 0)
		{
			cubeD = SpawnOneCube(cubeD_prefab, new Vector3(2, 8, 0));
			if (cubeD)
				cubeD.speed = Random.Range(0.1f, 0.2f);
		}
		if (Random.Range(0, 2) == 0)
		{
			cubeS = SpawnOneCube(cubeS_prefab, new Vector3(0, 8, 0));
			if (cubeS)
				cubeS.speed = Random.Range(0.1f, 0.2f);
		}
	}
	private Cube SpawnOneCube(GameObject model, Vector3 pos)
	{
		GameObject obj;
		obj = Instantiate(model, this.transform);
		if (!obj)
			return null;
		obj.transform.position = pos;
		obj.SetActive(true);
		return obj.GetComponent<Cube>();
	}

	private void CheckCubePos(GameObject cube)
	{
		if (!cube)
			return;
		if (cube.gameObject.transform.position.y <= Y)
		{
			Destroy(cube.gameObject);
		}
	}
	private void HandleInputs()
	{
		if (Input.GetKeyDown(KeyCode.A) && cubeA)
		{
			Debug.Log("Precision : " + (cubeA.transform.position.y).ToString("F1"));
			Destroy(cubeA.gameObject);
		}
		if (Input.GetKeyDown(KeyCode.D) && cubeD)
		{
			Debug.Log("Precision : " + (cubeD.transform.position.y - Y).ToString("F1"));
			Destroy(cubeD.gameObject);
		}
		if (Input.GetKeyDown(KeyCode.S) && cubeS)
		{
			Debug.Log("Precision : " + (cubeS.transform.position.y - Y).ToString("F1"));
			Destroy(cubeS.gameObject);
		}
	}
	void Update () 
	{
		HandleInputs();
		if (cubeA)
			CheckCubePos(cubeA.gameObject);
		if (cubeS)
			CheckCubePos(cubeS.gameObject);
		if (cubeD)
			CheckCubePos(cubeD.gameObject);
		aTime -= Time.deltaTime;
		if (aTime < 0.0f)
		{
			aTime = 2.0f;
			SpawnCubes();
		}
	}
}
