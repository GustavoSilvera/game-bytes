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
    GameObject other;

    public bool defenseOn;

    float cooldown = 0.5f;
    float time = 0.0f;

    [SerializeField]
    public float speed = 25.0f;

    private void Start()
    {
        if (gameObject.name == "Player1")
        {
            player = 0;
            other = GameObject.Find("Player2");
        }
        else
        {
            player = 1;
            other = GameObject.Find("Player1");
            direction = true;
        }
        GameObject.Find("p" + player + "Bubble").transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        //GameObject.Find("p" + player + "Bubble").transform.localScale = new Vector3(0, 0, 0);
        time += Time.deltaTime;
        if (MinigameInputHelper.GetHorizontalAxis(player) < -0.1) direction = true; //left
        else if (MinigameInputHelper.GetHorizontalAxis(player) > 0.1) direction = false; //right
        if (MinigameInputHelper.IsButton1Down(player) && time >= cooldown)
        {
            time = 0;
            GameObject p;
            if (direction)
            {
                p = Instantiate(projectile, transform.position - new Vector3((float)1, 0, 0), transform.rotation);
                p.GetComponent<Rigidbody2D>().velocity = new Vector2(-25.0f, 10.0f);
            }
            else
            {
                p = Instantiate(projectile, transform.position + new Vector3((float)1, 0, 0), transform.rotation);
                p.GetComponent<Rigidbody2D>().velocity = new Vector2(25.0f, 10.0f);
            }
        }
        if (MinigameInputHelper.IsButton1Down(player) && time >= cooldown)
        {
            double xdist = gameObject.transform.position.x - other.transform.position.x;
            if (!direction && ((gameObject.transform.position.x < 0 && xdist > 0 && xdist < 2) ||
                (gameObject.transform.position.x > 0 && xdist < 0 && xdist > -2)) &&
                !other.GetComponent<ActionScript>().defenseOn)
            {
                gameObject.GetComponent<HealthScript>().TakeDamage(1);
            }
            if (direction && ((gameObject.transform.position.x < 0 && xdist < 0 && xdist > -2) ||
                (gameObject.transform.position.x > 0 && xdist > 0 && xdist < 2)) &&
                !other.GetComponent<ActionScript>().defenseOn)
            {
                gameObject.GetComponent<HealthScript>().TakeDamage(1);
            }
        }
        if (MinigameInputHelper.IsButton2Down(player) && time >= cooldown)
        {
            double xdist = gameObject.transform.position.x - other.transform.position.x;
            if (!direction && ((gameObject.transform.position.x < 0 && xdist > 0 && xdist < 2) ||
                (gameObject.transform.position.x > 0 && xdist < 0 && xdist > -2)))
            {
                gameObject.GetComponent<HealthScript>().TakeDamage(1);
            }
            if (direction && ((gameObject.transform.position.x < 0 && xdist < 0 && xdist > -2) ||
                (gameObject.transform.position.x > 0 && xdist > 0 && xdist < 2)))
            {
                gameObject.GetComponent<HealthScript>().TakeDamage(1);
            }
        }
        if (MinigameInputHelper.IsButton2Down(player))
        {
            GameObject.Find("p" + player + "Bubble").transform.localScale = new Vector3(1, 1, 1);
            defenseOn = true;
        }
        if (MinigameInputHelper.IsButton2Up(player))
        {
            GameObject.Find("p" + player + "Bubble").transform.localScale = new Vector3(0, 0, 0);
            defenseOn = false;
        }
    }
}

/*public class Projectile : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
	    HealthScript c = (HealthScript)collision.gameObject.GetComponent(typeof(HealthScript));
	    c.TakeDamage();
	    Destroy(gameObject);
	}
}*/
