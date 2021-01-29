using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject[] aims;
    private GameObject aim;

    private bool arrivedAtAim = false;
    // Start is called before the first frame update
    void Start()
    {
        if (aims == null)
        {
            aims = GameObject.FindGameObjectsWithTag("EnemyAim");
            // aim = GameObject.FindWithTag("EnemyAim");
            
        }

        float dist = float.PositiveInfinity;
        foreach (var a in aims)
        {
            Vector2 dist2d = new Vector2(this.transform.position.x - a.transform.position.x,
                this.transform.position.y - a.transform.position.y);
            var d = dist2d.sqrMagnitude;
            if (d < dist)
            {
                dist = d;
                aim = a;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: doesn't work as expected yet. both branches executed?!
        Vector2 dist2d = new Vector2(this.transform.position.x - aim.transform.position.x,
            this.transform.position.y - aim.transform.position.y);
        if (dist2d.sqrMagnitude > 0.5f)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, aim.transform.position, 4.0f * Time.deltaTime);
        }
        else if (!arrivedAtAim) ;
        {
            // Debug.Log("arrived!" + dist2d.sqrMagnitude);
            arrivedAtAim = true;
        }
    }
}
