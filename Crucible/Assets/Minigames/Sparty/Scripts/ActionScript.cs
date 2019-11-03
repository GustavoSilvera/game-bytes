using System.Linq;
using System;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScript : MonoBehaviour
{
	public GameObject projectile;
	int player;
	bool direction;
	GameObject other, shield;

	public bool defenseOn;

	float cooldown = 0.5f;
	float time = 0.0f;
	string TYPE = "null";
	[SerializeField]
	public float speed = 25.0f;

	private void Start()
	{
		if (gameObject.name == "Player1"){
			player = 0;
			other = GameObject.Find("Player2");
		}
		else{
			player = 1;
			other = GameObject.Find("Player1");
			direction = true;
		}
		GameObject.Find("p" + player + "Bubble").transform.localScale = new Vector3(0, 0, 0);//0 shield
		shield = GameObject.Find("p" + player + "Bubble");
		TYPE = this.GetComponent<MoveScript>().TYPE;//player type
		Debug.Log(TYPE);
	}

	void Update(){

		float horizontal_axis = MinigameInputHelper.GetHorizontalAxis(player);
		bool button1 = MinigameInputHelper.IsButton1Down(player);
		bool button2 = MinigameInputHelper.IsButton2Down(player);
		bool button2Up = MinigameInputHelper.IsButton2Up(player);
		float pos_x = gameObject.transform.position.x;
		float pos_y = gameObject.transform.position.y;
		float o_pos_x = other.transform.position.x;
		float o_pos_y = other.transform.position.y;

		time += Time.deltaTime;
		if (horizontal_axis < -0.1) direction = true; //left
		else if (horizontal_axis > 0.1) direction = false; //right
		if (button1 && time >= cooldown && TYPE != "karate"){
			time = 0;
			GameObject p;
			if (direction){
				p = Instantiate(projectile, transform.position - new Vector3((float)1, 0, 0), transform.rotation);
				p.GetComponent<Rigidbody2D>().velocity = new Vector2(-25.0f, 10.0f);
			}
			else{
				p = Instantiate(projectile, transform.position + new Vector3((float)1, 0, 0), transform.rotation);
				p.GetComponent<Rigidbody2D>().velocity = new Vector2(25.0f, 10.0f);
			}
		}
		if (button1 && time >= cooldown && TYPE != "tennis"){//kick
			double xdist = pos_x - o_pos_x;
			if (!direction && ((pos_x < 0 && xdist > 0 && xdist < 2) ||
				(pos_x > 0 && xdist < 0 && xdist > -2)) &&
				!other.GetComponent<ActionScript>().defenseOn
				)
			{
				gameObject.GetComponent<HealthScript>().TakeDamage(1);
			}
			if (direction && ((pos_x < 0 && xdist < 0 && xdist > -2) ||
				(pos_x > 0 && xdist > 0 && xdist < 2)) &&
				!other.GetComponent<ActionScript>().defenseOn)
			{
				gameObject.GetComponent<HealthScript>().TakeDamage(1);
			}
		}
		if (button2 && time >= cooldown && TYPE != "tennis"){//punch
			double xdist = pos_x - o_pos_x;
			if (!direction && ((pos_x < 0 && xdist > 0 && xdist < 2) ||
				(pos_x > 0 && xdist < 0 && xdist > -2)))
			{
				gameObject.GetComponent<HealthScript>().TakeDamage(1);
			}
			if (direction && ((pos_x < 0 && xdist < 0 && xdist > -2) ||
				(pos_x > 0 && xdist > 0 && xdist < 2)))
			{
				gameObject.GetComponent<HealthScript>().TakeDamage(1);
			}
		}
		if (button2 && TYPE != "baseball"){
			shield.transform.localScale = new Vector3(1, 1, 1);
			defenseOn = true;
		}
		if (button2Up){
			shield.transform.localScale = new Vector3(0, 0, 0);
			defenseOn = false;
		}
	}
}
