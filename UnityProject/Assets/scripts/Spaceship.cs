using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    //Number of Parts in the Ship
    public int shipPartCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Return parts to ship
        if (collision.CompareTag("Player"))
        {
            shipPartCount += collision.GetComponent<Player>().carriedShipPartsCount;
            collision.GetComponent<Player>().carriedShipPartsCount = 0;
        }
    }
}
