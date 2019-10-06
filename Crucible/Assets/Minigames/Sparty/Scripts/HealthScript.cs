using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthScript : MonoBehaviour
{

	private float timer = 0.0f;
	private float waitTime = 1.0f;

	//private int startingHealth = 100;
	//public int playerHealth;
	//public Slider healthSlider; //reference to UI health bar

	int player;
	string otherName;

	// Use this for initialization
	void Start()
	{
		if (gameObject.name == "Player1")
		{
			player = 1;
			otherName = "Player2";
		}
		else
		{
			player = 2;
			otherName = "Player1";
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
			TakeDamage(2);
		}
	}

	public void TakeDamage(int amount)
	{
		MinigameController.Instance.AddScore(player, amount);
		//playerHealth -= amount;
		//healthSlider.value = playerHealth;
	}
}
