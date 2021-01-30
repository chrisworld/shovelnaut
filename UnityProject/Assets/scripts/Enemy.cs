using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject[] aims;
    private GameObject nearestAim;
    private GameObject[] parts;
    private GameObject nearestPart;

    private bool arrivedAtAim = false;
    private bool holdsPart = false;
    private bool aggressive = false;

    public float enemySpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (aims == null)
        {
            aims = GameObject.FindGameObjectsWithTag("EnemyAim");
            
        }
        if (parts == null)
        {
            parts = GameObject.FindGameObjectsWithTag("SpaceshipPart");
        }
        FindNearestObjects();
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
        // bring parts to spaceship
        if (holdsPart)
        {
            Debug.Log("moving towards aim");
            Vector2 distToAim = new Vector2(this.transform.position.x - nearestAim.transform.position.x,
                this.transform.position.y - nearestAim.transform.position.y);
            if (distToAim.sqrMagnitude > 0.1f)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, nearestAim.transform.position, enemySpeed * Time.deltaTime);
            }
            else if (!arrivedAtAim)
            {
                holdsPart = false;
                FindNearestObjects();
                // TODO: hide part, drop part etc
            }

        }
        // find new parts
        else if(!holdsPart)
        {
            Debug.Log("moving towards part");
            Vector2 distToPart = new Vector2(this.transform.position.x -  nearestPart.transform.position.x,
                this.transform.position.y - nearestPart.transform.position.y);
            if (distToPart.sqrMagnitude > 0.1f)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, nearestPart.transform.position, enemySpeed * Time.deltaTime);
            }
            else if (!holdsPart)
            {
                holdsPart = true;
                // TODO: hide part, drop part etc
            }
        }
    }
}
