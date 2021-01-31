using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkEnemy : MonoBehaviour
{
    Rigidbody2D rigidbody;
    bool hasDirection;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();

        hasDirection = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDirection)
        {

            ChooseDirection();

            StartCoroutine(DirectionLifespan());
        }
    }

    private void ChooseDirection()
    {

        //0 is x-axis, 1 is y-axis
        int axis = Random.Range(0, 2);

        int dir = 0;

        while (dir == 0)
        {
            dir = Random.Range(-1, 2);
        }

        if (axis == 0)
        {
            rigidbody.velocity = new Vector2(dir * moveSpeed, 0);
        }
        else if (axis == 1)
        {
            rigidbody.velocity = new Vector2(0, dir * moveSpeed);
        }

        hasDirection = true;

    }

    IEnumerator DirectionLifespan()
    {
        yield return new WaitForSeconds(Random.Range(2,5));

        hasDirection = false;
    }

}
