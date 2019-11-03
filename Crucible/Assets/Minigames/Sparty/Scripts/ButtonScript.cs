﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
	// Start is called before the first frame update
	bool hover = false;
	bool selected = false;
	const float hoverScale = (float)1.2;
	int current_place = 1;
	GameObject p1Select, p2Select;

	void Start()
	{
		p1Select = GameObject.Find("Player1Select");
		p2Select = GameObject.Find("Player2Select ");
		//gameObject.name == "Player1"
		//tennis = GameObject.Find("TennisButton");
		//karate = GameObject.Find("KarateButton");
		//baseball = GameObject.Find("BaseballButton");
		if(this.name == "TennisButton"){
			hover = true;
			current_place = 0;
		}
		else if(this.name == "KarateButton"){
			hover = false;
			current_place = 1;
		}
		else if(this.name == "BaseballButton"){
			hover = false;
			current_place = 2;
		}

		if(p1Select == null) Debug.LogError("could not find Player1Select");
		if(p2Select == null) Debug.LogError("could not find Player2Select");

		//Debug.Log(current_place);
		//Debug.Log(this.name);
	}
	// Update is called once per frame
	void Update()
	{
		int cursor_p1 = p1Select.GetComponent<MenuScript>().place;
		int cursor_p2 = p2Select.GetComponent<MenuScript>().place;
		bool p1_select = p1Select.GetComponent<MenuScript>().select;
		bool p2_select = p2Select.GetComponent<MenuScript>().select;

		if(cursor_p1 == current_place || cursor_p2 == current_place) {
			hover = true;
		}
		else hover = false;
		if(hover) this.transform.localScale = new Vector3(hoverScale, hoverScale,1);//scale
		else this.transform.localScale = new Vector3(1,1,1);//scale

		if((cursor_p1 == current_place && p1_select) || (cursor_p2 == current_place && p2_select)){
			selected = true;
		}
		else selected = false;
		if(selected) this.GetComponent<Image>().color = Color.red;
		else this.GetComponent<Image>().color = Color.white;
		
	}
}
