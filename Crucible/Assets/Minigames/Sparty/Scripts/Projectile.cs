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

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        lastFrameVelocity = rb.velocity;
    }
    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
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
