using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
	// Start is called before the first frame update
 	public AudioSource boing;
	public AudioSource mario;
	public AudioSource stomp;
	public AudioSource die;
	public void PlayBoing(){
		boing.Play();
	}
	public void PlayMario(){
		mario.Play();
	}
	public void PlayStomp(){
		stomp.Play();
	}
	public void PlayDie(){
		die.Play();
	}
}
