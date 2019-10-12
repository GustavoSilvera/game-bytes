using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Just for debugging, adds some velocity during OnEnable")]
    private Vector2 initialVelocity;

    [SerializeField]
    private float minVelocity = 10f;

    private Vector2 lastFrameVelocity;
    private Rigidbody2D rb;

    GameObject p1;
    GameObject p2;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        lastFrameVelocity = rb.velocity;

        double xdist = gameObject.transform.position.x - p1.transform.position.x;
        double ydist = gameObject.transform.position.y - p1.transform.position.y;
        if (xdist > -0.5 && xdist < 0.5 && ydist < 0.5 && ydist > -0.5)
        {
            p2.GetComponent<HealthScript>().TakeDamage(1);
			Destroy(gameObject);

		}
        xdist = gameObject.transform.position.x - p2.transform.position.x;
        ydist = gameObject.transform.position.y - p2.transform.position.y;
        if (xdist > -0.5 && xdist < 0.5 && ydist < 0.5 && ydist > -0.5)
        {
            p1.GetComponent<HealthScript>().TakeDamage(1);
			Destroy(gameObject);
		}
    }
    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
        p1 = GameObject.Find("Player1");
        p2 = GameObject.Find("Player2");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector2 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);

        Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

    //End of Collision Stuff
    
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
