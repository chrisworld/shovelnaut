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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DigOut()
    {
        GetComponent<Renderer>().enabled = true;
        isBuried = false;
    }
}
