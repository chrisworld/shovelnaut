using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Transform diggingHole;
    public float moveSpeed;

    //how long shovel hitbox appears when hitting
    public float hitTime;

    private Vector2 targetPosition;

    private SpaceshipPart diggablePart;

    public int carriedShipPartsCount;

    public int shovelMovesCount;

    public bool canDig;


    Rigidbody2D rigidbody;


    enum Direction {Up, Down, Left, Right};

    private Direction direction;

    // Start is called before the first frame update
    void Start()
    {

        canDig = true;

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

        if(horizontal < 0)
        {
            direction = Direction.Left;
        }
        else if (horizontal > 0)
        {
            direction = Direction.Right;
        }
        else if (vertical < 0)
        {
            direction = Direction.Down;
        }
        else if (vertical > 0)
        {
            direction = Direction.Up;
        }

        //Digging with shovel
        if (Input.GetKeyDown(KeyCode.X) && shovelMovesCount > 0 && canDig){
            Tilemap ground = GameObject.Find("Ground").GetComponent<Tilemap>();

           Vector3Int tilePosition = ground.WorldToCell(transform.position);
            //ground.SetTile(tilePosition);

           Vector3 newPosition = ground.CellToWorld(tilePosition);

            Transform d = Instantiate(diggingHole) as Transform;

            d.position = new Vector3(newPosition.x, newPosition.y, 0);

            //If collided with diggable ship part -> Dig it out!
            if(diggablePart != null)
            {
                diggablePart.DigOut();
            }

            shovelMovesCount--;
        }

        //Hit with shovel
        if (Input.GetKeyDown(KeyCode.C) && shovelMovesCount > 0)
        {

            Transform shovelhitbox = null;

            if(direction == Direction.Left)
            {
              shovelhitbox = transform.Find("ShovelHitboxLeft");


            }
            else if (direction == Direction.Right)
            {
                shovelhitbox = transform.Find("ShovelHitboxRight");

            }
            else if (direction == Direction.Up)
            {
                shovelhitbox = transform.Find("ShovelHitboxUp");

            }
            else if (direction == Direction.Down)
            {
                shovelhitbox = transform.Find("ShovelHitboxDown");

            }

            shovelhitbox.gameObject.SetActive(true);

            StartCoroutine(DeactivateShovelHitbox(shovelhitbox.gameObject));
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

    IEnumerator DeactivateShovelHitbox(GameObject hitbox)
    {
        yield return new WaitForSeconds(hitTime);

        hitbox.SetActive(false);

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

    //Collect a ship part   
    public void GetShipPart()
    {
        carriedShipPartsCount++;
    }

    public void LoseShipPart()
    {
        carriedShipPartsCount--;
    }
}
