using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		HealthScript c = (HealthScript)collision.gameObject.GetComponent(typeof(HealthScript));
		c.TakeDamage();
		Destroy(gameObject);
	}
}
