using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeFootSteps : MonoBehaviour
{

    public Transform footstepsUp;
    public Transform footstepsDown;
    public Transform footstepsLeft;
    public Transform footstepsRight;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetFootSteps", 1, 1);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetFootSteps()
    {
        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        Transform steps = null;

        if (v.x < 0)
        {
            steps = Instantiate(footstepsLeft as Transform);
        }
        else if (v.x > 0)
        {
            steps = Instantiate(footstepsRight as Transform);
        }
        else if (v.y < 0)
        {
            steps = Instantiate(footstepsDown as Transform);
        }
        else if (v.y > 0)
        {
            steps = Instantiate(footstepsUp as Transform);
        }

        steps.position = transform.position;
    }
}
