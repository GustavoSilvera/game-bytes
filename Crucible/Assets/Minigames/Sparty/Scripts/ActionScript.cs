using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScript : MonoBehaviour
{
    public GameObject projectile;
    int player;

    [SerializeField]
    public float speed = 4;

    private void Start()
    {
        if (gameObject.name == "Player1") player = 1;
        else player = 2;
    }

    void Update()
    {
        if (MinigameInputHelper.IsButton1Down(player))
        {
            GameObject p = Instantiate(projectile, transform.position, transform.rotation);
            //p.velocity = transform.forward * speed;
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