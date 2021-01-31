using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeFootSteps : MonoBehaviour
{

    public Transform footstepsUp;
    public Transform footstepsDown;
    public Transform footstepsLeft;
    public Transform footstepsRight;


    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetFootSteps", 1, speed);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetFootSteps()
    {
        Vector2 v = GetComponent<Rigidbody2D>().velocity.normalized;
        Transform steps = null;

        if (Math.Abs(v.y) > Math.Abs(v.x))
        {
            steps = Instantiate(footstepsUp as Transform);
        }
        else if(Math.Abs(v.y) < Math.Abs(v.x))
        {
            steps = Instantiate(footstepsLeft as Transform);
        }
      /*  else if (v.y < 0)
        {
            steps = Instantiate(footstepsDown as Transform);
        }
        else if (v.y > 0)
        {
            steps = Instantiate(footstepsUp as Transform);
        }*/

        if(steps != null)
        steps.position = transform.position;
    }
}
