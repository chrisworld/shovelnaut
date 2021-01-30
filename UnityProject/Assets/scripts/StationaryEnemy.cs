using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{

    public Transform projectile;

    public Vector3[] directions;

    public float timeBetweenShots;

    private int directionCount;

    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("Shoot", 0, timeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        Transform p = Instantiate(projectile as Transform);

        p.position = gameObject.transform.position;

        p.gameObject.GetComponent<Rigidbody2D>().velocity = directions[directionCount] * projectileSpeed;

        if(directionCount == directions.Length - 1)
        {
            directionCount = 0;
        }
        else
        {
            directionCount++;
        }

    }
    
}
