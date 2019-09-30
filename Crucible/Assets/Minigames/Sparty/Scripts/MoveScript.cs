using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
	Vector2 accel, vel;
	Vector2 joystick, pos;
	const float g = 3;//m/s^2
	const float friction = (float)0.5;//between 0 and 1;
	const float max_vel = 15;
	struct rect{
		public Vector2 pos;
		public float width, height;
	};
	rect platform;
	// Use this for initialization
	void Start () {
		accel = new Vector3(0,-g);
		vel = new Vector3(0,0);
		platform.pos = set_vec2(0, (float)-3.5);
		platform.width = 18;//complete length across end to end
		platform.height = 1;//thickness
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
	// Update is called once per frame
	void Update () {
		pos = set_vec2(this.transform.position.x, this.transform.position.y);
		joystick = set_vec2(MinigameInputHelper.GetHorizontalAxis(0), MinigameInputHelper.GetVerticalAxis(0));
		float ground = (platform.pos.y+platform.height);
		if(within_bounds(pos, platform) && joystick.y <= 0) {
			vel.y = 0;//transform.position.y = platform;
			transform.Translate(0f, (float)(ground - pos.y), 0f);
		}
		if(joystick.y > 0 && pos.y <= ground + 0.1){
			jump();
		}
		if(pos.y <= ground + 0.1) run(joystick.x);
		else accel.x = (float)(-0.001*vel.x);
		vel.x += accel.x;
		vel.y += accel.y;
		vel.x = clamp(-max_vel, max_vel, vel.x);//clamped at max_vel m/s
		transform.Translate(
			vel.x*Time.deltaTime, 
			vel.y*Time.deltaTime, 
			0.0f
		);
	}
}
