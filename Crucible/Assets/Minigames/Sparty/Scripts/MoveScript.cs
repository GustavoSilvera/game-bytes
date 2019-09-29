using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
	double x_accel, y_accel, platform;
	double x_vel, y_vel;
	// Use this for initialization
	void Start () {
		x_accel = 0;
		y_accel = -9.8;
		x_vel = 0;
		y_vel = 0;
		platform = -3.5+1;
	}
	void jump(){
		y_accel = 100;//m/s^2	
	}
	void run(double amnt){
		x_accel = 10*amnt;
	}
	// Update is called once per frame
	void Update () {
		x_vel += x_accel;
		y_vel += y_accel;
		float xmove = MinigameInputHelper.GetHorizontalAxis(0);
		float ymove = MinigameInputHelper.GetVerticalAxis(0);
		if(transform.position.y <= platform && ymove <= 0) {
			y_vel = 0;//transform.position.y = platform;
			y_accel = 0;
			transform.Translate(0f, (float)(-transform.position.y + platform), 0f);
		}
		else{
			y_accel = -9.8;	
		}
		if(xmove != 0){
			run(xmove);
		}
		else{
			x_accel = -0.05*x_vel;//slow down			
		}
		if(ymove > 0 && transform.position.y <= platform+0.1){
			jump();
		}
		else y_accel = -9.8;	
		//float x = (float)0.1*MinigameInputHelper.GetHorizontalAxis(0);
		//float y = (float)0.1*MinigameInputHelper.GetVerticalAxis(0);
		transform.Translate(
			(float)(0.001*x_vel), (float)(0.001*y_vel), 0.0f
		);
	}
}
