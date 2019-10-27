using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
	// Start is called before the first frame update
	bool hover = false;
	const float hoverScale = (float)1.5;
	int current_place = 0;
	GameObject p1Select, p2Select;

	void Start()
	{
		p1Select = GameObject.Find("Player1Select");
		p2Select = GameObject.Find("Player2Select");
		//gameObject.name == "Player1"
		//tennis = GameObject.Find("TennisButton");
		//karate = GameObject.Find("KarateButton");
		//baseball = GameObject.Find("BaseballButton");
		if(this.name == "TennisButton"){
			hover = true;
			current_place = 0;
		}if(this.name == "KarateButton"){
			hover = false;
			current_place = 1;
		}if(this.name == "BaseballButton"){
			hover = false;
			current_place = 2;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(p1Select.GetComponent<MenuScript>().place == current_place 
		|| p2Select.GetComponent<MenuScript>().place == current_place) hover = true;
		else hover = false;
		if(hover) transform.localScale += new Vector3(1, 1,1);//scale
		transform.localScale = new Vector3(1,1,1);//scale

	}
}
