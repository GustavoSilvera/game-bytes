using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthScript : MonoBehaviour
{

	private float timer = 0.0f;
	private float waitTime = 0f;

	//private int startingHealth = 100;
	//public int playerHealth;
	//public Slider healthSlider; //reference to UI health bar

	int player;
	GameObject other;

	// Use this for initialization
	void Start()
	{
		if (gameObject.name == "Player1")
		{
			player = 1;
			other = GameObject.Find("Player2");
		}
		else
		{
			player = 2;
			other = GameObject.Find("Player1");
		}
	}

	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		// Check if we have reached beyond 1 seconds.
		// Subtracting one is more accurate over time than resetting to zero.
		if (timer > waitTime)
		{
			//MinigameController.Instance.AddScore(2, 2);
			// Remove the recorded 2 seconds.
			timer = timer - waitTime;
			var colorer = other.GetComponent<Renderer>();
			colorer.material.SetColor("_Color", Color.white);
			//TakeDamage(2);
		}
		double xdist = gameObject.transform.position.x - other.transform.position.x;
		double ydist = gameObject.transform.position.y - other.transform.position.y;
		if (xdist < 1 && xdist > -1 && ydist > 0.5 && ydist < 1.1) TakeDamage(1);
	}

	public void TakeDamage(int amount)
	{
		MinigameController.Instance.AddScore(player, amount);
		var colorer = other.GetComponent<Renderer>();
		colorer.material.SetColor("_Color", Color.red);
		waitTime = 0.3f;

		//playerHealth -= amount;
		//healthSlider.value = playerHealth;
	}
}
