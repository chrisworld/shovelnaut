using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    //Number of Parts in the Ship
    public int shipPartCount;
    public int shovelMovesCount;
    public int maxShipParts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //LOSE-Condition
        if(shipPartCount == 0)
        {

        }
        else if (shipPartCount == maxShipParts)
        {
            //WIN-Condition
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Return parts to ship
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            shipPartCount += player.carriedShipPartsCount;
            player.carriedShipPartsCount = 0;


            if(player.shovelMovesCount == 0)
            {
                player.shovelMovesCount = shovelMovesCount;
            }
        }
    }

    public void StealPart()
    {
        shipPartCount--;
    }
}
