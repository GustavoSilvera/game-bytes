using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    int player;

    // Use this for initialization
    void Start () {

        if (gameObject.name == "Player1") player = 0;
        else player = 1;

    }
	// Update is called once per frame
	void Update () {
		float x = (float)0.1*MinigameInputHelper.GetHorizontalAxis(0);
		float y = (float)0.1*MinigameInputHelper.GetVerticalAxis(0);
		transform.Translate(
		x, y, 0.0f
		);
	}
}
