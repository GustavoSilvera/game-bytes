using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
	Vector2 accel, vel;
	Vector2 joystick, pos, pos2;
	const float g = 3;//m/s^2
	const float friction = (float)0.5;//between 0 and 1;
	const float max_vel = 15;
	bool is_in;
	string other_player_name;
	struct rect{
		public Vector2 pos;
		public float width, height;
	};
	rect platform;

	int player = 0;
	// Use this for initialization
	void Start () {
		accel = new Vector3(0,-g);
		vel = new Vector3(0,0);
		platform.pos = set_vec2(0, (float)-3.5);
		platform.width = 18;//complete length across end to end
		platform.height = 1;//thickness
		is_in = false;
		if (gameObject.name == "Player1"){
			player = 0;
			other_player_name = "Player2";
		}
		else{
			player = 1;
			other_player_name = "Player1";
		}
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
		vel.y = 30;//m/s BOOST	
	}
	void run(float amnt){
		if(amnt != 0) accel.x = amnt;
		else accel.x = (float)(-friction*vel.x);
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
	void collision(bool on_ground){
		if(!is_in){
			if (on_ground) vel.x = -(float)(vel.x);
			vel.y = -(float)(vel.y);
		}
	}
	// Update is called once per frame
	void Update () {
		pos = set_vec2(this.transform.position.x, this.transform.position.y);
		GameObject other = GameObject.Find(other_player_name);
		pos2 = set_vec2(other.transform.position.x, other.transform.position.y);
		joystick = set_vec2(
			MinigameInputHelper.GetHorizontalAxis(player), 
			MinigameInputHelper.GetVerticalAxis(player)
		);
		float ground = (platform.pos.y+platform.height);
		if(within_bounds(pos, platform) && joystick.y <= 0) {
			vel.y = 0;//transform.position.y = platform;
			transform.Translate(0f, (float)(ground - pos.y), 0f);
		}
		if(
			joystick.y > 0 && pos.y <= ground + 0.1 && //clicked jump and within y bound
			pos.x <= platform.pos.x + platform.width/2 && //within right bound
			pos.x >= platform.pos.x - platform.width/2)//within left bound
		{
			jump();
		}
		if(pos.y <= ground + 0.1) run(joystick.x);
		else accel.x = (float)(-0.001*vel.x);
		vel.x += accel.x;
		vel.y += accel.y;
		vel.x = clamp(-max_vel, max_vel, vel.x);//clamped at max_vel m/s
		vel.y = clamp(-3*max_vel, 3*max_vel, vel.y);//terminal velocity
		if(abs(pos.x - pos2.x) < 1 && abs(pos.y - pos2.y) < 1){
			collision((pos.y <= ground+0.1));
			is_in = true;
		}
		else is_in = false;
		transform.Translate(
			vel.x*Time.deltaTime, 
			vel.y*Time.deltaTime, 
			0.0f
		);
	}
}
