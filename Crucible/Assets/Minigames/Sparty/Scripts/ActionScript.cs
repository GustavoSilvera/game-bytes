using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScript : MonoBehaviour
{
    public GameObject projectile;
    int player;
    bool direction;

    [SerializeField]
    public float speed = 4;

    private void Start()
    {
        if (gameObject.name == "Player1") player = 0;
        else player = 1;
    }

    void Update()
    {
        if (MinigameInputHelper.GetHorizontalAxis(0) < -0.1) direction = true; //left
        else if (MinigameInputHelper.GetHorizontalAxis(0) > 0.1) direction = false; //right
        if (MinigameInputHelper.IsButton1Down(player))
        {
            GameObject p;
            if (direction)
            {
                p = Instantiate(projectile, transform.position - new Vector3((float) 1, 0, 0), transform.rotation);
                p.GetComponent<Rigidbody2D>().velocity = new Vector2(-10.0f, 3.0f);
            }
            else
            {
                p = Instantiate(projectile, transform.position + new Vector3((float) 1, 0, 0), transform.rotation);
                p.GetComponent<Rigidbody2D>().velocity = new Vector2(10.0f, 3.0f);
            }
        }
    }
}

/*public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        HealthScript c = (HealthScript)collision.gameObject.GetComponent(typeof(HealthScript));
        c.TakeDamage();
        Destroy(gameObject);
    }
}*/