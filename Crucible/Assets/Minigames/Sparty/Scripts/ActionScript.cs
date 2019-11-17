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
    float defCooldown = 4.0f;
    //int shieldHits = 0;

	float cooldown = 0.5f;
	float time = 0.0f;
	int TYPE = 0;
    [SerializeField]
    public float speed = 25.0f;
    SpriteRenderer spriteRenderer;

    Animator animator;
    float attacktime = 0.0f;

    int ballCharge = 0;

    private void Start()
	{
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

        animator = gameObject.GetComponent<Animator>();
    }

	void Update(){
        if (direction) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
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

        if ((button1 || MinigameInputHelper.IsButton1Held(player)) && time >= cooldown && TYPE != 4 && ballCharge < 30) ballCharge++;

        if (MinigameInputHelper.IsButton1Up(player) && time >= cooldown && ballCharge > 0 && TYPE != 4){
            animator.SetInteger("state", 3 + TYPE);
            attacktime = 0;
            time = 0;
			GameObject p;
            attacktime = 0;
            if (direction){
				p = Instantiate(projectile, transform.position - new Vector3((float)1, 0, 0), transform.rotation);
				p.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.25f, 0.5f) * ballCharge;

			}
			else{
				p = Instantiate(projectile, transform.position + new Vector3((float)1, 0, 0), transform.rotation);
				p.GetComponent<Rigidbody2D>().velocity = new Vector2(1.25f, 0.5f) * ballCharge;
			}
            ballCharge = 0;
        }

        if(attacktime > 0.5 && animator.GetInteger("state") == 3 + TYPE) animator.SetInteger("state", 0 + TYPE);
        attacktime += Time.deltaTime;

        if (button1 && time >= cooldown && TYPE != 0){//kick
			double xdist = pos_x - o_pos_x;
            /*if (!direction && ((pos_x < 0 && xdist > 0 && xdist < 2) ||
                (pos_x > 0 && xdist < 0 && xdist > -2)) &&
                other.GetComponent<ActionScript>().defenseOn)
            {
                other.GetComponent<ActionScript>().shieldHits = other.GetComponent<ActionScript>().shieldHits + 1;
            }*/
            if (!direction && ((pos_x < 0 && xdist > 0 && xdist < 2) ||
				(pos_x > 0 && xdist < 0 && xdist > -2)) &&
				!other.GetComponent<ActionScript>().defenseOn)
			{
				gameObject.GetComponent<HealthScript>().TakeDamage(1);
			}

            /*if (direction && ((pos_x < 0 && xdist < 0 && xdist > -2) ||
                (pos_x > 0 && xdist > 0 && xdist < 2)) &&
                other.GetComponent<ActionScript>().defenseOn)
            {
                other.GetComponent<ActionScript>().shieldHits++;
            }*/
            if (direction && ((pos_x < 0 && xdist < 0 && xdist > -2) ||
				(pos_x > 0 && xdist > 0 && xdist < 2)) &&
				!other.GetComponent<ActionScript>().defenseOn)
			{
				gameObject.GetComponent<HealthScript>().TakeDamage(1);
			}
		}
		if (button2 && time >= cooldown && TYPE != 0){//punch //IS THERE A SHIELD CHECK HERE??
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
        defCooldown += Time.deltaTime;
		if (button2 && TYPE != 8 && defCooldown > 3){
			shield.transform.localScale = new Vector3(1, 1, 1);
			defenseOn = true;
            defCooldown = 0;
		}
        /*if(shieldHits > 3)
        {
            defenseOn = false;
            shieldHits = 0;
            defCooldown = 0;
        }*/
		if (button2Up){
			shield.transform.localScale = new Vector3(0, 0, 0);
			defenseOn = false;
		}
	}
}
