using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : MonoBehaviour
{


    public float lifeSpan;

   public LayerMask collision;
    public float destroyRadius;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lifespan());
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, destroyRadius, collision))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Lifespan()
    {
        yield return new WaitForSeconds(lifeSpan);

        Destroy(gameObject);
    }
}
