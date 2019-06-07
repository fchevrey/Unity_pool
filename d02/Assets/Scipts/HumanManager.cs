using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour 
{
	//static public HumanManager instance{get; private set;}
	//private Vector3 _dest;
	private List<Unit> _humans;
	public Camera cam;
	// Use this for initialization
	void Start () 
	{
		_humans = new List<Unit>();	
	}
	public void AddToSelection(Unit unit)
	{
		_humans.Add(unit);
		unit.IsSelected();
	}
	public void EmptySelection()
	{
		_humans.Clear();
	}
	private void HandleClick()
	{
		RaycastHit2D hit;

		//Camera cam = Camera.current;
		if (cam == null)
			{
				Debug.Log("Pas de cam");
				return ;
			}
		//Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		Vector3 pos =  cam.ScreenToWorldPoint(Input.mousePosition);
		Vector2 pos2D= new Vector2(pos.x, pos.y);
		hit = Physics2D.Raycast(pos2D, Vector2.zero);
		if (hit.collider != null)
		{
			if (hit.collider.CompareTag(Tags.Human))
			{
				if (!Input.GetKey(KeyCode.LeftControl))
				{
					EmptySelection();				
				}
				AddToSelection(hit.transform.GetComponent<Unit>());
			}
			else if (hit.collider.CompareTag(Tags.OrcBuilding))
			{
				foreach (Unit human in _humans)
				{
					human.Attack(hit.transform.GetComponent<Element>());
				}
			}
			else if (hit.collider.CompareTag(Tags.Orc))
			{
				foreach (Unit human in _humans)
				{
					human.Attack(hit.transform.GetComponent<Element>());
				}
			}
		}
		else
		{
			foreach (Unit human in _humans)
			{
				human.GoTo(cam.ScreenToWorldPoint(Input.mousePosition));
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			HandleClick();
		}
		else if (Input.GetMouseButtonDown(1))
		{
			EmptySelection();
		}

	}
}
