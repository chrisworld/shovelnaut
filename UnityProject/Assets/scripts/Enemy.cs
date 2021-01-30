using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private GameObject[] aims;
    private GameObject nearestAim;
    private GameObject[] parts;
    private GameObject nearestPart;

    private bool arrivedAtAim = false;
    private bool holdsPart = false;
    private bool aggressive = false;

    private AIDestinationSetter aIDestinationSetter;

    // Start is called before the first frame update
    void Start()
    {

        Animator anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);

        /*
        if (aims == null)
        {
            aims = GameObject.FindGameObjectsWithTag("EnemyAim");
            
        }
        if (parts == null)
        {
            parts = GameObject.FindGameObjectsWithTag("SpaceshipPart");
        }
        FindNearestObjects(); */
        aIDestinationSetter = this.GetComponent<AIDestinationSetter>();
    }
    
    void FindNearestObjects()
    {
        float dist = float.PositiveInfinity;
        foreach (var a in aims)
        {
            Vector2 dist2d = new Vector2(this.transform.position.x - a.transform.position.x,
                this.transform.position.y - a.transform.position.y);
            var d = dist2d.sqrMagnitude;
            if (d < dist)
            {
                dist = d;
                nearestAim = a;
            }
        }

        dist = float.PositiveInfinity;
        foreach (var p in parts)
        {
            Vector2 dist2d = new Vector2(this.transform.position.x - p.transform.position.x,
                this.transform.position.y - p.transform.position.y);
            var d = dist2d.sqrMagnitude;
            if (d < dist)
            {
                dist = d;
                nearestPart = p;
            }
        }
    }


    void FixedUpdate()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Return parts to ship
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            // Debug.Log("Collided with player");

            // TODO: set player dazzle or something like that?
        } else  if (collision.CompareTag("Spaceship"))
        {
            // Debug.Log("Collided with spaceship");
            Spaceship spaceship = collision.GetComponent<Spaceship>();
            spaceship.StealPart();
            aIDestinationSetter.setTargetType(AIDestinationSetter.TargetType.EnemyAim);
            // TODO: remove spaceship part
        } else if(collision.CompareTag("EnemyAim"))
        {
            // Debug.Log("Collided with EnemyAim");
            GameObject aim = collision.gameObject;
            aIDestinationSetter.setTargetType(AIDestinationSetter.TargetType.Spaceship);
            // TODO: Drop/hide spaceship part
        } else
        {

            Debug.LogWarning("Collided with " + collision);
        }
    }
}
