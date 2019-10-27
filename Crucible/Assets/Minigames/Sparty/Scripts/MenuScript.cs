using System.Collections;
using System.Collections.Generic;
using UnityEngine;
																																																																																																																																																																																																			
public class MenuScript : MonoBehaviour
{
	GameObject player1;//, player2;
	// Start is called before the first frame update
	public int place = 0;
	int player = 0;
	bool up = false;
	bool down = false;
	const int min = 0;
	const int max = 2;
	void Start()
	{
		player1 = GameObject.Find("Player1Select");
		if(this.name == "Player1Select") player = 0;
		else player = 1;
		place = 0;
		//player2 = GameObject.Find("Player2Select");
		
	}

	// Update is called once per frame
	void Update()
	{
		//MinigameInputHelper.IsButton1Down(player)
		if(MinigameInputHelper.GetVerticalAxis(player) > 0 && !up && place > min){
			place--;
			up = true;
			transform.Translate(
				0, 
				(float)1.6,//metres 
				0.0f
			);		
			down = false;		
		}
		if (MinigameInputHelper.GetVerticalAxis(player) < 0 && !down && place < max){
			place++;
			down = true;
			transform.Translate(
				0, 
				(float)-1.6,//metres 
				0.0f
			);	
			up = false;	
		}
		if(MinigameInputHelper.GetVerticalAxis(player) == 0 ){
			up = false;
			down = false;		
		}
		
	}
}
