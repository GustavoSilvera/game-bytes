using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	GameObject p1Select, p2Select;
	GameObject player1, player2;
	GameObject main_game;
	int count = 0;
	void Start()
	{
		p1Select = GameObject.Find("Player1Select");
		p2Select = GameObject.Find("Player2Select ");
		main_game = GameObject.Find("Game");
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		if(main_game == null) Debug.LogError("could not find main game");
		if(p1Select == null) Debug.LogError("could not find p1");
		if(p2Select == null) Debug.LogError("could not find p2");
		if(player1 == null) Debug.LogError("could not find player1");
		if(player2 == null) Debug.LogError("could not find player2");
		count = 0;
		main_game.gameObject.SetActive(false);//turns off the game
	}

	// Update is called once per frame
	void Update()
	{
		bool p1_select = p1Select.GetComponent<MenuScript>().select;
		bool p2_select = p2Select.GetComponent<MenuScript>().select;
		int p1_string = p1Select.GetComponent<MenuScript>().TYPE;
		int p2_string = p2Select.GetComponent<MenuScript>().TYPE;
		if(p1_select && p2_select){//BEGIN GAME
			count++;
			player1.GetComponent<MoveScript>().TYPE = p1_string;
			player2.GetComponent<MoveScript>().TYPE = p2_string;
			if(count > 50){//timer
				main_game.gameObject.SetActive(true);
				this.gameObject.SetActive(false);
			}
		}
		
	}
}
