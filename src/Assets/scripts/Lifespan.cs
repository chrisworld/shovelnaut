using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{

    public float lifespan;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
