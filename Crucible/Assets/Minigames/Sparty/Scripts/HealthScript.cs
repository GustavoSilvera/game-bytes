using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

	private float timer = 0.0f;
	private float waitTime = 1.0f;

	private int startingHealth = 100;
	public int playerHealth;
	public Slider healthSlider; //reference to UI health bar

	// Use this for initialization
	void Start () {
		playerHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
        
		// Check if we have reached beyond 1 seconds.
		// Subtracting one is more accurate over time than resetting to zero.
		if (timer > waitTime)
		{
			// Remove the recorded 2 seconds.
			timer = timer - waitTime;
			TakeDamage(2);
		}
	}

    public void TakeDamage (int amount)
	{
		//playerHealth -= amount;
		//healthSlider.value = playerHealth;
	}
}
