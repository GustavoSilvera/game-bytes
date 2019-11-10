﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
	Vector2 accel, vel, read_vel2;
	Vector2 joystick, pos, pos2, pos2_old;
	public string TYPE;
	float g = (float)2.5;//gravity (m/s^2)
	float friction = (float)0.5;//between 0 and 1;
	float max_vel = 15;//maximum horizontal velocity
	float jump_vel = 30;//innitial velocity for jump
	float run_vel = 10;
	int jump_count = 0;
	bool double_jump;
	int player = 0;
	double playerWidth = 1;
	double playerHeight = 1;
	bool up = false;
	int max_jump=1;
	bool is_in = false;
	struct rect{
		public Vector2 pos;
		public float width, height;
	};
	rect platform;
	SFX sound;
	GameObject other;
	Animator animator;
	float jumpTime = 0.0f;

	string other_player_name;
	// Use this for initialization
	void Start () {
		sound = FindObjectOfType<SFX>();
		//init other player
		if (gameObject.name == "Player1"){
			player = 0;
			other_player_name = "Player2";
		}
		else{
			player = 1;
			other_player_name = "Player1";
		}
		other = GameObject.Find(other_player_name);
		animator = gameObject.GetComponent<Animator>();
		//init this player
		if(TYPE == "karate"){
			double_jump = true;
			max_jump = 2;
			jump_vel = 25;
			run_vel = 7;
			max_vel = 20;
			g = 2;//more floaty
			friction = (float)0.7;//more friction
		}
		else if(TYPE == "tennis"){
			double_jump = false;
			max_jump = 1;
			jump_vel = 35;
			run_vel = 10;
			max_vel = 15;
		}
		else{
			double_jump = false;
			max_vel = 12;
			jump_vel = 30;
			run_vel = 15;
			friction = (float)0.8;//more friction
		}
		accel = new Vector3(0,-g);
		vel = new Vector3(0,0);
		is_in = false;
		//init platform
		platform.pos = set_vec2(0, (float)-3.5);
		platform.width = 18;//complete length across end to end
		platform.height = 1;//thickness
	}
	float abs(float x){
		if(x < 0) return -x;
		return x;
	}
	float clamp(float min, float max, float val){
		if(val > max) return max;
		if(val < min) return min;
		return val;
	}
	Vector2 set_vec2(float x, float y){
		return (new Vector2(x, y));
	}
	void jump(){
		animator.SetInteger("state", 2);
		jumpTime = 0;
		jump_count++;
		sound.PlayMario();
		vel.y = jump_vel;//m/s BOOST
	}
	void run(float amnt){
		if(amnt != 0) {
			accel.x = amnt;
		}
		else {
			accel.x = (float)(-friction*vel.x);
		}
	}
	bool within_bounds(Vector2 obj, rect r){
		float rad = r.width/2;
		return(	
			obj.x <= r.pos.x + rad && 
			obj.x >= r.pos.x - rad &&
			obj.y <= r.pos.y + r.height// &&
			//obj.y >= r.pos.y - r.height
		);
	}
	int sign(float v){
		if(v>0) return 1;
		else if (v==0) return 0;
		return -1;
	}
	float sqr(float a){
		return a*a;
	}
	void collision(float ground){
		animator.SetInteger("state", 0);
		if(!is_in){		
			if (pos.y < ground + playerHeight/2.0 || vel.y > 0) {
				//vel.x = -(float)((vel.x + sign(read_vel2.x)*5));//deflected
				vel.x = -(float)((vel.x - read_vel2.x));//deflected
				sound.PlayBump();
			}
			vel.y = -(float)(vel.y);
			if(pos.y > ground && !(vel.y < 0)){//since velocity just got inverted... checking that it WAS going down
				if(pos.y > pos2.y && !GameObject.Find(other_player_name).GetComponent<ActionScript>().defenseOn){
					this.GetComponent<HealthScript>().TakeDamage(1);
					sound.PlayStomp();
				}		
				//float inside = sign(pos2.y - pos.y)*(1 - abs(pos2.y - pos.y));
				//if(pos.y > pos2.y)//if(!(pos2.y > pos.y)) 
				//this.transform.Translate(0f, -inside, 0f);	//push back a little
			}
		}
	}
	// Update is called once per frame
	void Update () {
		animator.SetInteger("state", 0);
		if (MinigameInputHelper.GetHorizontalAxis(player) < 0.1 &&
			MinigameInputHelper.GetHorizontalAxis(player) > -0.1 &&
			animator.GetInteger("state") < 2)
			animator.SetInteger("state", 0);
		else if(animator.GetInteger("state") < 2) animator.SetInteger("state", 1);
		jumpTime += Time.deltaTime;
		if (animator.GetInteger("state") == 2 && jumpTime > 1) {
			jumpTime = 0;
			animator.SetInteger("state", 0);
		}
		pos = set_vec2(this.transform.position.x, this.transform.position.y);
		pos2 = set_vec2(other.transform.position.x, other.transform.position.y);
		read_vel2 = (pos2 - pos2_old)/Time.deltaTime;
		pos2_old = set_vec2(other.transform.position.x, other.transform.position.y);
		joystick = set_vec2(
			MinigameInputHelper.GetHorizontalAxis(player), 
			MinigameInputHelper.GetVerticalAxis(player)
		);
		float ground = (platform.pos.y+platform.height);
		if( joystick.y > 0 && (up == false || pos.y <= ground + 0.1))
		{
			if( pos.x <= platform.pos.x + platform.width/2 &&
				pos.x >= platform.pos.x - platform.width/2 &&
				pos.y <= ground ) jump_count = 0;
			if(jump_count < max_jump){// || (double_jump && jump_count < 1)){
				jump();
				//audioSrc.PlayClipAtPoint(gameObject.GetComponent<AudioSource>.clip, pos);
				up = true;
			}
		}
		if(joystick.y > 0) up = true;
		else up = false;
		//collision with platform
		if(within_bounds(pos, platform) && !up) {
			vel.y = 0;//transform.position.y = platform;
			transform.Translate(0f, (float)(ground - pos.y), 0f);
		}
		else if (pos.y < -8){
			vel.y = 0;//stops from infinite falling
			double spawn = -6.5;//assuming fell on right side
			if(pos2.x < platform.pos.x) spawn = 6.5;//actually fell on the left side
			const int start_y = 5;
			transform.Translate((float)(spawn - pos.x), (float)(start_y - pos.y), 0f);
			other.GetComponent<HealthScript>().TakeDamage(1);
			sound.PlayDie();
		}
		if(pos.y <= ground){//+thresh?
			if( pos.x <= platform.pos.x + platform.width/2 &&
				pos.x >= platform.pos.x - platform.width/2 && !up){
				jump_count = 0;
			}
			run((run_vel/10) * joystick.x);
		}
		else if(abs(joystick.x) > 0) run((float)0.9*joystick.x);//slight air movement
		else accel.x = (float)(-0.001*vel.x);
		vel.x += accel.x;
		vel.y += accel.y;
		vel.x = clamp(-max_vel, max_vel, vel.x);//clamped at max_vel m/s
		vel.y = clamp(-2*max_vel, 2*max_vel, vel.y);//terminal velocity
		if(abs(pos.x - pos2.x) <= playerWidth && abs(pos.y - pos2.y) <= playerHeight){
			collision(ground);
			is_in = true;
		}
		else{
			is_in = false;
		}
		transform.Translate(
			vel.x*Time.deltaTime, 
			vel.y*Time.deltaTime, 
			0.0f
		);
	}
}
