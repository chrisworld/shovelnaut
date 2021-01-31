using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPart : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isBuried;

    void Start()
    {
        
        GetComponent<Renderer>().enabled = false;
    }


    public void DigIn()
    {
        GetComponent<Renderer>().enabled = false;
        isBuried = true;

        Transform collectHitbox = transform.Find("CollectHitbox");
        collectHitbox.gameObject.SetActive(false);
    }

    public void DigOut()
    {
        GetComponent<Renderer>().enabled = true;
        isBuried = false;

        Transform collectHitbox = transform.Find("CollectHitbox");
        collectHitbox.gameObject.SetActive(true);
    }

    public void Collect(Player player)
    {
        Debug.Log("Collecting SpaceShip part");

        player.GetShipPart();

        Destroy(gameObject);
    }
}
