using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
	{
		HealthScript c = (HealthScript)collision.gameObject.GetComponent(typeof(HealthScript));
		c.TakeDamage(5);
		Destroy(gameObject);
	}
}
