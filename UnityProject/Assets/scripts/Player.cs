using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Transform diggingHole;
    public float moveSpeed;

    private Vector2 targetPosition;

    private SpaceshipPart diggablePart;


    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new Vector2(0.0f, 0.0f);

        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Controls
        /*
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Input.mousePosition;
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(targetPosition.x, targetPosition.y, 0.0f));
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
        */


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rigidbody.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        //Digging/Using shovel
        if (Input.GetKeyDown(KeyCode.X)){
            /*Tilemap ground = GameObject.Find("Ground").GetComponent<Tilemap>();

           Vector3Int tilePosition = ground.WorldToCell(transform.position);
            ground.SetTile(tilePosition);
            */

            Transform d = Instantiate(diggingHole) as Transform;

            d.position = gameObject.transform.position;

            //If collided with diggable ship part -> Dig it out!
            if(diggablePart != null)
            {
                diggablePart.DigOut();
            }
        }


       /* if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidbody.velocity = new Vector2(-moveSpeed, 0);

            //TODO: Animation left

        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigidbody.velocity = new Vector2(moveSpeed, 0);

        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigidbody.velocity = new Vector2(0, moveSpeed);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rigidbody.velocity = new Vector2(0, -moveSpeed);

        }*/
        



    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpaceshipPart"))
        {
            Debug.Log("Collided with SpaceshipPart");
            diggablePart = collision.GetComponent<SpaceshipPart>();

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SpaceshipPart"))
        {
            Debug.Log("UnCollided with SpaceshipPart");
            diggablePart = null;
        }
    }
}
