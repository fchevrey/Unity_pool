using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public Text parTxt;
	public Text holeTxt;
	public Text shotTxt;
	public Text clubTxt;
	public Text[] scoresTxt;
	public Text finalScoreTxt;
	public Text clickEnterText;
	public Slider power;
	public GameObject HUD;
	public GameObject Score;
	public GameObject enterObj;
	private bool _hold = true;
	void Start () 
	{
		ShowScore(false, null);
	}
	private void ShowScorePanel(bool state, string enterStr)
	{
		Score.SetActive(state);
		HUD.SetActive(!state);
		enterObj.SetActive(false);
		if (enterStr != null)
		{
			enterObj.SetActive(true);
			clickEnterText.text = enterStr;
		}
	}
	public void ShowScore(bool state, string enterStr)
	{
		_hold = !state;// to avoid the pannel to be disbale by tab
		ShowScorePanel(state, enterStr);
	}
	public void SetHudValues(int par, int hole, int shot)
	{
		parTxt.text = "Par : " + par.ToString();
		holeTxt.text = "Hole : " + hole.ToString();
		shotTxt.text = "Shot : " + shot.ToString();
	}
	public void SetPar(int value)
	{
		parTxt.text = "Par : " + value.ToString();
	}
	public void SetHole(int value)
	{
		holeTxt.text = "Hole : " + value.ToString();
	}
	public void SetShot(int value)
	{
		shotTxt.text = "Shot : " + value.ToString();
	}
	public void SetClub(int club)
	{
		if (club == 0)
			clubTxt.text = "Wood";
		else if (club == 1)
			clubTxt.text = "Iron";
		else 
			clubTxt.text = "Wedge";
	}
	public void SetSliderValue(float value)
	{
		power.value = value;
	}
	public void SetScore(int[] scores, string finalScore)
	{
		for(int i = 0; i < scoresTxt.Length && i < scores.Length; i++)
		{
			if (scores[i] > 0)
				scoresTxt[i].text = scores[i].ToString();
		}
		if (finalScore != null);
			finalScoreTxt.text = finalScore;
	}
	private void Update() 
	{
		if (!_hold)
			return;
		if (Input.GetKeyDown(KeyCode.Tab))
			ShowScorePanel(true, null);
		else if (Input.GetKeyUp(KeyCode.Tab))
			ShowScorePanel(false, null);
	}

}
